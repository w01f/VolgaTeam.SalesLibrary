DROP PROCEDURE IF EXISTS `sp_get_navigation_user_report`;
CREATE PROCEDURE `sp_get_navigation_user_report`(in start_date datetime,in end_date datetime)
select su.first_name,
        su.last_name,
        group_concat(distinct concat(g.name,' (',(select count(id) from tbl_user_group where id_group = g.id),' users)')) as groups,
        concat(u_totals.total_count,' ', ifnull(group_concat(distinct concat('(',g_totals.name,'-',ROUND((u_totals.total_count/g_totals.total_count)*100),'%)')),'')) as totals,
        concat(u_libs.libs_count,' ', ifnull(group_concat(distinct concat('(',g_libs.name,'-',ROUND((u_libs.libs_count/g_libs.libs_count)*100),'%)')),'')) as libs,
        concat(u_pages.pages_count,' ', ifnull(group_concat(distinct concat('(',g_pages.name,'-',ROUND((u_pages.pages_count/g_pages.pages_count)*100),'%)')),'')) as pages
  from tbl_statistic_activity as sact
          join tbl_statistic_user as su on su.id_activity = sact.id
          join tbl_user as u on u.login = su.login
          left join (
            select su.login,count(su.id) as total_count from tbl_statistic_user as su
          join tbl_statistic_activity as sact on sact.id = su.id_activity and (sact.sub_type = 'Library Changed' or sact.sub_type = 'Page Changed')
    group by su.login
          ) as u_totals on u_totals.login = u.login
          left join (
            select su.login,count(su.id) as libs_count from tbl_statistic_user as su
          join tbl_statistic_activity as sact on sact.id = su.id_activity and sact.sub_type = 'Library Changed'
    group by su.login
          ) as u_libs on u_libs.login = u.login
          left join (
            select su.login,count(su.id) as pages_count from tbl_statistic_user as su
          join tbl_statistic_activity as sact on sact.id = su.id_activity and sact.sub_type = 'Page Changed'
    group by su.login
          ) as u_pages on u_pages.login = u.login
          left join tbl_user_group as ug on ug.id_user = u.id
          left join tbl_group as g on g.id = ug.id_group
          left join (
            select sg.name,count(sg.id) as total_count from tbl_statistic_group as sg
          join tbl_statistic_activity as sact on sact.id = sg.id_activity and (sact.sub_type = 'Library Changed' or sact.sub_type = 'Page Changed')
    group by sg.name
          ) as g_totals on g_totals.name = g.name
          left join (
            select sg.name,count(sg.id) as libs_count from tbl_statistic_group as sg
          join tbl_statistic_activity as sact on sact.id = sg.id_activity and sact.sub_type = 'Library Changed'
    group by sg.name
          ) as g_libs on g_libs.name = g.name
          left join (
            select sg.name,count(sg.id) as pages_count from tbl_statistic_group as sg
          join tbl_statistic_activity as sact on sact.id = sg.id_activity and sact.sub_type = 'Page Changed'
    group by sg.name
          ) as g_pages on g_pages.name = g.name
  where sact.date_time >= start_date and sact.date_time <= end_date
  group by su.first_name,su.last_name;
