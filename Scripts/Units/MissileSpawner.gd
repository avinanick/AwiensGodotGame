extends Spatial

# The x and z ranges should be both +- 20 for targeting concerns

export var x_spawn_range = 40
export var z_spawn_range = 40
export var x_target_range = 20
export var z_target_range = 20
export var spawn_period = 2

var missile_scene = preload("res://Scenes/Units/AlienMissile(PH).tscn")
var timer = 0
var main_scene

# Declare member variables here. Examples:
# var a = 2
# var b = "text"

# Called when the node enters the scene tree for the first time.
func _ready():
	main_scene = get_node("/root/MainScene")

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	timer += delta
	if timer >= spawn_period and main_scene.game_state == main_scene.game_states.running:
		timer = 0
		# spawn a new missile at a random location, with a random target
		var spawn_pos = Vector3(rand_range(-x_spawn_range, x_spawn_range), 
			self.transform.origin.y, rand_range(-z_spawn_range, z_spawn_range))
		var target_pos = Vector3(rand_range(-x_target_range, x_target_range), 
			0, rand_range(-z_target_range, z_target_range))
		var direction_vector = Vector3(target_pos.x - spawn_pos.x, target_pos.y - spawn_pos.y,
			target_pos.z - spawn_pos.z)
		direction_vector = direction_vector.normalized()
		var missile_rotation_radians = Vector3()
		
		var new_missile = missile_scene.instance()
		new_missile.set_name("alien missile")
		get_node("/root/MainScene").add_child(new_missile)
		new_missile.translation = spawn_pos
		new_missile.look_at(target_pos, Vector3(0,1,0))
		new_missile.missile_direction = direction_vector
