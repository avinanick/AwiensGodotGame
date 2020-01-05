extends MarginContainer
class_name UpgradeInterface

onready var points_label = get_node("OuterOutline/InnerBackground/PointsBackdrop/TotalPointsLabel")
var upgrade_requirements := {"Thunder Cannon" : [],
							"Flak Cannon" : []}

signal refresh_upgrades
signal upgrading_finished

# Called when the node enters the scene tree for the first time.
func _ready():
	upgrade_requirements["Flak Cannon"].append(get_node("OuterOutline/InnerBackground/ScrollContainer/VBoxContainer/UpgradesContainer/ShapedBlastUpgrade"))
	pass

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func display_upgrades():
	for requirement in upgrade_requirements:
		if Global.turret_unlocks[requirement]:
			print("Enabling buttons for ", requirement)
			for upgrade_button in upgrade_requirements[requirement]:
				if upgrade_button:
					upgrade_button.enable_upgrade()
		else:
			print("Disabling buttons for ", requirement)
			for upgrade_button in upgrade_requirements[requirement]:
				if upgrade_button:
					print("Disabling ", upgrade_button.upgrade_name)
					upgrade_button.disable_upgrade()
	emit_signal("refresh_upgrades")
	self.visible = true
	self.update_points(Global.total_points)
	
func make_connections():
	self.connect("upgrading_finished", get_parent(), "begin_transition_stage")

func update_points(var amount: int):
	points_label.text = str("Current Points: ", amount)

func _on_ContinueButton_button_up():
	emit_signal("upgrading_finished")
	self.visible = false


# I have a different function for each node for the same signal, that doesn't seem efficient.
func _on_upgrade_purchased(var cost: int):
	update_points(Global.total_points)


func _on_EditTurretsButton_button_up():
	var replace_interface = get_node("../TurretReplaceInterface")
	replace_interface.update_turret_options()
	replace_interface.visible = true
	self.visible = false
	
func _on_turret_purchased(var turret_type: String):
	if turret_type in upgrade_requirements:
		for upgrade_button in upgrade_requirements[turret_type]:
			upgrade_button.enable_upgrade()
