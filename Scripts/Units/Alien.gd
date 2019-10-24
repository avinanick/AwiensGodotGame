extends Destructible

class_name Alien

# I think the accuracy logic should be somewhere in here? or in the bullet
export var point_value: int = 1
export var speed: float = 5
onready var main_scene = get_node("/root/MainScene")
var alien_direction: Vector3

# Called when the node enters the scene tree for the first time.
func _ready():
	add_to_group("Aliens")
	pass # Replace with function body.

func take_damage(var amount: int):
	health -= amount
	if health <= 0:
		main_scene.enemy_destroyed(point_value)
		destroy_self()
# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func initialize_direction():
	pass
	
# Later this will be updated to do a sort of warp out
func retreat():
	self.queue_free()
