extends VBoxContainer

# Declare member variables here. Examples:
# var a = 2
# var b = "text"

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass


func _on_Retry_Button_button_up():
	get_tree().reload_current_scene()


func _on_Quit_Button_button_up():
	pass # Replace with function body. This should load the main menu
