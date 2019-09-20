extends MarginContainer

export var cost: int = 20
signal upgrade_purchased

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass


func _on_ShapedBlastButton_button_up():
	if Global.total_points >= cost:
		Global.total_points -= cost
		Global.unlocks["shaped_blast"] = true
		emit_signal("upgrade_purchased", cost)
		# I need to do something else to disable this button after the unlock I think
	else:
		print("Handle Error: not enough points")
