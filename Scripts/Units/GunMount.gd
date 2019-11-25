extends Spatial

onready var shield_scene = preload("res://Scenes/EnergyShield.tscn")
var shield = null

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func activate_shield():
	var new_shield = shield_scene.instance()
	self.add_child(new_shield)
	new_shield.translation = Vector3(0,0,0)
	shield = new_shield

func fire_weapon(start_location : Vector3, start_rotation : Vector3):
	var child_turret = get_child(0)
	if child_turret and is_instance_valid(child_turret):
		child_turret.fire(start_location, start_rotation)
		
func get_shield():
	return shield

func replace_turret(var new_type):
	var selected_turret = get_child(0)
	if selected_turret:
		var health: int = selected_turret.health
		var position: Vector3 = Vector3(0,0,0) # Since the gun will be positioned at this nodes location
		var node_name: String = selected_turret.get_name()
		var gun_position: String = selected_turret.position
		selected_turret.queue_free() # Will this maybe cause issues? perhaps it should be free instead
		var new_turret = new_type.instance()
		new_turret.translation = position
		new_turret.health = health
		new_turret.set_name(node_name)
		new_turret.position = gun_position
		self.add_child(new_turret)
		print(new_turret.get_path())
		print("Turret replaced")
	else:
		print("Selected turret not found")