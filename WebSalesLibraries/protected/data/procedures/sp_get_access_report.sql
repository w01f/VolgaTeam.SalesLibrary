DROP PROCEDURE IF EXISTS sp_get_access_report;
CREATE PROCEDURE sp_get_access_report(in start_date datetime,in end_date datetime)
  select
    final.group_name as name,
    ifnull(count(final.activity),0) as user_count,
    group_concat(distinct case when final.activity=1
      then concat(final.first_name,' ',final.last_name)
                          else null
                          end
                 separator ', ') as active_names,
    ifnull(sum(case when final.activity=1 then 1 else 0 end),0) as active_count,
    group_concat(distinct case when final.activity=0
      then concat(final.first_name,' ',final.last_name)
                          else null
                          end
                 separator ', ') as inactive_names,
    ifnull(sum(case when final.activity=1 then 0 else 1 end),0) as inactive_count
  from (select
          united.group_name,
          united.first_name,
          united.last_name,
          max(united.activity) as activity
        from (select
                su.first_name as first_name,
                su.last_name as last_name,
                sg.name as group_name,
                1 as activity
              from tbl_statistic_activity sact
                join tbl_statistic_user su on su.id_activity = sact.id
                join tbl_statistic_group sg on sg.id_activity = sact.id
              where sact.date_time >= start_date and sact.date_time <= end_date
              group by sg.name, su.first_name, su.last_name
              union
              select
                u.first_name as first_name,
                u.last_name as last_name,
                g.name as group_name,
                0 as activity
              from tbl_user u
                join tbl_user_group ug on ug.id_user = u.id
                join tbl_group g on g.id = ug.id_group
              group by g.name, u.first_name, u.last_name) united
        group by united.group_name, united.first_name, united.last_name
       ) final
  group by final.group_name;