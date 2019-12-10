extends Spatial

class_name EnemySpawner

# I'll likely modify these to spawn from the outer edges of a circle
export var spawn_radius: float = 40
export var base_spawn_period:float = 2
export var level_difficulty: int = 1
var spawn_period: float = 2

var enemy_index: int = 0
var timer = 0
onready var main_scene = get_node("/root/MainScene")
var warp_scene = preload("res://Scenes/Effects/AlienWarp.tscn")
var enemy_scenes := [preload("res://Scenes/Units/AlienMissile(PH).tscn"),
	preload("res://Scenes/Units/AlienDrone.tscn"),
	preload("res://Scenes/Units/AlienFighter.tscn"),
	preload("res://Scenes/Units/AlienBomber.tscn")]
var enemy_types := ["AlienMissile",
	"AlienDrone",
	"AlienFighter",
	"AlienBomber"]
var enemy_warp_scales := [Vector3(0.5,2,0.5),
Vector3(1,1,1),
Vector3(1,2,1),
Vector3(1,1,1)]
var base_spawn_periods := [5, 7, 11, 13]

signal type_chosen

# Called when the node enters the scene tree for the first time.
func _ready():
	randomize()
	add_to_group("Spawners")
	update_spawner_difficulty()
	self.connect("type_chosen", get_node("../EnemyWarningInterface"), "add_icon")
	self.connect("type_chosen", get_node("../Victory_interface"), "add_kill_icon")
	emit_signal("type_chosen", "AlienMissile")

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if main_scene.game_state == main_scene.game_states.running:
		spawn_enemy(delta)

func spawn_enemy(delta):
	timer += delta
	if timer >= spawn_period:
		timer = 0
		var spawn_angle_radians: float = rand_range(0,2 * PI)
		var x_value_spawn: float = cos(spawn_angle_radians) * spawn_radius
		var z_value_spawn: float = sin(spawn_angle_radians) * spawn_radius
		var new_enemy = enemy_scenes[enemy_index].instance()
		main_scene.add_child(new_enemy)
		new_enemy.translation = Vector3(x_value_spawn, self.transform.origin.y, z_value_spawn)
		new_enemy.initialize_direction()
		
func spawn_warp(delta):
	timer += delta
	if timer >= spawn_period:
		timer = 0
		var spawn_angle_radians: float = rand_range(0,2 * PI)
		var x_value_spawn: float = cos(spawn_angle_radians) * spawn_radius
		var z_value_spawn: float = sin(spawn_angle_radians) * spawn_radius
		var new_warp = warp_scene.instance()
		new_warp.scale = enemy_warp_scales[enemy_index]
		new_warp.connect("enemy_spawn_time", self, "spawn_enemy")
		main_scene.add_child(new_warp)
		new_warp.translation = Vector3(x_value_spawn, self.transform.origin.y, z_value_spawn)
		new_warp.initialize_direction(enemy_types[enemy_index])

func update_spawner_difficulty():
	spawn_period = base_spawn_period / pow(1.2, level_difficulty - 1)

func randomize_spawn():
	var index: int = randi() % enemy_scenes.size()
	enemy_index = index
	base_spawn_period = base_spawn_periods[index]
	emit_signal("type_chosen", enemy_types[index])
	update_spawner_difficulty()