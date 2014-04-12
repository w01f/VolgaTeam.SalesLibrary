DROP PROCEDURE IF EXISTS `sp_get_quiz_pass_group_report`;
CREATE PROCEDURE `sp_get_quiz_pass_group_report` (in start_date datetime,in end_date datetime)
select
  g.name as group_name,
  concat(ExtractValue(q.config,'/Quiz/Subtitle'),' - ',q.name,' - ',ExtractValue(q.config,'/Quiz/Date')) as quiz_name,
  tlg.name as top_level_name,
  count(distinct su.login) as user_count
from tbl_statistic_activity sact
  join tbl_statistic_user su on su.id_activity = sact.id
  left join tbl_statistic_detail sdet_i on sdet_i.id_activity = sact.id and sdet_i.tag = 'ID'
  left join tbl_quiz q on q.unique_id = sdet_i.data
  left join tbl_quiz_group qg on qg.id = q.id_group
  left join tbl_quiz_group tlg on tlg.id = qg.id_top_level
  join tbl_user u on u.login = su.login
  left join tbl_user_group ug on ug.id_user = u.id
  left join tbl_group g on g.id = ug.id_group
where sact.type = 'Quizzes'
      and sact.sub_type='Quiz Passed'
      and sact.date_time >= start_date and sact.date_time <= end_date
group by
  g.name,
  q.name;