DROP PROCEDURE IF EXISTS sp_get_inactive_users;
CREATE PROCEDURE sp_get_inactive_users (in start_date datetime,in end_date datetime)
select
	max(whole.id) as id,
	whole.login,
	whole.first_name,
	whole.last_name,
	whole.groups
from (select
				0 as id,
				su.login as login,
				su.first_name as first_name,
				su.last_name as last_name,
				group_concat(distinct sg.name separator ',') as groups,
				1 as activity
			from tbl_statistic_activity sact
				join tbl_statistic_user su on su.id_activity = sact.id
				join tbl_statistic_group sg on sg.id_activity = sact.id
			where sact.date_time >= start_date and sact.date_time <= end_date
			group by su.login, su.first_name, su.last_name
			union
			select
				u.id as id,
				u.login as login,
				u.first_name as first_name,
				u.last_name as last_name,
				group_concat(distinct g.name separator ',') as groups,
				0 as activity
			from tbl_user u
				join tbl_user_group ug on ug.id_user = u.id
				join tbl_group g on g.id = ug.id_group
			group by u.login, u.first_name, u.last_name
		 ) whole
group by whole.login, whole.first_name, whole.last_name, whole.groups
having max(whole.activity) < 1;