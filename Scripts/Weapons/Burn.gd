extends Node

var burn_period: float = 1.0
var burn_timer: float = 0.0
var burn_damage: int = 1

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	burn_timer += delta
	if burn_timer >= burn_period:
		get_parent().take_damage(burn_damage)
		burn_timer = 0.0
