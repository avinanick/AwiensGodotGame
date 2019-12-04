extends TextureRect
class_name alien_attacker_icon

var alien_icons := [preload("res://Resources/Materials/Icons/alienmissile.png"),
	preload("res://Resources/Materials/Icons/aliendrone.png"),
	preload("res://Resources/Materials/Icons/alienfighter.png"),
	preload("res://Resources/Materials/Icons/alienbomber.png")]

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.

func assign_icon(var enemy_type: String):
	# Assign the appropriate icon based on the required enemy type
	match enemy_type:
		"AlienMissile":
			self.texture = alien_icons[0]
		"AlienDrone":
			self.texture = alien_icons[1]
		"AlienFighter":
			self.texture = alien_icons[2]
		"AlienBomber":
			self.texture = alien_icons[3]
