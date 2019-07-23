extends Spatial

# Declare member variables here. Examples:
# var a = 2
# var b = "text"
var rotation_speed = 1
onready var selected_turret = get_node("/root/MainScene/EastGun")
onready var main_scene = get_node("/root/MainScene")
onready var camera = get_node("Camera")

# Called when the node enters the scene tree for the first time.
# This all seems to be working right now, I'd like to change up the way it looks to interact with turret objects better
func _ready():
	Input.set_mouse_mode(Input.MOUSE_MODE_CAPTURED)

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	# This is going in the process so I can use the input map instead of the event
	# It would be nice if I could change this to get their positions at the beginning and 
	# move based on that
	# Add some logic to each of these to check if the node exists first
	if main_scene.game_state == main_scene.game_states.running:
		if Input.is_action_just_pressed("select_one"):
			# select the north gun (0,3.5,13) (0,0,0)
			selected_turret = get_node("/root/MainScene/NorthGun")
			translation = Vector3(0,2.2,13)
			rotation_degrees = Vector3(0,180,0)
			print("switch to turret one")
		if Input.is_action_just_pressed("select_two"):
			# select the south gun (0,3.5,-13)(0,180,0)
			selected_turret = get_node("/root/MainScene/SouthGun")
			translation = Vector3(0,2.2,-13)
			rotation_degrees = Vector3(0,0,0)
			print("switch to turret two")
		if Input.is_action_just_pressed("select_three"):
			# select the east gun (13,3.5,0)(0,-90,0)
			selected_turret = get_node("/root/MainScene/EastGun")
			translation = Vector3(13,2.2,0)
			rotation_degrees = Vector3(0,-90,0)
			print("switch to turret three")
		if Input.is_action_just_pressed("select_four"):
			# select the west gun (-13,3.5,0)(0,90,0)
			selected_turret = get_node("/root/MainScene/WestGun")
			translation = Vector3(-13,2.2,0)
			rotation_degrees = Vector3(0,90,0)
			print("switch to turret four")
		if Input.is_action_pressed("fire_one"):
			# I need to modify this to accurately spawn at the camera position instead of base player
			selected_turret.fire(camera.get_global_transform().origin, rotation) # crashes if the targetted turret was destroyed
	
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
