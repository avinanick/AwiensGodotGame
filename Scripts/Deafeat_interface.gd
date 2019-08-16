extends MarginContainer

# Declare member variables here. Examples:
# var a = 2
# var b = "text"

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func update_score(points: int):
	var points_label = get_node("VBoxContainer/Points_Container/NinePatchRect/Points_Earned_Label")
	points_label.text = str(points)