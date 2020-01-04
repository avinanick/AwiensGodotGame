extends Turret
class_name AutoTurret

var target
onready var targeting_range = get_node("TargetingRange")

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func make_connections():
	pass
	
func scan_for_targets():
	pass
