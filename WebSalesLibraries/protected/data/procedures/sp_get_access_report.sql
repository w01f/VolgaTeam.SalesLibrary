DROP PROCEDURE IF EXISTS sp_get_access_report;
CREATE PROCEDURE sp_get_access_report(in start_date datetime,in end_date datetime)
select g.name,
        ifnull(count(ug.id),0) as user_count,
        ifnull(active.user_count,0) as active_count,
        active.user_names as active_names,
        ifnull(inactive.user_count,0) as inactive_count,
        inactive.user_names as inactive_names
  from tbl_group as g
          join tbl_user_group as ug on ug.id_group = g.id
          left join
                    (select g.name,
                            count(distinct u.login) as user_count,
                            group_concat(distinct concat(u.first_name,' ',u.last_name) separator ', ') as user_names
                      from tbl_group as g
                              join tbl_user_group as ug on ug.id_group = g.id
                              join tbl_user as u on u.id = ug.id_user
                              left join tbl_statistic_user as su on su.login = u.login
                              left join tbl_statistic_activity as sact on sact.id = su.id_activity and sact.date_time >= start_date and sact.date_time <= end_date
                      where sact.id is not null
                      group by g.name
                    ) as active on active.name = g.name
          left join
                    (select g.name,
                            count(distinct u.login) as user_count,
                            group_concat(distinct concat(u.first_name,' ',u.last_name) separator ', ') as user_names
                      from tbl_group as g
                              join tbl_user_group as ug on ug.id_group = g.id
                              join tbl_user as u on u.id = ug.id_user
                      where u.login not in (select su.login from tbl_statistic_user as su join tbl_statistic_activity as sact on sact.id = su.id_activity and sact.date_time >= start_date and sact.date_time <= end_date)
                      group by g.name
                    ) as inactive on inactive.name = g.name
  group by g.name;