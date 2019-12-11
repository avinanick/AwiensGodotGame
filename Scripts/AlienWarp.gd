extends Spatial

signal warp_completed
signal warp_expanded
signal warp_out_completed
signal warp_out_expanded

# Called when the node enters the scene tree for the first time.
func _ready():
	self.connect("warp_out_expanded", get_parent(), "on_warp_out_expanded")
	self.connect("warp_out_completed", get_parent(), "on_warp_out_completed")
	self.connect("warp_expanded", get_parent(), "on_warp_expanded")
	self.connect("warp_completed", get_parent(), "on_warp_completed")

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func on_warp_completed():
	emit_signal("warp_completed")

func on_warp_expanded():
	emit_signal("warp_expanded")
	
func on_warp_out_completed():
	emit_signal("warp_out_completed")
	
func on_warp_out_expanded():
	emit_signal("warp_out_expanded")
	
func start_warp_in():
	get_node("AnimationPlayer").play("WarpIn")
	
func start_warp_out():
	get_node("AnimationPlayer").play("WarpOut")
