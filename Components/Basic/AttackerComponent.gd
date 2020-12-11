extends "res://Components/Basic/Component.gd"


export var projectile: PackedScene = preload("res://Scenes/Bullet.tscn")
export var projectile_damage: int = 2

signal primary_attack


# Called when the node enters the scene tree for the first time.
func _ready():
	self.connect("primary_attack", self, "fire")
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func fire():
	pass

func process_message(var message: String, var args: Dictionary):
	if message == "fire_primary":
		emit_signal("primary_attack")
