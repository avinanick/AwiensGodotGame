extends Node

var mouse_sensitivity: float = 1.0
var master_volume: float = 25

signal volume_changed

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func change_volume(var new_value):
	master_volume = new_value
	emit_signal("volume_changed", new_value)
