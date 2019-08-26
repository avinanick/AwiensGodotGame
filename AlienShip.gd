extends Alien

# Declare member variables here. Examples:
# var a = 2
# var b = "text"
var defense_destroyer: bool = false
export var attack_range: float = 10
onready var earthlings = get_tree().get_nodes_in_group("Earthlings")
var current_target

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

# Add a function that searches through the defender group for enemies in range
# This function scans the earthlings group for valid targets, if this ship is a defense destroyer, all are valid,
# otherwise only non-combatants are valid
func scan_for_targets():
	for earthling in earthlings:
		var valid_target: bool = false
		if defense_destroyer:
			valid_target = true
		elif not earthling.combatant:
			valid_target = true
		if valid_target:
			# I'm not sure yet if this gives the global positions or local, could cause issues if local
			var distance: float = self.translation.distance_to(earthling.translation) 
			# If any target is in range, assign it as the current target and stop scanning
			if distance < attack_range:
				current_target = earthling
				return