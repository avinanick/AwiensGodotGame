extends MarginContainer
class_name UpgradeInterface

onready var points_label = get_node("OuterOutline/TotalPointsLabel")

signal upgrading_finished

# Called when the node enters the scene tree for the first time.
func _ready():
	#self.connect("upgrading_finished", get_node("../LevelCountdown"), "on_transition_start")
	#self.connect("upgrading_finished", get_node("../EnemyWarningInterface"), "on_transition_start")
	self.connect("upgrading_finished", get_parent(), "begin_transition_stage")

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func display_upgrades():
	self.visible = true
	self.update_points(Global.total_points)

func update_points(var amount: int):
	points_label.text = str(amount)

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
