extends Sprite

onready var ally_indicator = preload("res://Scenes/Interfaces/AllyRadarIndicator.tscn")
onready var enemy_indicator = preload("res://Scenes/Interfaces/EnemyRadarIndicator.tscn")
# I need to get a better way to populate these with the map size, then turn it into a radius
export var x_map_range: float = 50
export var y_map_range: float = 50
var shrink_factor: float
onready var player_view = get_node("PlayerView")
onready var player_avatar = get_node("../../Avatar")

# Called when the node enters the scene tree for the first time.
func _ready():
	var self_size = self.texture.get_size()
	shrink_factor = x_map_range / self_size.x

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	populate_indicators()

func clear_indicators():
	var indicators = get_tree().get_nodes_in_group("Indicators")
	for indicator in indicators:
		indicator.queue_free()

func populate_indicators():
	# start by getting all the living earthlings and populating their position
	# I need to invert the y positions on the radar so it matches the intuition for north vs south
	# Since in 2d, positive y is downward
	clear_indicators()
	var earthlings = get_tree().get_nodes_in_group("Earthlings")
	var avatar_position = player_avatar.get_global_transform().origin
	player_view.position = Vector2(-1 * avatar_position.x * 3, -1 * avatar_position.z * 3)
	player_view.rotation_degrees = -1 * player_avatar.rotation_degrees.y
	for earthling in earthlings:
		var earthling_position: Vector3 = earthling.get_global_transform().origin
		# I'll need to use the x and z values to figure out where on the radar it goes
		var indicator_position: Vector2 = Vector2( -1 * earthling_position.x * 3, -1 * earthling_position.z * 3)
		var new_ally = ally_indicator.instance()
		self.add_child(new_ally)
		new_ally.add_to_group("Indicators")
		new_ally.position = indicator_position
	var aliens = get_tree().get_nodes_in_group("Aliens")
	for alien in aliens:
		var alien_position: Vector3 = alien.get_global_transform().origin
		# I'll need to use the x and z values to figure out where on the radar it goes
		var indicator_position: Vector2 = Vector2(-1 * alien_position.x * 3, -1 * alien_position.z * 3)
		var new_enemy = enemy_indicator.instance()
		self.add_child(new_enemy)
		new_enemy.add_to_group("Indicators")
		new_enemy.position = indicator_position