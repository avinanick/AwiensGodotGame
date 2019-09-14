extends VBoxContainer

# This will be used to receive all the button signals and sent the appropriate info to the game manager
# Using this intermediary instead of the game manager directly so this can be a self contained scene
onready var main_scene := get_node("/root/MainScene") as GameManager

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass


func _on_Save_Quit_button_button_up():
	# Modify this to save to a file before I quit to the main menu
	main_scene.save_arcade_game()
	Global.reset_all()
	get_tree().change_scene("res://Scenes/Main_Menu.tscn")


func _on_Continue_button_button_up():
	main_scene.next_level()


func _on_Quit_button_button_up():
	# When abandoning the city, delete the current save then load the main menu
	var dir = Directory.new()
	dir.remove("user://arcadesave.save")
	Global.reset_all()
	get_tree().change_scene("res://Scenes/Main_Menu.tscn")
