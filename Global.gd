extends Node

var current_level: int = 1
var total_points: int = 0
var unlocks := {
	"flak_cannon": false,
	"shaped_blast": false
}

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func reset_all():
	current_level = 1
	total_points = 0
	reset_unlocks()

func reset_unlocks():
	for key in unlocks:
		unlocks[key] = false
