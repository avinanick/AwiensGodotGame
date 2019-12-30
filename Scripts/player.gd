extends Spatial
class_name Player

onready var selected_turret = get_node("/root/MainScene/Guns/SouthGun")
onready var main_scene := get_node("/root/MainScene")
onready var camera := get_node("Camera") as Camera

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
		if Input.is_action_just_pressed("select_north"):
			# select the north gun (0,3.5,13) (0,0,0)
			var previous_turret = selected_turret
			selected_turret = get_node("/root/MainScene/Guns/NorthGun")
			if selected_turret.get_child_count() > 0:
				translation = Vector3(0,2.2,12)
				rotation_degrees = Vector3(0,180,0)
				if selected_turret and is_instance_valid(selected_turret):
					selected_turret.reset_sights()
			else:
				selected_turret = previous_turret
		if Input.is_action_just_pressed("select_south"):
			# select the south gun (0,3.5,-13)(0,180,0)
			var previous_turret = selected_turret
			selected_turret = get_node("/root/MainScene/Guns/SouthGun")
			if selected_turret.get_child_count() > 0:
				translation = Vector3(0,2.2,-12)
				rotation_degrees = Vector3(0,0,0)
				if selected_turret and is_instance_valid(selected_turret):
					selected_turret.reset_sights()
			else:
				selected_turret = previous_turret
		if Input.is_action_just_pressed("select_east"):
			# select the east gun (13,3.5,0)(0,-90,0)
			var previous_turret = selected_turret
			selected_turret = get_node("/root/MainScene/Guns/EastGun")
			if selected_turret.get_child_count() > 0:
				translation = Vector3(-12,2.2,0)
				rotation_degrees = Vector3(0,90,0)
				if selected_turret and is_instance_valid(selected_turret):
					selected_turret.reset_sights()
			else:
				selected_turret = previous_turret
		if Input.is_action_just_pressed("select_west"):
			# select the west gun (-13,3.5,0)(0,90,0)
			var previous_turret = selected_turret
			selected_turret = get_node("/root/MainScene/Guns/WestGun")
			if selected_turret.get_child_count() > 0:
				translation = Vector3(12,2.2,0)
				rotation_degrees = Vector3(0,-90,0)
				if selected_turret and is_instance_valid(selected_turret):
					selected_turret.reset_sights()
			else:
				selected_turret = previous_turret
		if Input.is_action_pressed("fire_one"):
			# I need to modify this to accurately spawn at the camera position instead of base player
			# Getting an issue here when I replace the selected turret in the selection screen.
			# This is because selected turret is still true for a short while after a node is freed
			if selected_turret and is_instance_valid(selected_turret):
				selected_turret.fire_weapon(camera.get_global_transform().origin, rotation) 
		if Input.is_action_just_pressed("pause"):
			var manager = get_node("/root/MainScene")
			manager.game_state = manager.game_states.paused
	
# This I think should be called whenever an input event happens
func _input(event):
	if event is InputEventMouseMotion and main_scene.game_state == main_scene.game_states.running:
		# I'm using this method instead of the rotations so that I can keep the z rotation at 0
		# plus gives inverted controls, minus gives regular
		if rotation_degrees.x - (event.relative.y * GameOptions.mouse_sensitivity) > 90:
			rotation_degrees.x = 90
			selected_turret.sight(0, event.relative.x * GameOptions.mouse_sensitivity)
		elif rotation_degrees.x - (event.relative.y * GameOptions.mouse_sensitivity) < -90:
			rotation_degrees.x = -90
			selected_turret.sight(0, event.relative.x * GameOptions.mouse_sensitivity)
		else:
			rotation_degrees.x -= (event.relative.y * GameOptions.mouse_sensitivity)
			selected_turret.sight(event.relative.y * GameOptions.mouse_sensitivity, event.relative.x * GameOptions.mouse_sensitivity)
		rotation_degrees.y -= (event.relative.x * GameOptions.mouse_sensitivity)
		
func activate_camera():
	camera.make_current()
	
