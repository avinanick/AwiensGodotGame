extends Turret
class_name AutoTurret

var target
onready var targeting_zone = get_node("TargetingRange")

export var targeting_range: float = 15.0

# Called when the node enters the scene tree for the first time.
func _ready():
	self.deactivate()

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func _physics_process(delta):
	if target == null:
		var space_state = get_world().direct_space_state
		scan_for_targets(space_state)
	else:
		var direction_to_enemy: Vector3 = target.get_global_transform().origin - self.get_global_transform().origin
		sight(atan(direction_to_enemy.y / direction_to_enemy.x),atan(direction_to_enemy.z / Vector2(direction_to_enemy.x, direction_to_enemy.y).length()))
		fire(self.get_global_transform().origin, self.rotation)
		
func activate():
	self.visible = true
	get_node("TargetingRange").monitoring = true
	self.add_to_group("Earthlings")
	self.set_physics_process(true)
	
func deactivate():
	self.visible = false
	get_node("TargetingRange").monitoring = false
	self.remove_from_group("Earthlings")
	self.set_physics_process(false)
	
func finish_death():
	self.deactivate()
		
func fire(start_location: Vector3, start_rotation: Vector3):
	if not self.validate_target_clearance(get_world().direct_space_state, target):
		print("Target lost")
		self.target = null
	elif self.timer >= self.fire_cooldown:
		timer = 0
		print("Firing weapon...")
		var fire_audio = get_node("TurretFireAudio")
		if fire_audio:
			fire_audio.play()
		var newBullet = projectile.instance()
		# Get the camera's rotation (in radians) and use that to calculate the direction vector for the
		# bullet
		var directionVector := Vector3()
		directionVector.x = (-1) * sin(start_rotation.y) * cos(start_rotation.x)
		directionVector.y = sin(start_rotation.x)
		directionVector.z = (-1) * cos(start_rotation.y) * cos(start_rotation.x)
		directionVector = directionVector.normalized()
		newBullet.add_collision_exception_with(self)
		# If the turret has an active shield, set the bullet to ignore the shield
		var shield = get_parent().get_shield()
		if shield:
			newBullet.add_collision_exception_with(shield)
		newBullet.translation = start_location
		newBullet.rotation = start_rotation
		newBullet.bulletDirection = directionVector
		main_scene.add_child(newBullet)

func make_connections():
	pass
	
func scan_for_targets(var space_state: PhysicsDirectSpaceState):
	var targets_in_range = targeting_zone.get_overlapping_bodies()
	for possible_target in targets_in_range:
		if possible_target.is_in_group("Aliens"):
			if self.validate_target_clearance(space_state, possible_target):
				target = possible_target
				print("Target acquired")
				return

func validate_target_clearance(var space_state: PhysicsDirectSpaceState, var target) -> bool:
	var clearance_check = space_state.intersect_ray(self.get_global_transform().origin, target.get_global_transform().origin, [self, get_parent().get_shield()])
	if clearance_check:
		return (clearance_check.collider == target) and (self.get_global_transform().origin.distance_to(target.get_global_transform().origin) < targeting_range)
	return false