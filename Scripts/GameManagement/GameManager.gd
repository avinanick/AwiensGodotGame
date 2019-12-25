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

onready var victory_screen := get_node("Victory_interface")
onready var defeat_screen := get_node("Defeat_interface")
onready var main_overlay := get_node("MainOverlay")
onready var upgrade_interface := get_node("UpgradesInterface")
onready var turret_replace_interface := get_node("TurretReplaceInterface")
onready var player_avatar := get_node("Avatar")
onready var turret_placement_camera := get_node("TurretPlacementCamera") as Camera

var enemy_spawner = preload("res://Scenes/Units/EnemySpawner.tscn")

signal start_transition
signal pause_game
signal player_defeat
signal player_victory
signal start_level

# Called when the node enters the scene tree for the first time.
# This should be modified to respond to load data
func _ready():
	self.connect("start_transition", get_node("LevelCountdown"), "on_transition_start")
	self.connect("start_transition", get_node("EnemyWarningInterface"), "on_transition_start")
	self.connect("player_victory", get_node("EnemyWarningInterface"), "clear_display")
	self.connect("player_victory", get_node("Victory_interface"), "on_player_victory")
	if victory_screen:
		victory_screen.visible = false
	if defeat_screen:
		defeat_screen.visible = false
	if upgrade_interface:
		upgrade_interface.visible = false
	if turret_replace_interface:
		turret_replace_interface.visible = false
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
	

# Process should probably be redone using signals
#func _process(delta):
#	pass
			
# Used to increment points when destroying an alien
func enemy_destroyed(var point_value: int, var alien_name: String):
	points += point_value
		
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
	
func start_level_preparation():
	# This is called after the continue button in the upgrade interface is clicked. It should start a timed countdown
	# until the enemy waves start spawning again (this is not currently shown to the player) and update the number
	# and difficulty of the spawners. At some point I'd also like to bring up an interface that shows the new enemy
	# types
	upgrade_interface.visible = false
	turret_replace_interface.visible = false
	self.game_state = game_states.transitioning
	self.points = 0
	self.timer = 0
	emit_signal("start_transition")
	# Every 5 levels, add another spawner
	if self.level % 5 == 0:
		var new_spawner = enemy_spawner.instance()
		new_spawner.translation = Vector3(0,30,0)
		self.add_child(new_spawner)
	var spawners = get_tree().get_nodes_in_group("Spawners")
	for spawner in spawners:
		spawner.randomize_spawn()
		spawner.update_spawner_difficulty()
		
func start_next_level():
	if game_state == game_states.transitioning:
		Input.set_mouse_mode(Input.MOUSE_MODE_CAPTURED)
		self.game_state = self.game_states.running
	
###########################################REPLACE WITH BELOW#########################################

func begin_transition_stage():
	self.game_state = self.game_states.transitioning
	# Every 5 levels, add another spawner
	if self.level % 5 == 0:
		var new_spawner = enemy_spawner.instance()
		new_spawner.translation = Vector3(0,30,0)
		self.add_child(new_spawner)
	emit_signal("start_transition")

func pause_game():
	if self.game_state == self.game_states.running:
		self.game_state = self.game_states.paused
		emit_signal("pause_game")

func player_defeat():
	self.game_state = self.game_states.defeat
	emit_signal("player_defeat")

func player_victory():
	if self.game_state == self.game_states.running:
		print("Victory!")
		Input.set_mouse_mode(Input.MOUSE_MODE_VISIBLE)
		self.game_state = self.game_states.victory
		Global.total_points += points
		self.level += 1
		Global.current_level += 1
		points = 0
		emit_signal("player_victory")
	
func start_level():
	Input.set_mouse_mode(Input.MOUSE_MODE_CAPTURED)
	self.game_state = self.game_states.running
	emit_signal("start_level")

