extends "res://Scripts/NewArcadeGame.gd"

# Declare member variables here. Examples:
# var a = 2
# var b = "text"

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass


func _on_Arcade_Button_button_up():
	var save_game := File.new()
	if not save_game.file_exists("user://arcadesave.save"):
		print("Error: save file not found")
		return # Need some better error handling here
		
	# Read the data from the save file (there should likely be only one line, but while loop just in case
	save_game.open("user://arcadesave.save", File.READ)
	while not save_game.eof_reached():
		var current_line = parse_json(save_game.get_line())
		Global.current_level = current_line["level"]
		Global.total_points = current_line["points"]
	# and finally, load the actual arcade scene
	get_tree().change_scene("res://Scenes/MainScene.tscn")

