extends Turret

onready var targeting_zone = get_node("TargetingZone")

export var lightning_damage: int = 3

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func fire(start_location : Vector3, start_rotation : Vector3):
	# This should rotate the targetingZone to where the camera is looking and check if there are any enemies in that
	# zone, if the cooldown is ready. TargetingZone can be rotated directly, and will work perfectly
	if is_alive and timer >= fire_cooldown:
		if targeting_zone:
			targeting_zone.rotation = start_rotation
			var target = scan_for_targets()
			if target:
				timer = 0
				target.take_damage(lightning_damage)
	
func scan_for_targets():
	# Checks if there are any valid targets in the targeting zone area, if so, returns the closest one
	var possible_targets = targeting_zone.get_overlapping_areas()
	var target = null
	if possible_targets.size() > 0:
		var smallest_distance: int
		for possible_target in possible_targets:
			var distance = self.get_global_transform().origin.distance_to(possible_target.get_global_transform().origin)
			if target:
				if possible_target.is_in_group("Aliens") and distance < smallest_distance:
					target = possible_target
					smallest_distance = distance
			else:
				if possible_target.is_in_group("Aliens"):
					target = possible_target
					smallest_distance = distance
	else:
		print("No targets in range")
	return target