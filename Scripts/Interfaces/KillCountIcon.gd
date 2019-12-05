extends alien_attacker_icon

var kill_number = 0

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func increment_count():
	kill_number += 1
	$CountLabel.text = str("x", kill_number)