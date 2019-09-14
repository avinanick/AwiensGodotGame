extends MarginContainer

export var cost: int = 30

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
