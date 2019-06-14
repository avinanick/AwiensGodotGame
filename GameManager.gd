extends Node

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
var timer = 0
var game_state = game_states.running
var points = 0
var hits = 0
var shots = 0

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	match game_state:
		game_states.defeat:
			print("you lost!")
		game_states.victory:
			print("we won!")
		game_states.running:
			timer += delta

func score_points(amount):
	points += amount