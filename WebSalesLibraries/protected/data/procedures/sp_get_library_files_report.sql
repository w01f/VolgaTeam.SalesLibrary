DROP PROCEDURE IF EXISTS sp_get_library_files_report;
CREATE PROCEDURE sp_get_library_files_report()
  select
    lib.name as library,
    lib.last_update as library_date,
    l.name as link_name,
    l.file_name as file_name,
    l.file_extension as file_type,
    l.format as file_format,
    l.file_date as file_date
  from tbl_library lib
    join tbl_link l on l.id_library = lib.id
    left join tbl_preview p on p.id_container = l.id_preview
  where l.type not in (5,6,8,9)
  group by lib.id, l.id, l.name, l.format;