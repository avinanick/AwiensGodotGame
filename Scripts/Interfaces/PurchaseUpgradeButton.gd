extends MarginContainer

export var cost: int = 10
export(String, "Turret", "Upgrade", "Repair") var upgrade_type := "Turret"
export var upgrade_name: String = ""
export var upgrade_icon: Texture = preload("res://icon.png")

onready var interface_handler
signal upgrade_purchased

# Called when the node enters the scene tree for the first time.
func _ready():
	get_node("UpgradeList/UpgradeButton/CostLabel").text = str(cost)
	get_node("UpgradeList/UpgradeName").text = upgrade_name
	get_node("UpgradeList/UpgradeButton").icon = upgrade_icon

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func _on_UpgradeButton_button_up():
	if Global.total_points >= cost:
		Global.total_points -= cost
		emit_signal("upgrade_purchased", cost)
		get_node("FlakCannonGroup/FlakCannonButton").disabled = true
		match upgrade_type:
			"Turret":
				Global.turret_unlocks[upgrade_name] = true
			"Upgrade":
				Global.upgrade_unlocks[upgrade_name] = true
			"Repair":
				# I don't like having this unique special case, I bet I can do better later
				var earthlings = get_tree().get_nodes_in_group("Earthlings")
				for earthling in earthlings:
					earthling.health = earthling.max_health
		get_node("UpgradeList/UpgradeButton").disabled = true
	else:
		print("Handle Error: Not enough points")
