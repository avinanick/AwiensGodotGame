extends VBoxContainer

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass


func _on_Retry_Button_button_up():
	Global.reset_all()
	get_tree().reload_current_scene()


func _on_Quit_Button_button_up():
	var dir = Directory.new()
	dir.remove("user://arcadesave.save")
	Global.reset_all()
	get_tree().change_scene("res://Scenes/Interfaces/Main_Menu.tscn")
