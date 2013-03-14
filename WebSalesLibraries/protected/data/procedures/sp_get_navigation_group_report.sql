DROP PROCEDURE IF EXISTS `sp_get_navigation_group_report`;
CREATE PROCEDURE `sp_get_navigation_group_report`(in start_date datetime,in end_date datetime)
select distinct(sg.name),
        ifnull(g_totals.total_count,0) as totals,
        ifnull(g_libs.libs_count,0) as libs,
        ifnull(g_pages.pages_count,0) as pages
  from tbl_statistic_group as sg
          join tbl_statistic_activity as sact on sact.id = sg.id_activity
          left join (
            select sg.name,count(sg.id) as total_count from tbl_statistic_group as sg
          join tbl_statistic_activity as sact on sact.id = sg.id_activity and (sact.sub_type = 'Library Changed' or sact.sub_type = 'Page Changed')
    where sact.date_time >= start_date and sact.date_time <= end_date
    group by sg.name
          ) as g_totals on g_totals.name = sg.name
          left join (
            select sg.name,count(sg.id) as libs_count from tbl_statistic_group as sg
          join tbl_statistic_activity as sact on sact.id = sg.id_activity and sact.sub_type = 'Library Changed'
    where sact.date_time >= start_date and sact.date_time <= end_date
    group by sg.name
          ) as g_libs on g_libs.name = sg.name
          left join (
            select sg.name,count(sg.id) as pages_count from tbl_statistic_group as sg
          join tbl_statistic_activity as sact on sact.id = sg.id_activity and sact.sub_type = 'Page Changed'
    where sact.date_time >= start_date and sact.date_time <= end_date
    group by sg.name
          ) as g_pages on g_pages.name = sg.name
  where sact.date_time >= start_date and sact.date_time <= end_date;
