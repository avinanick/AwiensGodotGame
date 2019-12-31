tool
extends HBoxContainer

export(String, "North", "West", "East", "South") var turret_position = "North" setget set_turret_position, get_turret_position

var can_update_key: bool = false
var actions: Array = ["select_north", "select_west", "select_east", "select_south"]

signal keybind_updated

# Called when the node enters the scene tree for the first time.
func _ready():
	self.update_key_text()

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func get_turret_position() -> String:
	return turret_position

func set_turret_position(position_value: String):
	turret_position = position_value
	get_node("KeyLabel").text = str("Select ", position_value, " Turret Key")
	if not Engine.editor_hint:
		update_key_text()
		
func update_key_text():
	get_node("KeyButton").text = InputMap.get_action_list(str("select_", turret_position.to_lower()))[0].as_text()
	
func update_keybind(new_key):
	var action_string: String = str("select_", turret_position.to_lower())
	#Delete key of pressed button
	if !InputMap.get_action_list(action_string).empty():
		InputMap.action_erase_event(action_string, InputMap.get_action_list(action_string)[0])
	
	#Check if new key was assigned somewhere
	for i in actions:
		if InputMap.action_has_event(i, new_key):
			InputMap.action_erase_event(i, new_key)
			
	#Add new Key
	InputMap.action_add_event(action_string, new_key)
		
func _input(event):
	if can_update_key:
		if event is InputEventKey:
			can_update_key = false
			update_keybind(event)
			get_node("PopupDialog").hide()
			update_key_text()
			emit_signal("keybind_updated")

func _on_KeyButton_button_up():
	can_update_key = true
	get_node("PopupDialog").popup()
