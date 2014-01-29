DROP PROCEDURE IF EXISTS `sp_get_quiz_pass_user_report`;
CREATE PROCEDURE `sp_get_quiz_pass_user_report` (in start_date datetime,in end_date datetime)
select
  max(sact.date_time) as quiz_pass_date,
  su.first_name,
  su.last_name,
  group_concat(distinct g.name separator ',') as group_name,
  sdet.data as quiz_name,
  quiz_stat.try_count as quiz_try_count
from tbl_statistic_activity sact
  join tbl_statistic_user su on su.id_activity = sact.id
  join tbl_statistic_detail sdet on sdet.id_activity = sact.id and sdet.tag = 'Name'
  left join (
      select
          count(sact.date_time) as try_count,
          sdet.data as quiz_name,
          su.login as user_login
      from tbl_statistic_activity sact
        join tbl_statistic_user su on su.id_activity = sact.id
        join tbl_statistic_detail sdet on sdet.id_activity = sact.id and sdet.tag = 'Name'
      where sact.type = 'Quizzes'
            and sact.sub_type='Quiz Finished'
            and sact.date_time >= start_date and sact.date_time <= end_date
      group by sdet.data, su.login
    ) quiz_stat on quiz_stat.quiz_name = sdet.data and su.login = quiz_stat.user_login
  join tbl_user u on u.login = su.login
  left join tbl_user_group ug on ug.id_user = u.id
  left join tbl_group g on g.id = ug.id_group
where sact.type = 'Quizzes'
      and sact.sub_type='Quiz Passed'
      and sact.date_time >= start_date and sact.date_time <= end_date
group by
  su.first_name,
  su.last_name,
  sdet.data,
  quiz_stat.try_count;
