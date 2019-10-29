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

func turret_selected(var turret_type: String, var turret_location: String):
	match turret_type:
		"Chaingun":
			main_scene.replace_turret(chaingun_scene, turret_location)
		"Flak Cannon":
			main_scene.replace_turret(flak_cannon_scene, turret_location)
