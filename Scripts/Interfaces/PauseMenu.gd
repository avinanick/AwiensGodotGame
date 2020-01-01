extends MarginContainer

# Declare member variables here. Examples:
# var a = 2
# var b = "text"

# Called when the node enters the scene tree for the first time.
func _ready():
	self.visible = false

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func make_connections():
	get_parent().connect("pause_game", self, "on_pause")
	get_parent().connect("unpause_game", self, "on_unpause")
	
func on_pause():
	self.visible = true

func on_unpause():
	self.visible = false

func _on_AbandonButton_button_up():
	# When abandoning the city, delete the current save then load the main menu
	print("Quiting game")
	var dir = Directory.new()
	dir.remove("user://arcadesave.save")
	Global.reset_all()
	get_tree().change_scene("res://Scenes/Interfaces/Main_Menu.tscn")


func _on_ResumeButton_button_up():
	get_parent().pause_unpause_game()
