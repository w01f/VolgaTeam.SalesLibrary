DROP PROCEDURE IF EXISTS sp_get_inactive_users;
CREATE PROCEDURE sp_get_inactive_users (in start_date datetime,in end_date datetime)
select
	u.id as id,
  	u.login as login,
  	u.first_name as first_name,
  	u.last_name as last_name,
  	group_concat(g.name separator ',') as groups
from tbl_user u
  	left join tbl_user_group ug on ug.id_user = u.id
  	left join tbl_group g on g.id = ug.id_group
where u.login not in (select su.login
                  		from tbl_statistic_user su
                    	join tbl_statistic_activity sact on sact.id = su.id_activity and sact.date_time >= start_date and sact.date_time <= end_date)
group by u.login, u.first_name, u.last_name;