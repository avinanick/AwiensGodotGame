extends MarginContainer

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass


func _on_Arcade_Button_button_up():
	Global.current_level = 1
	Global.total_points = 0
	get_tree().change_scene("res://Scenes/Levels/MainScene.tscn")
