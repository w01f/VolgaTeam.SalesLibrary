DROP PROCEDURE IF EXISTS sp_get_all_qpages;
CREATE PROCEDURE sp_get_all_qpages(in start_date datetime,in end_date datetime,in filter_by_view_date boolean)
select
  q.id as id,
  q.title as title,
  q.is_email as is_email,
  q.create_date as create_date,
  q.expiration_date as expiration_date,
  q.restricted as restricted,
  q.pin_code as pin_code,
  u.login as login,
  u.first_name as first_name,
  u.last_name as last_name,
  u.email as email,
  group_concat(g.name separator ',') as groups,
  views.total_views as total_views
from tbl_qpage q
  join tbl_user u on u.id = q.id_owner
  left join tbl_user_group ug on ug.id_user = u.id
  left join tbl_group g on g.id = ug.id_group
  left join (
              select
                s_q.id_qpage as id,
                count(s_q.id) as total_views,
                max(sa.date_time) as last_view_date
              from tbl_statistic_qpage s_q
                join tbl_statistic_activity sa on sa.id = s_q.id_activity
              group by s_q.id_qpage)
            views on views.id = q.id
where
  if(filter_by_view_date,views.last_view_date,q.create_date) >= start_date and
  if(filter_by_view_date,views.last_view_date,q.create_date) <= end_date
group by q.id,q.is_email,q.create_date,q.expiration_date,u.login, u.first_name, u.last_name, u.email, views.total_views;