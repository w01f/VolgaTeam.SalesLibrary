DROP PROCEDURE IF EXISTS sp_get_file_activity_report;
CREATE PROCEDURE sp_get_file_activity_report(in start_date datetime,in end_date datetime)
    select
      'library link' as file_type,
      l.file_name as file_name,
      l.file_name as file_link,
      l.id_preview as file_detail,
      case when l.original_format='folder' or l.original_format='other' then 'other' else l.file_extension end as file_extension,
      count(slink.id_link) as action_count,
      lib.name as lib_name,
      g.name as group_name
    from tbl_statistic_link slink
      join tbl_link l on l.id = slink.id_link
      join tbl_library lib on lib.id = l.id_library
      join tbl_statistic_activity as sact on sact.id = slink.id_activity
      join tbl_statistic_user as su on su.id_activity = sact.id
      join tbl_user as u on u.login = su.login
      left join tbl_user_group as ug on ug.id_user = u.id
      left join tbl_group as g on g.id = ug.id_group
    where
      l.file_name is not null and
      l.file_name <> '' and
      sact.date_time >= start_date and
      sact.date_time <= end_date
    group by l.id, l.file_name, g.name
    union
    select
      'qpage' as file_type,
      qp.title as file_name,
      substring(sd.data, locate('"url":', sd.data)+7, locate('"', substring(sd.data, locate('"url":', sd.data) + 7)) - 1) as file_link,
      substring(sd.data, locate('"url":', sd.data)+7, locate('"', substring(sd.data, locate('"url":', sd.data) + 7)) - 1) as file_detail,
      '' as file_extension,
      count(qpage.id_qpage) as action_count,
      group_concat(distinct lb.name separator ', ')  as lib_name,
      g.name as group_name
    from tbl_statistic_qpage qpage
      join tbl_qpage qp on qp.id = qpage.id_qpage
      join tbl_statistic_activity as sact on sact.id = qpage.id_activity
      join tbl_statistic_data sd on sd.id_activity = sact.id
      join tbl_statistic_user as su on su.id_activity = sact.id
      join tbl_user as u on u.login = su.login
      left join tbl_user_group as ug on ug.id_user = u.id
      left join tbl_group as g on g.id = ug.id_group
      left join tbl_link_qpage l_q on l_q.id_qpage = qp.id
      left join tbl_link l on l.id = l_q.id_link
      left join tbl_library lb on lb.id = l.id_library
    where
      sact.date_time >= start_date and
      sact.date_time <= end_date
    group by qp.id
    union
    select
      'secure_link' as file_type,
      substring(sd.data, locate('"url":', sd.data)+7, locate('"', substring(sd.data, locate('"url":', sd.data) + 7)) - 1) as file_name,
      substring(sd.data, locate('"url":', sd.data)+7, locate('"', substring(sd.data, locate('"url":', sd.data) + 7)) - 1) as file_link,
      '' as file_detail,
      '' as file_extension,
      count(sd.data) as action_count,
      'SECURE LINK'  as lib_name,
      g.name as group_name
    from tbl_statistic_data sd
      join tbl_statistic_activity as sact on sact.id = sd.id_activity
      join tbl_statistic_user as su on su.id_activity = sact.id
      join tbl_user as u on u.login = su.login
      left join tbl_user_group as ug on ug.id_user = u.id
      left join tbl_group as g on g.id = ug.id_group
    where
      sact.type = 'Secure Links' and
      sd.data like '%"url":%' and
      sact.date_time >= start_date and
      sact.date_time <= end_date
    group by file_name, g.name;