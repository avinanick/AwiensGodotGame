extends KinematicBody

class_name Destructible

# Declare member variables here. Examples:
# var a = 2
# var b = "text"
export var health: int = 10
export var max_health: int = 10
var is_alive: bool = true

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.
	
func take_damage(var amount: int):
	health -= amount
	if health <= 0:
		destroy_self()
	
func destroy_self():
	# at some point I'll likely be modifying this to play destroy animations
	queue_free()

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass
