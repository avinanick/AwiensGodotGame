extends MarginContainer
class_name MainOverlay

onready var time_left_label = get_node("HBoxContainer/Counters/Counter/Background/TimerLabel")

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func structure_health_changed(var structure):
	var health_bar: TextureProgress
	match structure.position:
		"North":
			health_bar = get_node("HBoxContainer/Bars/TurretBar/NorthTurretHealth")
		"East":
			health_bar = get_node("HBoxContainer/Bars/TurretBar/EastTurretHealth")
		"West":
			health_bar = get_node("HBoxContainer/Bars/TurretBar/WestTurretHealth")
		"South":
			health_bar = get_node("HBoxContainer/Bars/TurretBar/SouthTurretHealth")
		"1":
			health_bar = get_node("HBoxContainer/Bars/CityBar/BuildlingHealth1")
		"2":
			health_bar = get_node("HBoxContainer/Bars/CityBar/BuildingHealth2")
		"3":
			health_bar = get_node("HBoxContainer/Bars/CityBar/BuildingHealth3")
		"4":
			health_bar = get_node("HBoxContainer/Bars/CityBar/BuildingHealth4")
	print(structure.position)
	health_bar.value = int((float(structure.health) / float(structure.max_health)) * 100)

func update_time(var time_remaining: int):
	time_left_label.text = str(time_remaining)