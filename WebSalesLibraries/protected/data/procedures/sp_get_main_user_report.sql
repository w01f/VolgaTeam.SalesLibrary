DROP PROCEDURE IF EXISTS `sp_get_main_user_report`;
CREATE PROCEDURE `sp_get_main_user_report`(in start_date datetime,in end_date datetime)
select su.first_name,
        su.last_name,
        g.name as group_name,
        (select count(id) from tbl_user_group where id_group = g.id) as group_user_count,
        ifnull(u_totals.total_count,0) as user_activity_total,
        ifnull(g_totals.total_count,0) as group_activity_total,
        ifnull(u_logins.log_count,0) as user_activity_login,
        ifnull(g_logins.log_count,0) as group_activity_login,
        ifnull(u_docs.doc_count,0) as user_activity_doc,
        ifnull(g_docs.doc_count,0) as group_activity_doc,
        ifnull(u_videos.video_count,0) as user_activity_video,
        ifnull(g_videos.video_count,0) as group_activity_video
  from tbl_statistic_activity as sact
          join tbl_statistic_user as su on su.id_activity = sact.id
          join tbl_user as u on u.login = su.login
          left join (select su.login,count(su.id) as total_count from tbl_statistic_user as su
              join tbl_statistic_activity as sact on sact.id = su.id_activity
    where sact.date_time >= start_date and sact.date_time <= end_date
    group by su.login) as u_totals on u_totals.login = u.login
          left join (
            select su.login,count(su.id) as log_count from tbl_statistic_user as su
          join tbl_statistic_activity as sact on sact.id = su.id_activity and sact.sub_type = 'Login'
    where sact.date_time >= start_date and sact.date_time <= end_date
    group by su.login
          ) as u_logins on u_logins.login = u.login
          left join (
            select su.login,count(su.id) as doc_count from tbl_statistic_user as su
          join tbl_statistic_activity as sact on sact.id = su.id_activity
          join tbl_statistic_detail as sdet on sdet.id_activity = sact.id and sdet.tag = 'Original Format' and sdet.data <> 'video' and sdet.data <> 'mp4' and sdet.data <> 'wmv'
    where sact.date_time >= start_date and sact.date_time <= end_date
    group by su.login
          ) as u_docs on u_docs.login = u.login
          left join (
            select su.login,count(su.id) as video_count from tbl_statistic_user as su
          join tbl_statistic_activity as sact on sact.id = su.id_activity
          join tbl_statistic_detail as sdet on sdet.id_activity = sact.id and sdet.tag = 'Original Format' and (sdet.data = 'video' or sdet.data = 'mp4' or sdet.data = 'wmv')
    where sact.date_time >= start_date and sact.date_time <= end_date
    group by su.login
          ) as u_videos on u_videos.login = u.login
          left join tbl_user_group as ug on ug.id_user = u.id
          left join tbl_group as g on g.id = ug.id_group
          left join (select sg.name,count(sg.id) as total_count from tbl_statistic_group as sg
              join tbl_statistic_activity as sact on sact.id = sg.id_activity
    where sact.date_time >= start_date and sact.date_time <= end_date
    group by sg.name) as g_totals on g_totals.name = g.name
          left join (
            select sg.name,count(sg.id) as log_count from tbl_statistic_group as sg
          join tbl_statistic_activity as sact on sact.id = sg.id_activity and sact.sub_type = 'Login'
    where sact.date_time >= start_date and sact.date_time <= end_date
    group by sg.name
          ) as g_logins on g_logins.name = g.name
          left join (
            select sg.name,count(sg.id) as doc_count from tbl_statistic_group as sg
          join tbl_statistic_activity as sact on sact.id = sg.id_activity
          join tbl_statistic_detail as sdet on sdet.id_activity = sact.id and sdet.tag = 'Original Format' and sdet.data <> 'video' and sdet.data <> 'mp4' and sdet.data <> 'wmv'
    where sact.date_time >= start_date and sact.date_time <= end_date
    group by sg.name
          ) as g_docs on g_docs.name = g.name
          left join (
            select sg.name,count(sg.id) as video_count from tbl_statistic_group as sg
          join tbl_statistic_activity as sact on sact.id = sg.id_activity
          join tbl_statistic_detail as sdet on sdet.id_activity = sact.id and sdet.tag = 'Original Format' and (sdet.data = 'video' or sdet.data = 'mp4' or sdet.data = 'wmv')
    where sact.date_time >= start_date and sact.date_time <= end_date
    group by sg.name
          ) as g_videos on g_videos.name = g.name
  where sact.date_time >= start_date and sact.date_time <= end_date
  group by su.first_name,su.last_name,g.name;
