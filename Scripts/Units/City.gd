extends Spatial

onready var shield_scene = preload("res://Scenes/CityShield.tscn")

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta): 
	# This should really  be moved out of process and into a signal response to cut down on frame by frame
	# work
	if get_child_count() < 1:
		var main_scene = get_node("/root/MainScene")
		main_scene.game_state = main_scene.game_states.defeat
		print("loser!")
		self.queue_free()
	
func activate_shield():
	var new_shield = shield_scene.instance()
	new_shield.translation = Vector3(0,0,0)
	self.add_child(new_shield)