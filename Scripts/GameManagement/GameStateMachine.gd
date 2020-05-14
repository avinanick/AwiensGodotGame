extends Node


enum game_states {
	running,
	player_victory,
	player_defeat,
	paused,
	transition
}

var game_state = game_states.transition
var current_level: int = 0

signal level_start
signal player_victory
signal player_defeat
signal transition_start


# Called when the node enters the scene tree for the first time.
func _ready():
	on_preparation_completed()


# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func on_city_destroyed():
	game_state = game_states.player_defeat
	Input.set_mouse_mode(Input.MOUSE_MODE_VISIBLE)
	emit_signal("player_defeat")
	
func on_level_completion():
	if game_state == game_states.player_victory:
		Input.set_mouse_mode(Input.MOUSE_MODE_VISIBLE)
		game_state = game_states.transition
		emit_signal("transition_start")

func on_level_time_up():
	if game_state == game_states.running:
		Input.set_mouse_mode(Input.MOUSE_MODE_VISIBLE)
		game_state = game_states.player_victory
		emit_signal("player_victory")
		
func on_preparation_completed():
	game_state = game_states.running
	current_level += 1
	Input.set_mouse_mode(Input.MOUSE_MODE_CAPTURED)
	emit_signal("level_start")
