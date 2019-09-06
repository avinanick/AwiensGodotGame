extends Spatial

class_name EnemySpawner

# I'll likely modify these to spawn from the outer edges of a circle
export var spawn_radius: float = 40
export var spawn_period:float = 2
export var level_difficulty: int = 1

var enemy_scene = preload("res://Scenes/Units/AlienMissile(PH).tscn")
var timer = 0
onready var main_scene = get_node("/root/MainScene")

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if main_scene.game_state == main_scene.game_states.running:
		spawn_enemy(delta)
	pass

func spawn_enemy(delta):
	timer += delta
	if timer >= spawn_period:
		timer = 0
	pass
