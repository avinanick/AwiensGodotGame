extends Spatial

var timer: float = 0.5
var exploding: bool = false
var cleanup: bool = false

signal explosion_expanded
signal explosion_finished

# Called when the node enters the scene tree for the first time.
func _ready():
	self.connect("explosion_finished", get_parent(), "on_explosion_finished")
	self.connect("explosion_expanded", get_parent(), "on_warp_out_expanded")

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if exploding or cleanup:
		timer -= delta
		if timer <= 0:
			if exploding:
				exploding = false
				cleanup = true
				timer = 0.5
				get_child(0).emitting = false
				emit_signal("explosion_expanded")
			else:
				emit_signal("explosion_finished")

func explode():
	exploding = true
	get_child(0).emitting = true
	get_child(1).play()
