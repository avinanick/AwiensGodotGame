extends Destructible

class_name Structure
# Declare member variables here. Examples:
# var a = 2
# var b = "text"
var combatant = false
# Called when the node enters the scene tree for the first time.
func _ready():
	add_to_group("Earthlings")

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass
