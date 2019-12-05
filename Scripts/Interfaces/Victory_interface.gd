extends MarginContainer
class_name VictoryInterface

var kill_icon_scene = preload("res://Scenes/Interfaces/KillCountIcon.tscn")
var active_icons := {}

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.

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

func update_score(points: int):
	var point_label = get_node("VBoxContainer/Points_label_container/points_label")
	point_label.text = str("Points Earned: ", points)
