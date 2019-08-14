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
	ended
}

# This script should handle failing on city destruction and victory after some amount of time, likely also handle
# accuracy tracking and such
# calculate points at the end using some mix of time and accuracy
export var game_time = 100
var timer: float = 0
var game_state = game_states.running
var points: int = 0
var hits: int = 0
var shots: int = 0

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	timer += delta
	if timer >= game_time and game_state == game_states.running:
		game_state = game_states.victory
	match game_state:
		game_states.defeat:
			# This should actually make the defeat screen available if it isn't already
			print("you lost!")
		game_states.victory:
			print("we won! Points: " + str(points))
		game_states.running:
			timer += delta
			
# Used to increment points when destroying an alien
func enemy_destroyed(var point_value: int):
	points += point_value

func score_points(amount):
	points += amount

func _on_Timer_timeout():
	if game_state == game_states.running:
		game_state = game_states.victory
		
func next_level():
	pass # STUB
