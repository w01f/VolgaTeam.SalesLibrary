DROP PROCEDURE IF EXISTS sp_get_library_files_report;
CREATE PROCEDURE sp_get_library_files_report()
  select
    lib.name as library,
    lib.path as library_path,
    lib.last_update as library_date,
    pg.name as page_name,
    l.id as link_id,
    l.name as link_name,
    l.file_name as file_name,
    l.file_relative_path as file_path,
    case when l.original_format='folder' or l.original_format='other' then 'other' else l.file_extension end as file_type,
    l.original_format as file_format,
    group_concat(distinct l_c.tag separator ', ') as categories,
    l.tags as keywords,
    l.file_date as file_date,
    l.date_add as link_add_date,
    l.date_modify as link_modify_date
  from tbl_library lib
    join tbl_link l on l.id_library = lib.id
    join tbl_folder f on f.id = l.id_folder
    join tbl_page pg on pg.id = f.id_page
    left join tbl_preview p on p.id_container = l.id_preview
    left join tbl_link_category l_c on l_c.id_link = l.id
  where l.type in (0,1,3,4,10,11,12,999)
  group by lib.id, l.id, l.name, l.original_format;