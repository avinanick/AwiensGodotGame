extends Spatial

onready var shield_scene = preload("res://Scenes/CityShield.tscn")
var shield = null
var buildings_left: int = 4

signal city_destroyed

# Called when the node enters the scene tree for the first time.
func _ready():
	get_parent().connect("start_level", self, "validate_upgrades")
	self.connect("city_destroyed", get_parent(), "player_defeat")

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta): 
#	pass
	
func activate_shield():
	print("Activating city shield")
	var new_shield = shield_scene.instance()
	self.add_child(new_shield)
	new_shield.translation = Vector3(0,0,0)
	shield = new_shield
	
func building_lost():
	self.buildings_left -= 1
	if self.buildings_left < 1:
		emit_signal("city_destroyed")
		print("loser!")
	
func validate_upgrades():
	if Global.upgrade_unlocks["Energy Shields"] and not self.shield:
		activate_shield()