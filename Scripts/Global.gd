extends Node

var current_level: int = 1
var total_points: int = 0
var turret_unlocks := {
	"Flak Cannon": false,
	"Thunder Cannon": false
}
var upgrade_unlocks := {
	"Shaped Blast": false,
	"Incendiary Rounds": false,
	"Energy Shields": false
}

# variables for calculating the vector3 directions when bursting a flak shell
var burst_amount: float = 0.2
var burst_start_radius: float = 0.1
var burst_shrapnel_vectors := []

# turrets
var north_turret_health: int = 10
var east_turret_health: int = 10
var west_turret_health: int = 10
var south_turret_health: int = 10
var north_turret_type: String = "Chaingun"
var east_turret_type: String = "Chaingun"
var west_turret_type: String = "Chaingun"
var south_turret_type: String = "Chaingun"

# city 
var north_east_building_health: int = 10
var north_west_building_health: int = 10
var south_east_building_health: int = 10
var south_west_building_health: int = 10

# Called when the node enters the scene tree for the first time.
func _ready():
	# code to calculate flak shell burst shrapnel
	var a: float = 4 * PI * burst_start_radius * burst_start_radius / burst_amount
	var d: float = sqrt(a)
	var m_theta: float = round(PI / d)
	var d_theta: float = PI / m_theta
	var d_phi: float = a / d_theta
	for m in range(m_theta as int):
		var theta: float = PI * (m + 0.5) / m_theta
		var m_phi: float = round(2 * PI * sin(theta) / d_phi)
		for n in range(m_phi as int):
			var phi: float = 2 * PI * n / m_phi
			# Create point using theta, phi, and radius values
			burst_shrapnel_vectors.append(burst_start_radius * 
										Vector3(sin(theta) * cos(phi), 
												sin(theta) * sin(phi), 
												cos(theta)))
	print(burst_shrapnel_vectors.size())

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func load_arcade_game():
	var save_game := File.new()
	if not save_game.file_exists("user://arcadesave.save"):
		print("Error: save file not found")
		return # Need some better error handling here
		
	# Read the data from the save file (there should likely be only one line, but while loop just in case
	save_game.open("user://arcadesave.save", File.READ)
	while not save_game.eof_reached():
		var current_line = parse_json(save_game.get_line())
		# sanity check, as for some reason this is reading an extra line at the end
		if current_line:
			self.current_level = current_line["level"] as int
			self.total_points = current_line["points"] as int
			self.north_turret_health = current_line["north_turret_health"] as int
			self.north_turret_type = current_line["north_turret_type"] as String
			self.east_turret_health = current_line["east_turret_health"] as int
			self.east_turret_type = current_line["east_turret_type"] as String
			self.west_turret_health = current_line["west_turret_health"] as int
			self.west_turret_type = current_line["west_turret_type"] as String
			self.south_turret_health = current_line["south_turret_health"] as int
			self.south_turret_type = current_line["south_turret_type"] as String
			self.north_east_building_health = current_line["north_east_building_health"] as int
			self.north_west_building_health = current_line["north_west_building_health"] as int
			self.south_east_building_health = current_line["south_east_building_health"] as int
			self.south_west_building_health = current_line["south_west_building_health"] as int
			for key in turret_unlocks:
				turret_unlocks[key] = current_line[key] as bool
			for key in upgrade_unlocks:
				upgrade_unlocks[key] = upgrade_unlocks[key] as bool
	# and finally, load the actual arcade scene
	get_tree().change_scene("res://Scenes/MainScene.tscn")

func on_structure_health_changed(var structure):
	match structure.position:
		"North":
			self.north_turret_health = structure.health
		"East":
			self.east_turret_health = structure.health
		"West":
			self.west_turret_health = structure.health
		"South":
			self.south_turret_health = structure.health
		"NorthEast":
			self.north_east_building_health = structure.health
		"NorthWest":
			self.north_west_building_health = structure.health
		"SouthEast":
			self.south_east_building_health = structure.health
		"SouthWest":
			self.south_west_building_health = structure.health
	
func on_turret_type_changed(var turret_type: String, var turret_location: String):
	match turret_location:
		"North":
			self.north_turret_type = turret_type
		"East":
			self.east_turret_type = turret_type
		"West":
			self.west_turret_type = turret_type
		"South":
			self.south_turret_type = turret_type

func reset_all():
	current_level = 1
	total_points = 0
	reset_unlocks()

func reset_unlocks():
	for key in turret_unlocks:
		turret_unlocks[key] = false
	for key in upgrade_unlocks:
		upgrade_unlocks[key] = false
		
func save_arcade_game():
	# The corresponding load game is in the LoadGameInterface script
	var save_game := File.new()
	save_game.open("user://arcadesave.save", File.WRITE)
	var save_dict := {
		"level" : self.current_level,
		"points" : self.total_points,
		"north_turret_health" : self.north_turret_health,
		"north_turret_type" : self.north_turret_type,
		"east_turret_health" : self.east_turret_health,
		"east_turret_type" : self.east_turret_type,
		"west_turret_health" : self.west_turret_health,
		"west_turret_type" : self.west_turret_type,
		"south_turret_health" : self.south_turret_health,
		"south_turret_type" : self.south_turret_type,
		"north_east_building_health" : self.north_east_building_health,
		"north_west_building_health" : self.north_west_building_health,
		"south_east_building_health" : self.south_east_building_health,
		"south_west_building_health" : self.south_west_building_health
	}
	for key in turret_unlocks:
		save_dict[key] = turret_unlocks[key]
	for key in upgrade_unlocks:
		save_dict[key] = upgrade_unlocks[key]
	save_game.store_line(to_json(save_dict))
	save_game.close()
	
