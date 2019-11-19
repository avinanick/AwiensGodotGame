extends Turret

# Declare member variables here. Examples:
# var a = 2
# var b = "text"

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func fire(start_location : Vector3, start_rotation : Vector3):
	# This should rotate the targetingZone to where the camera is looking and check if there are any enemies in that
	# zone, if the cooldown is ready. TargetingZone can be rotated directly, and will work perfectly
	if is_alive and timer >= fire_cooldown:
		timer = 0
	pass
