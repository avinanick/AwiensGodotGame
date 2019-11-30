extends MarginContainer
class_name MainOverlay

onready var time_left_label = get_node("VBoxContainer/Counters/Counter/Background/TimerLabel")

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
			health_bar = get_node("VBoxContainer/HealthSpacing/HealthBars/CentralBars/NorthTurretHealthBar")
		"East":
			health_bar = get_node("VBoxContainer/HealthSpacing/HealthBars/EastTurretHealthBar")
		"West":
			health_bar = get_node("VBoxContainer/HealthSpacing/HealthBars/WestTurretHealthBar")
		"South":
			health_bar = get_node("VBoxContainer/HealthSpacing/HealthBars/CentralBars/SouthTurretHealthBar")
		"NorthWest":
			health_bar = get_node("VBoxContainer/HealthSpacing/HealthBars/CentralBars/CityShieldHealthBar/NorthWestCityBar")
		"NorthEast":
			health_bar = get_node("VBoxContainer/HealthSpacing/HealthBars/CentralBars/CityShieldHealthBar/NorthEastCityBar")
		"SouthWest":
			health_bar = get_node("VBoxContainer/HealthSpacing/HealthBars/CentralBars/CityShieldHealthBar/SouthWestCityBar")
		"SouthEast":
			health_bar = get_node("VBoxContainer/HealthSpacing/HealthBars/CentralBars/CityShieldHealthBar/SouthEastCityBar")
		"NorthShield":
			health_bar = get_node("VBoxContainer/HealthSpacing/HealthBars/CentralBars/NorthTurretHealthBar/NorthShieldHealthBar")
		"EastShield":
			health_bar = get_node("VBoxContainer/HealthSpacing/HealthBars/EastTurretHealthBar/EastShieldHealthBar")
		"WestShield":
			health_bar = get_node("VBoxContainer/HealthSpacing/HealthBars/WestTurretHealthBar/WestShieldHealthBar")
		"SouthShield":
			health_bar = get_node("VBoxContainer/HealthSpacing/HealthBars/CentralBars/SouthTurretHealthBar/SouthShieldHealthBar")
		"CityShield":
			health_bar = get_node("VBoxContainer/HealthSpacing/HealthBars/CentralBars/CityShieldHealthBar")
	print(structure.position)
	# In case there is a mistake and this didn't get assigned...
	if health_bar:
		health_bar.value = int((float(structure.health) / float(structure.max_health)) * 100)

func update_time(var time_remaining: int):
	time_left_label.text = str(time_remaining)