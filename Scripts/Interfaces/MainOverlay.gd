extends MarginContainer
class_name MainOverlay

onready var time_left_label = get_node("VBoxContainer/Counters/Counter/Background/TimerLabel")

export var game_time: float = 30
var timer: float = 0

signal times_up

# Called when the node enters the scene tree for the first time.
func _ready():
	set_process(false)

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	timer += delta
	if timer < game_time:
		update_time(int(game_time - timer))
	else:
		print("times up")
		self.end_level()
		
func end_level():
	set_process(false)
	emit_signal("times_up")
	
func make_connections():
	self.connect("times_up", get_parent(), "player_victory")
	get_parent().connect("start_level", self, "start_level")
	
func start_level():
	timer = 0
	set_process(true)

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
