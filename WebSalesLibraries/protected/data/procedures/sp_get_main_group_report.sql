DROP PROCEDURE IF EXISTS `sp_get_main_group_report`;
CREATE PROCEDURE `sp_get_main_group_report`(in start_date datetime,in end_date datetime)
select distinct(sg.name),
        ifnull(g_totals.total_count,0) as totals,
        ifnull(g_logins.log_count,0) as logins,
        ifnull(g_docs.doc_count,0) as docs,
        ifnull(g_videos.video_count,0) as videos
  from tbl_statistic_group as sg
          join tbl_statistic_activity as sact on sact.id = sg.id_activity
          left join (
            select sg.name,count(sact.id) as total_count from tbl_statistic_group as sg
          join tbl_statistic_activity as sact on sact.id = sg.id_activity
    where sact.date_time >= start_date and sact.date_time <= end_date
    group by sg.name
          ) as g_totals on g_totals.name = sg.name
          left join (
            select sg.name,count(sg.id) as log_count from tbl_statistic_group as sg
          join tbl_statistic_activity as sact on sact.id = sg.id_activity and sact.sub_type = 'Login'
    where sact.date_time >= start_date and sact.date_time <= end_date
    group by sg.name
          ) as g_logins on g_logins.name = sg.name
          left join (
            select sg.name,count(sg.id) as doc_count from tbl_statistic_group as sg
          join tbl_statistic_activity as sact on sact.id = sg.id_activity
          join tbl_statistic_detail as sdet on sdet.id_activity = sact.id and sdet.tag = 'Original Format' and sdet.data <> 'video' and sdet.data <> 'mp4' and sdet.data <> 'wmv'
    where sact.date_time >= start_date and sact.date_time <= end_date
    group by sg.name
          ) as g_docs on g_docs.name = sg.name
          left join (
            select sg.name,count(sg.id) as video_count from tbl_statistic_group as sg
          join tbl_statistic_activity as sact on sact.id = sg.id_activity
          join tbl_statistic_detail as sdet on sdet.id_activity = sact.id and sdet.tag = 'Original Format' and (sdet.data = 'video' or sdet.data = 'mp4' or sdet.data = 'wmv')
    where sact.date_time >= start_date and sact.date_time <= end_date
    group by sg.name
          ) as g_videos on g_videos.name = sg.name
  where sact.date_time >= start_date and sact.date_time <= end_date;
