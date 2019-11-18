extends MarginContainer

export var cost: int = 5
signal upgrade_purchased

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass


func _on_RepairButton_button_up():
	if Global.total_points >= cost:
		Global.total_points -= cost
		emit_signal("upgrade_purchased", cost)
		var earthlings = get_tree().get_nodes_in_group("Earthlings")
		for earthling in earthlings:
			earthling.repair_structure()
	else:
		print("Handle Error: not enough points")
