extends Spatial

onready var shield_scene = preload("res://Scenes/CityShield.tscn")
var shield = null

# Called when the node enters the scene tree for the first time.
func _ready():
	get_parent().connect("start_transition", self, "validate_upgrades")
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
	print("Activating city shield")
	var new_shield = shield_scene.instance()
	self.add_child(new_shield)
	new_shield.get_global_tranform().origin = Vector3(0,0,0)
	shield = new_shield
	
func validate_upgrades():
	if Global.upgrade_unlocks["Energy Shields"] and not self.shield:
		activate_shield()