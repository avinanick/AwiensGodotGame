extends "res://Components/Basic/Component.gd"


export var health_points: int = 10

signal incoming_damage


# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func take_damage(var amount: int):
	pass

func process_message(var message: String, var args: Dictionary):
	if message == "deal damage":
		emit_signal("incoming_damage", args["damage"])