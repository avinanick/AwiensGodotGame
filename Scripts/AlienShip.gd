extends Alien

# Declare member variables here. Examples:
# var a = 2
# var b = "text"
var defense_destroyer: bool = false
export var attack_range: float = 10
onready var earthlings = get_tree().get_nodes_in_group("Earthlings")
onready var city = get_node("/root/City")
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
			var distance: float = self.get_global_transform().origin.distance_to(earthling.get_global_transform().origin) 
			# If any target is in range, assign it as the current target and stop scanning
			if distance < attack_range:
				current_target = earthling
				return
				
# This function checks if the ship is currently headed toward the city, if not, then it will need to start turning
func update_ship_direction():
	if city:
		# get the vector from the current position to the city, if we aren't pointed at the city, start turning
		# the default rotation for the ship will be along the positive z-axis, which is a rotation of:
		#       x: 0, y: -90, z: 0
		#       Only need to change the y rotation for direction, with + going toward +x-axis from here
		var direction_to_city: Vector3 = city.get_global_transform().origin - self.get_global_transform().origin
		var facing_direction: Vector3 = Vector3(cos(self.rotation.y), 0, -1 * sin(self.rotation.y))
		# Compare the direction to city with the facing direction to see if they are the same
	pass