extends MarginContainer
class_name UpgradeInterface

onready var points_label = get_node("OuterOutline/TotalPointsLabel")
onready var main_scene = get_node("/root/MainScene")

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func update_points(var amount: int):
	points_label.text = str(amount)

func _on_ContinueButton_button_up():
	main_scene.start_level_preparation()


# I have a different function for each node for the same signal, that doesn't seem efficient.
func _on_FlakCannonUpgrade_upgrade_purchased(var cost: int):
	update_points(Global.total_points)


func _on_ShapedBlastUpgrade_upgrade_purchased(var cost: int):
	update_points(Global.total_points)


func _on_RepairUpgradeButton_upgrade_purchased(var cost: int):
	update_points(Global.total_points)


func _on_EditTurretsButton_button_up():
	var replace_interface = get_node("../TurretReplaceInterface")
	replace_interface.update_turret_options()
	replace_interface.visible = true
	self.visible = false
