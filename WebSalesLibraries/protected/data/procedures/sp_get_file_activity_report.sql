DROP PROCEDURE IF EXISTS sp_get_file_activity_report;
CREATE PROCEDURE sp_get_file_activity_report(in start_date datetime,in end_date datetime)
select
  sdet.data as file_name,
  count(sdet.data) as action_count,
  g.name as group_name
from tbl_statistic_detail sdet
   join tbl_statistic_activity as sact on sact.id = sdet.id_activity
   join tbl_statistic_user as su on su.id_activity = sact.id
   join tbl_user as u on u.login = su.login
   left join tbl_user_group as ug on ug.id_user = u.id
   left join tbl_group as g on g.id = ug.id_group
where sdet.tag = 'File' and sact.date_time >= start_date and sact.date_time <= end_date
group by sdet.data, g.name;