extends Destructible

class_name Alien

# I think the accuracy logic should be somewhere in here? or in the bullet
export var alien_name: String = ""
export var point_value: int = 1
export var speed: float = 5
onready var main_scene = get_node("/root/MainScene")
var alien_direction: Vector3

signal alien_destroyed

# Called when the node enters the scene tree for the first time.
func _ready():
	add_to_group("Aliens")
	self.connect("alien_destroyed", get_node("/root/MainScene"), "enemy_destroyed")
	self.connect("alien_destroyed", get_node("/root/MainScene/Victory_interface"), "enemy_destroyed")

func take_damage(var amount: int):
	health -= amount
	if health <= 0:
		emit_signal("alien_destroyed", self.point_value, self.alien_name)
		destroy_self()
		
# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func initialize_direction():
	pass
	
# Later this will be updated to do a sort of warp out
func retreat():
	self.queue_free()
