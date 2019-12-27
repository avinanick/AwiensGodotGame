extends Spatial

class_name EnemySpawner

# I'll likely modify these to spawn from the outer edges of a circle
export var spawn_radius: float = 40
export var base_spawn_period:float = 2
export var level_difficulty: int = 1
var spawn_period: float = 2
var spawning: bool = true

var enemy_index: int = 0
var timer = 0
var enemy_scenes := [preload("res://Scenes/Units/AlienMissile(PH).tscn"),
	preload("res://Scenes/Units/AlienDrone.tscn"),
	preload("res://Scenes/Units/AlienFighter.tscn"),
	preload("res://Scenes/Units/AlienBomber.tscn")]
var enemy_types := ["AlienMissile",
	"AlienDrone",
	"AlienFighter",
	"AlienBomber"]
var base_spawn_periods := [3, 5, 7, 11]
var min_level_requirement := [1, 1, 3, 5]

signal type_chosen

# Called when the node enters the scene tree for the first time.
func _ready():
	randomize()
	add_to_group("Spawners")

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if spawning:
		spawn_enemy(delta)
		
func end_level():
	print("level ended")
	spawning = false
	self.set_process(false)
	
func make_connections():
	self.connect("type_chosen", get_node("../EnemyWarningInterface"), "add_icon")
	self.connect("type_chosen", get_node("../Victory_interface"), "add_kill_icon")
	get_parent().connect("player_victory", self, "end_level")
	get_parent().connect("player_defeat", self, "end_level")
	get_parent().connect("start_transition", self, "on_start_transition")
	self.randomize_spawn()

func spawn_enemy(delta):
	timer += delta
	if timer >= spawn_period:
		timer = 0
		var spawn_angle_radians: float = rand_range(0,2 * PI)
		var x_value_spawn: float = cos(spawn_angle_radians) * spawn_radius
		var z_value_spawn: float = sin(spawn_angle_radians) * spawn_radius
		var new_enemy = enemy_scenes[enemy_index].instance()
		get_parent().add_child(new_enemy)
		new_enemy.translation = Vector3(x_value_spawn, self.transform.origin.y, z_value_spawn)
		new_enemy.warp_in()
		
func on_start_level():
	spawning = true
	self.set_process(true)
		
func on_start_transition():
	self.randomize_spawn()

func update_spawner_difficulty():
	spawn_period = base_spawn_period / pow(1.2, level_difficulty - 1)

func randomize_spawn():
	var strongest_alien_index: int = 0
	for i in range(min_level_requirement.size()):
		print("Checking ", get_parent().level, " against ", min_level_requirement[i], " for index ", i)
		if get_parent().level >= min_level_requirement[i]:
			print("passed check, new index ", i)
			strongest_alien_index = i
	print("Strongest alien index: ", strongest_alien_index)
	var index: int = randi() % (strongest_alien_index + 1)
	enemy_index = index
	base_spawn_period = base_spawn_periods[index]
	emit_signal("type_chosen", enemy_types[index])
	update_spawner_difficulty()