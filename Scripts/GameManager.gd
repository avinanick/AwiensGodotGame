extends Node

class_name GameManager
# Declare member variables here. Examples:
# var a = 2
# var b = "text" 
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
export var game_time: int = 100
var level: int = 1
var timer: float = 0
var game_state = game_states.running
var points: int = 0
onready var victory_screen = get_node("Victory_interface")
onready var defeat_screen = get_node("Defeat_interface")
onready var main_overlay = get_node("MainOverlay")
onready var upgrade_interface = get_node("UpgradesInterface")

# Called when the node enters the scene tree for the first time.
func _ready():
	if victory_screen:
		victory_screen.visible = false
	if defeat_screen:
		defeat_screen.visible = false
	if upgrade_interface:
		upgrade_interface.visible = false
	

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
		game_states.running:
			timer += delta
			main_overlay.update_time(int(ceil(game_time - timer)))
		game_states.transitioning:
			timer += delta
			# Stub for now until I plan out everything that will go into the transition
			if timer >= 3:
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
	Input.set_mouse_mode(Input.MOUSE_MODE_CAPTURED)
	upgrade_interface.visible = false
	self.game_state = game_states.transitioning
	self.points = 0
	self.timer = 0
