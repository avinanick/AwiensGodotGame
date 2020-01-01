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
var game_state = game_states.transitioning
var points: int = 0

onready var victory_screen := get_node("Victory_interface")
onready var defeat_screen := get_node("Defeat_interface")
onready var main_overlay := get_node("MainOverlay")
onready var upgrade_interface := get_node("UpgradesInterface")
onready var turret_replace_interface := get_node("TurretReplaceInterface")
onready var player_avatar := get_node("Avatar")
onready var turret_placement_camera := get_node("TurretPlacementCamera") as Camera

var enemy_spawner = preload("res://Scenes/Units/EnemySpawner.tscn")

signal load_data
signal start_transition
signal pause_game
signal unpause_game
signal player_defeat
signal player_victory
signal start_level

# Called when the node enters the scene tree for the first time.
# This should be modified to respond to load data
func _ready():
	self.connect("start_transition", get_node("LevelCountdown"), "on_transition_start")
	self.connect("start_transition", get_node("EnemyWarningInterface"), "on_transition_start")
	self.connect("player_victory", get_node("EnemyWarningInterface"), "clear_display")
	var all_children = self.get_children()
	for child in all_children:
		if child.has_method("make_connections"):
			child.make_connections()
	var guns_collection = get_node("Guns")
	for gun in guns_collection.get_children():
		gun.make_connections()
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
		emit_signal("load_data")
	emit_signal("start_transition")
	

# Process should probably be redone using signals
#func _process(delta):
#	pass
			
# Used to increment points when destroying an alien
func enemy_destroyed(var point_value: int, var alien_name: String):
	points += point_value

func begin_transition_stage():
	self.game_state = self.game_states.transitioning
	# Every 5 levels, add another spawner
	if self.level % 5 == 0:
		var new_spawner = enemy_spawner.instance()
		new_spawner.translation = Vector3(0,30,0)
		self.add_child(new_spawner)
		new_spawner.make_connections()
	emit_signal("start_transition")

func pause_unpause_game():
	if self.game_state == self.game_states.running:
		self.game_state = self.game_states.paused
		Input.set_mouse_mode(Input.MOUSE_MODE_VISIBLE)
		emit_signal("pause_game")
	if self.game_state == self.game_states.paused:
		self.game_state = self.game_states.running
		Input.set_mouse_mode(Input.MOUSE_MODE_CAPTURED)
		emit_signal("unpause_game")
		

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
		emit_signal("player_victory", points)
		points = 0
	
func start_level():
	if self.game_state == self.game_states.transitioning:
		Input.set_mouse_mode(Input.MOUSE_MODE_CAPTURED)
		self.game_state = self.game_states.running
		emit_signal("start_level")

