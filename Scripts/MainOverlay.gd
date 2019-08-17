extends MarginContainer

# Declare member variables here. Examples:
# var a = 2
# var b = "text"
onready var time_left_label = get_node("HBoxContainer/Counters/Counter/Background/Number")

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass
func update_time(var time_remaining: int):
	time_left_label.text = str(time_remaining)