extends Spatial

var turret_type_dict := {"Chaingun" : preload("res://Scenes/Units/ProtoGun.tscn"),
	"Flak Cannon" : preload("res://Scenes/Units/FlakCannon.tscn"),
	"Thunder Cannon" : preload("res://Scenes/Units/ThunderCannon.tscn")}

onready var shield_scene = preload("res://Scenes/EnergyShield.tscn")
var shield = null

# Called when the node enters the scene tree for the first time.
func _ready():
	pass

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func activate_shield():
	if self.get_child_count() > 0:
		print("Activating turret shield")
		var new_shield = shield_scene.instance()
		self.add_child(new_shield)
		new_shield.translation = Vector3(0,0,0)
		shield = new_shield
	else:
		print("No gun to shield for ", self.name)

func fire_weapon(start_location : Vector3, start_rotation : Vector3):
	var child_turret = get_child(0)
	if child_turret and is_instance_valid(child_turret):
		child_turret.fire(start_location, start_rotation)
		
func get_shield():
	return shield
	
func load_data():
	if "North" in self.name:
		replace_turret(Global.north_turret_type)
	if "East" in self.name:
		replace_turret(Global.east_turret_type)
	if "West" in self.name:
		replace_turret(Global.west_turret_type)
	if "South" in self.name:
		replace_turret(Global.south_turret_type)
	get_child(0).load_data()
	validate_upgrades()
	
func make_connections():
	get_parent().get_parent().connect("start_transition", self, "validate_upgrades")
	get_parent().get_parent().connect("load_data", self, "load_data")
	get_child(0).make_connections()
	
func rebuild_turret():
	pass

func replace_turret(var new_type: String):
	var new_turret_type = self.turret_type_dict[new_type]
	var selected_turret = get_child(0)
	if selected_turret:
		var health: int = selected_turret.health
		var position: Vector3 = Vector3(0,0,0) # Since the gun will be positioned at this nodes location
		var node_name: String = selected_turret.get_name()
		var gun_position: String = selected_turret.position
		selected_turret.queue_free() # Will this maybe cause issues? perhaps it should be free instead
		var new_turret = new_turret_type.instance()
		new_turret.translation = position
		new_turret.health = health
		new_turret.set_name(node_name)
		new_turret.position = gun_position
		self.add_child(new_turret)
		new_turret.make_connections()
		print(new_turret.get_path())
		print("Turret replaced")
	else:
		print("Selected turret not found")
		
func reset_sights():
	get_child(0).reset_sights()
		
func sight(var x_rotation: float, var y_rotation: float):
	var turret = get_child(0)
	if turret and is_instance_valid(turret):
		turret.sight(x_rotation, y_rotation)
		
func validate_upgrades():
	if Global.upgrade_unlocks["Energy Shields"] and not self.shield:
		activate_shield()