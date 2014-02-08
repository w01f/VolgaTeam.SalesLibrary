DROP PROCEDURE IF EXISTS `sp_get_quiz_pass_group_report`;
CREATE PROCEDURE `sp_get_quiz_pass_group_report` (in start_date datetime,in end_date datetime)
select
  g.name as group_name,
  sdet.data as quiz_name,
  count(distinct su.login) as user_count
from tbl_statistic_activity sact
  join tbl_statistic_user su on su.id_activity = sact.id
  join tbl_statistic_detail sdet on sdet.id_activity = sact.id and sdet.tag = 'Name'
  join tbl_user u on u.login = su.login
  left join tbl_user_group ug on ug.id_user = u.id
  left join tbl_group g on g.id = ug.id_group
where sact.type = 'Quizzes'
      and sact.sub_type='Quiz Passed'
      and sact.date_time >= start_date and sact.date_time <= end_date
group by
  g.name,
  sdet.data;