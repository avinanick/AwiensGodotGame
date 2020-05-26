extends KinematicBody

class_name Destructible

export var health: int = 10
export var max_health: int = 10
var damage_modifier: float = 1.0
var is_alive: bool = true

signal attack_incoming
signal destructible_destroyed

# Called when the node enters the scene tree for the first time.
func _ready():
	self.connect("attack_incoming", self, "handle_damage")
	
func take_damage(var amount: int):
	emit_signal("attack_incoming")
	
func destroy_self():
	# at some point I'll likely be modifying this to play destroy animations
	emit_signal("destructible_destroyed")
	queue_free()

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func handle_damage(var amount: int):
	health -= (amount * damage_modifier)
	if health <= 0:
		destroy_self()
