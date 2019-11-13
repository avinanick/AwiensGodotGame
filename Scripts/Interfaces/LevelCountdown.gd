extends MarginContainer

var timer: float = 5.3 # seems like a decent starting point
var displaying_number: TextureRect = null
var next_number: int = 5

signal level_countdown_finished

# Called when the node enters the scene tree for the first time.
func _ready():
	# Just to be sure, but they should already all be invisible
	for child in self.get_children():
		child.visible = false
	self.visible = false
	self.connect("level_countdown_finished", get_node(".."), "start_next_level")

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if self.visible:
		timer -= delta
		if timer <= 0:
			emit_signal("level_countdown_finished")
		if timer <= next_number:
			if displaying_number:
				displaying_number.visible = false
				displaying_number.modulate = Color(1, 1, 1)
				displaying_number.rect_scale = Vector2(1,1)
			match next_number:
				5:
					displaying_number = get_node("FiveIcon")
					displaying_number.visible = true
				4: 
					displaying_number = get_node("FourIcon")
					displaying_number.visible = true
				3:
					displaying_number = get_node("ThreeIcon")
					displaying_number.visible = true
				2: 
					displaying_number = get_node("TwoIcon")
					displaying_number.visible = true
				1: 
					displaying_number = get_node("OneIcon")
					displaying_number.visible = true
			next_number = next_number - 1
		if displaying_number:
			displaying_number.modulate = Color(1,1,1, timer - next_number)
			# for scale, I think I want it to start at 1,1 and end at 4,4
			var scaling_addition: float = 3 * (1 - (timer - next_number))
			displaying_number.rect_scale = Vector2(1 + scaling_addition, 1 + scaling_addition)
		
func on_transition_start():
	self.visible = true
	timer = 5.3
