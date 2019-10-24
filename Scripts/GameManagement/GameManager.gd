extends Node

class_name GameManager

enum game_states{
	running,
	paused,
	victory,
	defeat,
	transitioning
}

# This script should handle failing on city destruction and victory after some amount of time, likely also handle
# accuracy tracking and such
# calculate points at the end using some mix of time and accuracy
export var game_time: int = 30
var level: int = 1
var timer: float = 0
var game_state = game_states.running
var points: int = 0
var selected_turret

onready var victory_screen := get_node("Victory_interface")
onready var defeat_screen := get_node("Defeat_interface")
onready var main_overlay := get_node("MainOverlay")
onready var upgrade_interface := get_node("UpgradesInterface")
onready var turret_change_interface := get_node("TurretChangeInterface")
onready var player_avatar := get_node("Avatar")
onready var turret_placement_camera := get_node("TurretPlacementCamera") as Camera

var enemy_spawner = preload("res://Scenes/Units/EnemySpawner.tscn")

# Called when the node enters the scene tree for the first time.
# This should be modified to respond to load data
func _ready():
	if victory_screen:
		victory_screen.visible = false
	if defeat_screen:
		defeat_screen.visible = false
	if upgrade_interface:
		upgrade_interface.visible = false
	if turret_change_interface:
		turret_change_interface.visible = false
	if Global.current_level != 1:
		print("Loading level")
		var spawners_to_create: int = Global.current_level / 5
		for i in range(spawners_to_create):
			var new_spawner = enemy_spawner.instance()
			new_spawner.translation = Vector3(0,30,0)
			self.add_child(new_spawner)
			new_spawner.randomize_spawn()
			new_spawner.update_spawner_difficulty()
		get_node("MissileSpawner").randomize_spawn()
			# This should also start the unit warning and countdown till start
	

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if timer >= game_time and game_state == game_states.running:
		game_state = game_states.victory
	match game_state:
		game_states.defeat:
			# This should actually make the defeat screen available if it isn't already
			if defeat_screen and not defeat_screen.visible:
				Input.set_mouse_mode(Input.MOUSE_MODE_VISIBLE)
				print("you lost!")
				defeat_screen.update_score(points)
				defeat_screen.update_time(int(ceil(game_time - timer)))
				defeat_screen.visible = true
		game_states.victory:
			if victory_screen and not victory_screen.visible:
				Input.set_mouse_mode(Input.MOUSE_MODE_VISIBLE)
				print("we won! Points: " + str(points))
				victory_screen.update_score(points)
				victory_screen.visible = true
				var aliens = get_tree().get_nodes_in_group("Aliens")
				for alien in aliens:
					alien.retreat()
		game_states.running:
			# Sanity check
			if victory_screen.visible:
				victory_screen.visible = false
			timer += delta
			main_overlay.update_time(int(ceil(game_time - timer)))
		game_states.transitioning:
			if victory_screen.visible:
				victory_screen.visible = false
			if selected_turret and not turret_change_interface.visible:
				turret_change_interface.update_buttons()
				turret_change_interface.visible = true
			timer += delta
			# For now I'll give the player 10 seconds to change up their turret layout
			if timer >= 10:
				timer = 0
				turret_change_interface.visible = false
				player_avatar.activate_camera()
				Input.set_mouse_mode(Input.MOUSE_MODE_CAPTURED)
				self.game_state = self.game_states.running
			
# Used to increment points when destroying an alien
func enemy_destroyed(var point_value: int):
	points += point_value

func score_points(amount: int):
	points += amount
		
func next_level():
	# This should update the global singleton with the number of points earned this round, increment the level, then bring
	# up the menu for spending points. 
	victory_screen.visible = false
	Global.total_points += points
	Global.current_level += 1
	self.level += 1
	upgrade_interface.update_points(Global.total_points)
	upgrade_interface.visible = true
	
func start_level_preparation():
	# This is called after the continue button in the upgrade interface is clicked. It should start a timed countdown
	# until the enemy waves start spawning again (this is not currently shown to the player) and update the number
	# and difficulty of the spawners. At some point I'd also like to bring up an interface that shows the new enemy
	# types
	turret_placement_camera.make_current()
	upgrade_interface.visible = false
	self.game_state = game_states.transitioning
	self.points = 0
	self.timer = 0
	# Every 5 levels, add another spawner
	if self.level % 5 == 0:
		var new_spawner = enemy_spawner.instance()
		new_spawner.translation = Vector3(0,30,0)
		self.add_child(new_spawner)
	var spawners = get_tree().get_nodes_in_group("Spawners")
	for spawner in spawners:
		spawner.randomize_spawn()
		spawner.update_spawner_difficulty()
		
func save_arcade_game():
	# The corresponding load game is in the LoadGameInterface script
	var save_game := File.new()
	save_game.open("user://arcadesave.save", File.WRITE)
	var save_dict := {
		"level" : Global.current_level,
		"points" : Global.total_points
	}
	save_game.store_line(to_json(save_dict))
	save_game.close()
	
func replace_turret(var new_type):
	if selected_turret:
		var health: int = selected_turret.health
		var position: Vector3 = selected_turret.get_global_transform().origin
		var node_name: String = selected_turret.get_name()
		var gun_position: String = selected_turret.position
		selected_turret.queue_free()
		var new_turret = new_type.instance()
		new_turret.translation = position
		new_turret.health = health
		new_turret.set_name(node_name)
		new_turret.position = gun_position
		get_node("Guns").add_child(new_turret)
		turret_change_interface.visible = false
		selected_turret = null
	

