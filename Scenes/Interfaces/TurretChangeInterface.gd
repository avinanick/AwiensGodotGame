extends MarginContainer
class_name TurretChanger

onready var flak_cannon_button = get_node("NinePatchRect/VBoxContainer/FlakCannonButton")
onready var main_scene := get_node("/root/MainScene") as GameManager

var flak_cannon_scene = preload("res://Scenes/Units/FlakCannon.tscn")
var chaingun_scene = preload("res://Scenes/Units/ProtoGun.tscn")

# Called when the node enters the scene tree for the first time.
func _ready():
	flak_cannon_button.disabled = true
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func update_buttons():
	if Global.unlocks["flak_cannon"]:
		flak_cannon_button.disabled = false

func _on_ChaingunButton_button_up():
	main_scene.replace_turret(chaingun_scene)


func _on_FlakCannonButton_button_up():
	main_scene.replace_turret(flak_cannon_scene)
