extends MarginContainer

# Called when the node enters the scene tree for the first time.
func _ready():
	get_node("VBoxContainer/MouseSensitivityEdit/SensitivitySlider")

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func mouse_sensitivity_modified(var value: float):
	GameOptions.mouse_sensitivity = value
	get_node("VBoxContainer/MouseSensitivityEdit/SensitivityValueLabel").text = str(value)