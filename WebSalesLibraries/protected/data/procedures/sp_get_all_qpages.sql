DROP PROCEDURE IF EXISTS sp_get_all_qpages;
CREATE PROCEDURE sp_get_all_qpages()
select
  q.id as id,
  q.title as title,
  q.is_email as is_email,
  q.create_date as create_date,
  q.expiration_date as expiration_date,
  u.login as login,
  u.first_name as first_name,
  u.last_name as last_name,
  u.email as email,
  group_concat(g.name separator ',') as groups
from tbl_qpage q
  join tbl_user u on u.id = q.id_owner
  left join tbl_user_group ug on ug.id_user = u.id
  left join tbl_group g on g.id = ug.id_group
group by q.id,q.is_email,q.create_date,q.expiration_date,u.login, u.first_name, u.last_name, u.email;