extends Camera

# Declare member variables here. Examples:
# var a = 2
# var b = "text"
var rotation_speed = 1
onready var selected_turret = get_node("/root/MainScene/EastGun")

# Called when the node enters the scene tree for the first time.
func _ready():
	Input.set_mouse_mode(Input.MOUSE_MODE_CAPTURED)
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	# This is going in the process so I can use the input map instead of the event
	# would it work just as well in the input func?
	# I think I need to add some logic for getting what turret is selected
	if Input.is_action_just_pressed("select_one"):
		# select the north gun (0,3.5,13) (0,0,0)
		selected_turret = get_node("/root/MainScene/NorthGun")
		translation = Vector3(0,3.5,13)
		rotation_degrees = Vector3(0,180,0)
		print("switch to turret one")
	if Input.is_action_just_pressed("select_two"):
		# select the south gun (0,3.5,-13)(0,180,0)
		selected_turret = get_node("/root/MainScene/SouthGun")
		translation = Vector3(0,3.5,-13)
		rotation_degrees = Vector3(0,0,0)
		print("switch to turret two")
	if Input.is_action_just_pressed("select_three"):
		# select the east gun (13,3.5,0)(0,-90,0)
		selected_turret = get_node("/root/MainScene/EastGun")
		translation = Vector3(13,3.5,0)
		rotation_degrees = Vector3(0,-90,0)
		print("switch to turret three")
	if Input.is_action_just_pressed("select_four"):
		# select the west gun (-13,3.5,0)(0,90,0)
		selected_turret = get_node("/root/MainScene/WestGun")
		translation = Vector3(-13,3.5,0)
		rotation_degrees = Vector3(0,90,0)
		print("switch to turret four")
	if Input.is_action_pressed("fire_one"):
		selected_turret.fire() # crashes if the targetted turret was destroyed
	pass
	
# This I think should be called whenever an input event happens
func _input(event):
	if event is InputEventMouseMotion:
		# I'm using this method instead of the rotations so that I can keep the z rotation at 0
		# plus gives inverted controls, minus gives regular
		if rotation_degrees.x - (event.relative.y * rotation_speed) > 90:
			rotation_degrees.x = 90
		elif rotation_degrees.x - (event.relative.y * rotation_speed) < -90:
			rotation_degrees.x = -90
		else:
			rotation_degrees.x -= (event.relative.y * rotation_speed)
		rotation_degrees.y -= (event.relative.x * rotation_speed)
	
	pass
