/* Original values for treasure_type=1000:
update ace_world.treasure_death set 
tier = 7, 
loot_quality_mod=0,
unknown_chances=19,
item_chance=100,
item_min_amount=1,
item_max_amount=2,
item_treasure_type_selection_chances=8,
magic_item_chance=100,
magic_item_min_amount=2,
magic_item_max_amount=3,
magic_item_treasure_type_selection_chances=8,
mundane_item_chance=100,
mundane_item_min_amount=1,
mundane_item_max_amount=2,
mundane_item_type_selection_chances=7
where treasure_type = 1000
*/

#Update treasure type for non sanctuary golems to point to some other similar loot treasure type
update weenie_properties_d_i_d as d 
    inner join weenie_properties_string as str on str.object_Id = d.object_id
set d.value = 32
where 
	d.type = 35 
    and d.value = 1000
    and str.value != 'Sanctuary Golem';


update weenie_properties_d_i_d as d 
    inner join weenie_properties_string as str on str.object_Id = d.object_id
set d.value = 32
where 
	d.type = 35 
    and d.value = 998
    and str.value != 'Ruschk Resident';

update treasure_death set 
tier = 8, 
loot_quality_mod=0.9,
unknown_chances=0,
item_chance=0,
item_min_amount=0,
item_max_amount=0,
item_treasure_type_selection_chances=8,
magic_item_chance=100,
magic_item_min_amount=30,
magic_item_max_amount=50,
magic_item_treasure_type_selection_chances=8,
mundane_item_chance=100,
mundane_item_min_amount=1,
mundane_item_max_amount=2,
mundane_item_type_selection_chances=7
where treasure_type = 1000;



update treasure_death set 
tier = 7, 
loot_quality_mod=0.9,
unknown_chances=0,
item_chance=0,
item_min_amount=0,
item_max_amount=0,
item_treasure_type_selection_chances=1,
magic_item_chance=100,
magic_item_min_amount=30,
magic_item_max_amount=50,
magic_item_treasure_type_selection_chances=8,
mundane_item_chance=100,
mundane_item_min_amount=1,
mundane_item_max_amount=2,
mundane_item_type_selection_chances=7
where treasure_type = 998;
