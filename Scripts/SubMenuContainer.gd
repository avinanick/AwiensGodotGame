extends VBoxContainer

# Declare member variables here. Examples:
# var a = 2
# var b = "text"
var new_game_menu = preload("res://Scenes/NewGameInterface.tscn")
var load_game_menu = preload("res://Scenes/LoadGameInterface.tscn")

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass


func _on_NewGameButton_button_up():
	# Clear out the current shown menu then Load the new game interface as a child of this node
	for child in self.get_children():
		child.queue_free()
	var new_game_options = new_game_menu.instance()
	self.add_child(new_game_options)


func _on_LoadGameButton_button_up():
	# Clear out the current shown menu then Load the game level from a saved file, then load the arcade scene
	# I should probably only do this if there is a game to load
	for child in self.get_children():
		child.queue_free()
	var load_game_options = load_game_menu.instance()
	self.add_child(load_game_options)

