extends MarginContainer
class_name DefeatInterface

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func make_connections():
	get_parent().connect("player_defeat", self, "on_player_defeat")
	
func on_player_defeat():
	self.visible = true
	Input.set_mouse_mode(Input.MOUSE_MODE_VISIBLE)

func update_score(points: int):
	var points_label = get_node("VBoxContainer/Points_Container/Points_Earned_Label")
	points_label.text = str(points)
	
func update_time(time_remaining: int):
	var time_label = get_node("VBoxContainer/Time_Remaining_Container/Time_Remaining_Label")
	time_label.text = str(time_remaining)