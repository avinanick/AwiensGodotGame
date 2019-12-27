extends MarginContainer

var turret_type_dict := {"Chaingun" : preload("res://Scenes/Units/ProtoGun.tscn"),
	"Flak Cannon" : preload("res://Scenes/Units/FlakCannon.tscn"),
	"Thunder Cannon" : preload("res://Scenes/Units/ThunderCannon.tscn")}

signal North_turret_type_selected
signal South_turret_type_selected
signal East_turret_type_selected
signal West_turret_type_selected

# Called when the node enters the scene tree for the first time.
func _ready():
	pass

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func _on_ContinueButton_button_up():
	get_node("../UpgradesInterface").visible = true
	self.visible = false
	
func make_connections():
	self.connect("North_turret_type_selected", get_node("../Guns/NorthGun"), "replace_turret")
	self.connect("South_turret_type_selected", get_node("../Guns/SouthGun"), "replace_turret")
	self.connect("East_turret_type_selected", get_node("../Guns/EastGun"), "replace_turret")
	self.connect("West_turret_type_selected", get_node("../Guns/WestGun"), "replace_turret")

func turret_selected(var turret_type: String, var turret_location: String):
	print("Selected turret type is ", turret_type)
	emit_signal(str(turret_location, "_turret_type_selected"), self.turret_type_dict[turret_type])
	#match turret_location:
	#	"North":
	#		emit_signal("north_turret_type_selected", self.turret_type_dict[turret_type])
	#	"South":
	#		emit_signal("south_turret_type_selected", self.turret_type_dict[turret_type])
	#	"East":
	#		emit_signal("east_turret_type_selected", self.turret_type_dict[turret_type])
	#	"West":
	#		emit_signal("west_turret_type_selected", self.turret_type_dict[turret_type])
			
func update_turret_options():
	get_node("Panel/TitleElementSeparator/TurretsVSeparation/TurretsHSeparationTop/NorthTurretList/TurretSelectionDropdown").update_turret_list()
	get_node("Panel/TitleElementSeparator/TurretsVSeparation/TurretsHSeparationTop/SouthTurretList/TurretSelectionDropdown").update_turret_list()
	get_node("Panel/TitleElementSeparator/TurretsVSeparation/TurretsHSeparationBot/WestTurretList/TurretSelectionDropdown").update_turret_list()
	get_node("Panel/TitleElementSeparator/TurretsVSeparation/TurretsHSeparationBot/EastTurretList/TurretSelectionDropdown").update_turret_list()
	