DROP PROCEDURE IF EXISTS `sp_get_main_group_report`;
CREATE PROCEDURE `sp_get_main_group_report`(in start_date datetime,in end_date datetime)
select distinct(g.name),
        ifnull(g_totals.total_count,0) as totals,
        ifnull(g_logins.log_count,0) as logins,
        ifnull(g_docs.doc_count,0) as docs,
        ifnull(g_videos.video_count,0) as videos
  from tbl_group g
          left join (
            select sg.name,count(sact.id) as total_count from tbl_statistic_group as sg
                  join tbl_statistic_activity as sact on sact.id = sg.id_activity
            where sact.date_time >= start_date and sact.date_time <= end_date
            group by sg.name
            ) as g_totals on g_totals.name = g.name
          left join (
            select sg.name,count(sg.id) as log_count from tbl_statistic_group as sg
                  join tbl_statistic_activity as sact on sact.id = sg.id_activity and sact.sub_type = 'Login'
            where sact.date_time >= start_date and sact.date_time <= end_date
            group by sg.name
            ) as g_logins on g_logins.name = g.name
          left join (
            select sg.name,count(sg.id) as doc_count from tbl_statistic_group as sg
                  join tbl_statistic_activity as sact on sact.id = sg.id_activity
                  join tbl_statistic_data as sdet on sdet.id_activity = sact.id and sdet.data like '%"originalFormat": %' and sdet.data not like '%"originalFormat": "video"%' and sdet.data not like '%"originalFormat": "wmv"%' and sdet.data not like '%"originalFormat": "mp4"%'
            where sact.date_time >= start_date and sact.date_time <= end_date
            group by sg.name
            ) as g_docs on g_docs.name = g.name
          left join (
            select sg.name,count(sg.id) as video_count from tbl_statistic_group as sg
                  join tbl_statistic_activity as sact on sact.id = sg.id_activity
                  join tbl_statistic_data as sdet on sdet.id_activity = sact.id and sdet.data like '%"originalFormat": %' and (sdet.data like '%"originalFormat": "video"%' or sdet.data like '%"originalFormat": "wmv"%' or sdet.data like '%"originalFormat": "mp4"%')
            where sact.date_time >= start_date and sact.date_time <= end_date
            group by sg.name
            ) as g_videos on g_videos.name = g.name;