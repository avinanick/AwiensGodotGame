extends OptionButton

# Declare member variables here. Examples:
# var a = 2
# var b = "text"

# Called when the node enters the scene tree for the first time.
func _ready():
	self.add_item("Chaingun")
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func update_turret_list():
	# Check each turret in the global turrets, if it is unlocked and not already added, add it to the list
	for turret_type in Global.turret_unlocks:
		if Global.turret_unlocks[turret_type]:
			var turret_already_added: bool = false
			for item_index in range(self.get_item_count()):
				if self.get_item_text(item_index) == turret_type:
					turret_already_added = true
			if not turret_already_added:
				self.add_item(turret_type)


func _on_TurretSelectionDropdown_item_selected(ID):
	# This should alert the interface that the current turret selection has changed
	# Probably would be best to use signals for this, but I'm just going through getting parents
	var replace_interface = self.get_parent().get_parent().get_parent().get_parent().get_parent().get_parent()
	var turret_position: String
	match get_parent().name:
		"NorthTurretList":
			turret_position = "North"
		"SouthTurretList":
			turret_position = "South"
		"EastTurretList":
			turret_position = "East"
		"WestTurretList":
			turret_position = "West"
	replace_interface.turret_selected(self.get_item_text(self.get_item_index(ID)), turret_position)
