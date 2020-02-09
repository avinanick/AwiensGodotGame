extends MarginContainer

# Called when the node enters the scene tree for the first time.
func _ready():
	var sensetivity_slider = get_node("OptionsList/MouseSensitivityEdit/SensitivitySlider")
	sensetivity_slider.value = GameOptions.mouse_sensitivity
	get_node("OptionsList/MouseSensitivityEdit/SensitivityValueLabel").text = str(GameOptions.mouse_sensitivity)
	get_node("OptionsList/MasterVolumeEdit/VolumeSlider").value = (GameOptions.master_volume + 80) / 1.6
	get_node("OptionsList/MasterVolumeEdit/VolumeValueLabel").text = str(int((GameOptions.master_volume + 80) / 1.6))

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func mouse_sensitivity_modified(var value: float):
	GameOptions.mouse_sensitivity = value
	get_node("OptionsList/MouseSensitivityEdit/SensitivityValueLabel").text = str(value)
	
func master_volume_modified(var value: float):
	var dB_volume: float = -80 + (value * 1.6)
	GameOptions.change_volume(dB_volume)
	get_node("OptionsList/MasterVolumeEdit/VolumeValueLabel").text = str(value)
