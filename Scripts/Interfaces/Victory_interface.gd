extends MarginContainer
class_name VictoryInterface

var kill_icon_scene = preload("res://Scenes/Interfaces/KillCountIcon.tscn")

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func enemy_destroyed(var point_value: int, var alien_name: String):
	# I'm a little iffy about this method, since the enemy is queued to be freed, maybe I should just pass
	# values
	pass

func update_score(points: int):
	var point_label = get_node("VBoxContainer/Points_label_container/NinePatchRect/points_label")
	point_label.text = str(points)
