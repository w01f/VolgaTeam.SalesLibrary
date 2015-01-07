DROP PROCEDURE IF EXISTS sp_get_video_links;
CREATE PROCEDURE sp_get_video_links()
select
  l.file_name as file_name,
  l.name as link_name,
  lcat.group_name as category_groups,
  lcat.tag_name as category_tags,
  replace(l.tags,' ',', ') as keywords,
  l.properties as properties,
  (case when l.format='mp4' then l.file_relative_path else concat('/',prv.relative_path) end) as mp4_path,
  concat('/',thumb.relative_path) as thumb_path,
  lb.name as station,
  l.date_modify as link_date,
  l.file_date as file_date
from tbl_link l
  join tbl_library lb on lb.id = l.id_library
  left join (select cat.id_link, group_concat(distinct cat.category separator ', ') as group_name, group_concat(cat.tag separator ', ') as tag_name from tbl_link_category cat group by cat.id_link) lcat on lcat.id_link = l.id
  left join tbl_preview prv on prv.id_container = l.id_preview and prv.type = 'mp4'
  left join tbl_preview thumb on thumb.id_container = l.id_preview and thumb.type = 'mp4 thumb'
where l.type in (3,4);