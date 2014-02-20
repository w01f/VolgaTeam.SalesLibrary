DROP PROCEDURE IF EXISTS `sp_get_quiz_pass_user_report`;
CREATE PROCEDURE `sp_get_quiz_pass_user_report` (in start_date datetime,in end_date datetime)
select
  max(quiz_results_passed.pass_date) as quiz_pass_date,
  u.first_name,
  u.last_name,
  concat(ExtractValue(q.config,'/Quiz/Subtitle'),' - ',q.name,' - ',ExtractValue(q.config,'/Quiz/Date')) as quiz_name,
  group_concat(distinct g.name separator ',') as group_name,
  quiz_results_all.try_count as quiz_try_count
from tbl_quiz q
  join (select
          qr.id_quiz as id_quiz,
          qr.id_user as id_user,
          qr.quiz_set as quiz_set,
          max(qr.date) as pass_date
        from tbl_quiz_result qr
        where qr.date >= start_date and qr.date <= end_date
        group by
          qr.id_quiz,
          qr.id_user,
          qr.quiz_set
        having min(qr.successful) = 1
        ) quiz_results_passed on quiz_results_passed.id_quiz = q.unique_id
  join (select
          qr.id_quiz as id_quiz,
          qr.id_user as id_user,
          count(distinct qr.quiz_set) as try_count
        from tbl_quiz_result qr
        where qr.date >= start_date and qr.date <= end_date
        group by
          qr.id_quiz,
          qr.id_user
       ) quiz_results_all on quiz_results_all.id_quiz = q.unique_id and quiz_results_all.id_user = quiz_results_passed.id_user
  join tbl_user u on u.id = quiz_results_passed.id_user
  left join tbl_user_group ug on ug.id_user = u.id
  left join tbl_group g on g.id = ug.id_group
where q.name is not null
group by
  u.first_name,
  u.last_name,
  q.name;
