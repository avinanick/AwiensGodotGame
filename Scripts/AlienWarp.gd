extends Spatial

signal enemy_spawn_time

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func initialize_direction(var enemy_type: String):
	match enemy_type:
		"AlienMissile":
			print("pick a random earthling")
		_:
			print("point to the city")

func on_warp_expanded():
	emit_signal("enemy_spawn_time")
