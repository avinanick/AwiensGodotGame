extends MarginContainer

var attacker_icon_scene = preload("res://Scenes/Interfaces/AlienAttackerIcon.tscn")

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func add_icon(var enemy_type: String):
	# Adds an icon of the selected enemy type
	var new_icon = attacker_icon_scene.instance()
	new_icon.assign_icon(enemy_type)
	var enemy_grid = $DisplayPanel/VBoxContainer/IconPanel/ScrollContainer/EnemyList
	enemy_grid.add_child(new_icon)

func clear_display():
	# Clears out all the icons on the grid
	var enemy_list = $DisplayPanel/VBoxContainer/IconPanel/ScrollContainer/EnemyList
	for icon in enemy_list.get_children():
		icon.queue_free()
		
func on_round_start():
	self.visible = false
		
func on_transition_start():
	self.visible = true