extends MarginContainer
#tool

export var cost: int = 10 #setget set_cost_text, get_cost_text
export(String, "Turret", "Upgrade", "Repair") var upgrade_type := "Turret"
export var upgrade_name: String = ""  #setget set_name_text, get_name_text
export var upgrade_icon: Texture = preload("res://icon.png") #setget set_icon_texture, get_icon_texture

onready var interface_handler
signal turret_purchased
signal upgrade_purchased

# Called when the node enters the scene tree for the first time.
func _ready():
	get_node("UpgradeList/UpgradeButton/CostLabel").text = str(cost)
	get_node("UpgradeList/UpgradeName").text = upgrade_name
	get_node("UpgradeList/UpgradeButton").icon = upgrade_icon
	var button = get_node("UpgradeList/UpgradeButton")
	if button:
		button.hint_tooltip = self.hint_tooltip

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func _on_UpgradeButton_button_up():
	if Global.total_points >= cost:
		Global.total_points -= cost
		emit_signal("upgrade_purchased", cost)
		get_node("UpgradeList/UpgradeButton").disabled = true
		match upgrade_type:
			"Turret":
				Global.turret_unlocks[upgrade_name] = true
				emit_signal("turret_purchased", upgrade_name)
			"Upgrade":
				Global.upgrade_unlocks[upgrade_name] = true
			"Repair":
				# I don't like having this unique special case, I bet I can do better later
				var earthlings = get_tree().get_nodes_in_group("Earthlings")
				for earthling in earthlings:
					earthling.health = earthling.max_health
		disable_upgrade()
	else:
		print("Handle Error: Not enough points")
		
func disable_upgrade():
	get_node("UpgradeList/UpgradeButton").disabled = true
	
func enable_upgrade():
	get_node("UpgradeList/UpgradeButton").disabled = false
		
func get_cost_text():
	var cost_label = get_node("UpgradeList/UpgradeButton/CostLabel")
	if cost_label:
		return cost_label.text
		
func get_icon_texture():
	var upgrade_button = get_node("UpgradeList/UpgradeButton")
	if upgrade_button:
		return upgrade_button.icon
		
func get_name_text():
	var name_label = get_node("UpgradeList/UpgradeName")
	if name_label:
		return name_label.text
		
func refresh_upgrade():
	if visible:
		if upgrade_type == "Repair":
			enable_upgrade()
		if upgrade_type == "Upgrade":
			if Global.upgrade_unlocks[self.upgrade_name]:
				self.disable_upgrade()
		if upgrade_type == "Turret":
			if Global.turret_unlocks[self.upgrade_name]:
				self.disable_upgrade()
		
func set_icon_texture(var icon_value: Texture):
	var upgrade_button = get_node("UpgradeList/UpgradeButton")
	if upgrade_button:
		upgrade_button.icon = icon_value
		
func set_cost_text(var cost_value: int):
	var cost_label = get_node("UpgradeList/UpgradeButton/CostLabel")
	if cost_label:
		cost_label.text = str(cost_value)
		
func set_name_text(var name_value: String):
	var name_label = get_node("UpgradeList/UpgradeName")
	if name_label:
		name_label.text = name_value
