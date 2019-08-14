extends VBoxContainer

# This will be used to receive all the button signals and sent the appropriate info to the game manager
# Using this intermediary instead of the game manager directly so this can be a self contained scene
onready var main_scene = get_node("/root/MainScene")

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass


func _on_Retry_button_button_up():
	get_tree().reload_current_scene()


func _on_Continue_button_button_up():
	pass # Replace with function body. This will tell the game manager to move to the next scene, that will handle it


func _on_Quit_button_button_up():
	pass # Replace with function body. This should load the main menu
