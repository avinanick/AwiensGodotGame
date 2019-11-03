extends MarginContainer

onready var main_scene := get_node("/root/MainScene") as GameManager

var flak_cannon_scene = preload("res://Scenes/Units/FlakCannon.tscn")
var chaingun_scene = preload("res://Scenes/Units/ProtoGun.tscn")

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func _on_ContinueButton_button_up():
	main_scene.start_level_preparation()
	

func turret_selected(var turret_type: String, var turret_location: String):
	match turret_type:
		"Chaingun":
			main_scene.replace_turret(chaingun_scene, turret_location)
		"Flak Cannon":
			main_scene.replace_turret(flak_cannon_scene, turret_location)
			
func update_turret_options():
	get_node("Panel/TitleElementSeparator/TurretsVSeparation/TurretsHSeparationTop/NorthTurretList/TurretSelectionDropdown").update_turret_list()
	get_node("Panel/TitleElementSeparator/TurretsVSeparation/TurretsHSeparationTop/SouthTurretList/TurretSelectionDropdown").update_turret_list()
	get_node("Panel/TitleElementSeparator/TurretsVSeparation/TurretsHSeparationBot/WestTurretList/TurretSelectionDropdown").update_turret_list()
	get_node("Panel/TitleElementSeparator/TurretsVSeparation/TurretsHSeparationBot/EastTurretList/TurretSelectionDropdown").update_turret_list()
	