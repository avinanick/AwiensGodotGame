extends MarginContainer

export var cost: int = 10
onready var interface_handler
signal upgrade_purchased

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass


func _on_FlakCannonButton_button_up():
	if Global.total_points >= cost:
		Global.total_points -= cost
		Global.unlocks["flak_cannon"] = true
		emit_signal("upgrade_purchased", cost)
		get_node("FlakCannonGroup/FlakCannonButton").disabled = true
	else:
		print("Handle Error: Not enough points")
