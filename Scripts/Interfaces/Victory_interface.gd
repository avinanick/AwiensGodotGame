extends MarginContainer
class_name VictoryInterface

var kill_icon_scene = preload("res://Scenes/Interfaces/KillCountIcon.tscn")
var active_icons := {}

signal continue_game

# Called when the node enters the scene tree for the first time.
func _ready():
	self.connect("continue_game", get_node("../UpgradesInterface"), "display_upgrades")
	#self.connect("continue_game", get_parent(), "begin_transition_stage")

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func add_kill_icon(var icon_type: String):
	if not active_icons.has(icon_type):
		var new_icon = kill_icon_scene.instance()
		new_icon.assign_icon(icon_type)
		var icon_container = $VBoxContainer/HBoxContainer/KillCountContainer/Panel/KillCountIconGrid
		icon_container.add_child(new_icon)
		active_icons[icon_type] = new_icon

func clear_kill_icons():
	active_icons.clear()
	var icon_container = $VBoxContainer/HBoxContainer/KillCountContainer/Panel/KillCountIconGrid
	var icons = icon_container.get_children()
	for icon in icons:
		icon.queue_free()

func enemy_destroyed(var point_value: int, var alien_name: String):
	var killed_alien_icon = active_icons.get(alien_name)
	if killed_alien_icon:
		killed_alien_icon.increment_count()
		
func on_player_victory(var points: int):
	self.visible = true
	self.update_score(points)

func update_score(points: int):
	var point_label = get_node("VBoxContainer/Points_label_container/points_label")
	point_label.text = str("Points Earned: ", points)


func _on_Continue_button_button_up():
	#main_scene.next_level()
	self.clear_kill_icons()
	emit_signal("continue_game")
	self.visible = false


func _on_Save_Quit_button_button_up():
	Global.save_arcade_game()
	Global.reset_all()
	get_tree().change_scene("res://Scenes/Interfaces/Main_Menu.tscn")


func _on_Quit_button_button_up():
	# When abandoning the city, delete the current save then load the main menu
	print("Quiting game")
	var dir = Directory.new()
	dir.remove("user://arcadesave.save")
	Global.reset_all()
	get_tree().change_scene("res://Scenes/Interfaces/Main_Menu.tscn")
