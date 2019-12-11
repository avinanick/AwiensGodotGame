extends Spatial

# Declare member variables here. Examples:
# var a = 2
# var b = "text"

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func sight(var x_rotation: float, var y_rotation: float):
	# This should be called each frame, following the camera using the same values that rotate the camera
	$Armature/Bone/Bone001/Bone002.rotation_degrees.x -= x_rotation
	$Armature/Bone/Bone001.rotation_degrees.y -= y_rotation
	
func reset_sights():
	# This should reset the sights to the base for it's position, which it should get from it's parent
	var position = get_parent().position
	$Armature/Bone/Bone001/Bone002.rotation_degrees.x = -90
	match position:
		"North":
			$Armature/Bone/Bone001.rotation_degrees.y = 180
		"South":
			$Armature/Bone/Bone001.rotation_degrees.y = 0
		"East":
			$Armature/Bone/Bone001.rotation_degrees.y = -90
		"West":
			$Armature/Bone/Bone001.rotation_degrees.y = 90