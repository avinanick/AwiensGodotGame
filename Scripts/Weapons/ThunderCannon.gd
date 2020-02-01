extends Turret
class_name ThunderCannon

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
			var modified_rotation = Vector3(-1 * start_rotation.x, start_rotation.y, -1 * start_rotation.z)
			targeting_zone.rotation = modified_rotation
			var target = scan_for_targets()
			if target:
				var lightning_effect = get_node("LightningBlast")
				lightning_effect.scale.z = self.translation.distance_to(target.translation)
				lightning_effect.rotation = start_rotation
				if lightning_effect:
					lightning_effect.get_node("AnimationPlayer").play("LightningFire")
				timer = 0
				target.take_damage(lightning_damage)
	
func scan_for_targets():
	# Checks if there are any valid targets in the targeting zone area, if so, returns the closest one
	var possible_targets = targeting_zone.get_overlapping_bodies()
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
	return target
