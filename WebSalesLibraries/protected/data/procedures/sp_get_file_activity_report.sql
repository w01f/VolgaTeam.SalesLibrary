DROP PROCEDURE IF EXISTS sp_get_file_activity_report_legacy;
CREATE PROCEDURE sp_get_file_activity_report_legacy(in start_date datetime,in end_date datetime)
  select
    case when sdet.data like '%"file": %'
      then substring(sdet.data, locate('"file": ', sdet.data)+9, locate('"', substring(sdet.data, locate('"file": ', sdet.data) + 9)) - 1)
    else substring(sdet.data, locate('"file":', sdet.data)+8, locate('"', substring(sdet.data, locate('"file":', sdet.data) + 8)) - 1)
    end as file_name,
    count(sdet.data) as action_count,
    g.name as group_name
  from tbl_statistic_data sdet
    join tbl_statistic_activity as sact on sact.id = sdet.id_activity
    join tbl_statistic_user as su on su.id_activity = sact.id
    join tbl_user as u on u.login = su.login
    left join tbl_user_group as ug on ug.id_user = u.id
    left join tbl_group as g on g.id = ug.id_group
  where
    sdet.data like '%"file":%' and
    sact.date_time >= start_date and
    sact.date_time <= end_date
  group by file_name, g.name
  union
  select
    case when sdet.data like '%"url": %'
      then substring(sdet.data, locate('"url": ', sdet.data)+8, locate('"', substring(sdet.data, locate('"url": ', sdet.data) + 8)) - 1)
    else substring(sdet.data, locate('"url":', sdet.data)+7, locate('"', substring(sdet.data, locate('"url":', sdet.data) + 7)) - 1)
    end as file_name,
    count(sdet.data) as action_count,
    g.name as group_name
  from tbl_statistic_data sdet
    join tbl_statistic_activity as sact on sact.id = sdet.id_activity
    join tbl_statistic_user as su on su.id_activity = sact.id
    join tbl_user as u on u.login = su.login
    left join tbl_user_group as ug on ug.id_user = u.id
    left join tbl_group as g on g.id = ug.id_group
  where
    sdet.data like '%"url":%' and
    sact.date_time >= start_date and
    sact.date_time <= end_date
  group by file_name, g.name;
