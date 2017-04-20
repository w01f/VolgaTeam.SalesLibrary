DROP PROCEDURE IF EXISTS sp_get_trending_links;
CREATE PROCEDURE sp_get_trending_links(
in start_date datetime,
in end_date datetime,
in formats varchar(1024),
in libraries varchar(1024),
in max_links int,
in thumbnail_mode varchar(32))
    select
      l.id as link_id,
      l.name as link_name,
      l.file_name as file_name,
      l.search_format as link_format,
      lib.id as lib_id,
      lib.name as lib_name,
      count(s_a.id) as link_views,
      case when thumbnail_mode = 'top'
        then (case
              when l.original_format='jpeg' or l.original_format='gif' or l.original_format='png' then
                l.file_relative_path
              when l.original_format='video' then
                (select pv.relative_path from tbl_preview pv where pv.id_container=l.id_preview and pv.type='mp4 thumb' order by pv.relative_path limit 1)
              when l.original_format='ppt' or l.original_format='doc' or l.original_format='pdf' then
                (select pv.relative_path from tbl_preview pv where pv.id_container=l.id_preview and pv.type='png_phone' order by pv.relative_path limit 1)
              when l.original_format='link bundle' then
                (select pv.relative_path from tbl_preview pv join tbl_link child_link on child_link.id_preview=pv.id_container join tbl_link_bundle lb on lb.id_link=child_link.id where lb.id_bundle=l.id and lb.use_as_thumbnail=1 and (pv.type='png_phone' or pv.type='mp4 thumb') order by pv.relative_path limit 1)
              end)
      else (case
            when l.original_format='jpeg' or l.original_format='gif' or l.original_format='png' then
              l.file_relative_path
            when l.original_format='video' then
              (select pv.relative_path from tbl_preview pv where pv.id_container=l.id_preview and pv.type='mp4 thumb' order by rand() limit 1)
            when l.original_format='ppt' or l.original_format='doc' or l.original_format='pdf' then
              (select pv.relative_path from tbl_preview pv where pv.id_container=l.id_preview and pv.type='png_phone' order by rand() limit 1)
            when l.original_format='link bundle' then
              (select pv.relative_path from tbl_preview pv join tbl_link child_link on child_link.id_preview=pv.id_container join tbl_link_bundle lb on lb.id_link=child_link.id where lb.id_bundle=l.id and lb.use_as_thumbnail=1 and (pv.type='png_phone' or pv.type='mp4 thumb') order by rand() limit 1)
            end)
      end as thumbnail
    from tbl_link l
      join tbl_library lib on lib.id = l.id_library
      join tbl_statistic_link s_l on s_l.id_link = l.id
      join tbl_statistic_activity s_a on s_a.id = s_l.id_activity and s_a.date_time>=start_date and s_a.date_time<=end_date
    where find_in_set(l.search_format,formats) and (libraries is null or find_in_set(lib.name,libraries))
    group by l.id, l.name, l.file_name, l.search_format, lib.name
    order by link_views desc
    limit max_links;