extends Spatial

# Declare member variables here. Examples:
# var a = 2
# var b = "text"

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if get_child_count() < 1:
		var main_scene = get_node("/root/MainScene")
		main_scene.game_state = main_scene.game_states.defeat
		self.queue_free()

# Somewhere in the process I want to count the number of children and lose based on that