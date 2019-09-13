extends Alien

# Declare member variables here. Examples:
# var a = 2
# var b = "text"
export var defense_destroyer: bool = false
export var attack_range: float = 10
export var turn_speed: float = 3
export var attack_damage: int = 1
export var attack_interval: float = 2
export var projectile = preload("res://Scenes/Bullet.tscn")
export var projectile_speed: float = 100
onready var earthlings = get_tree().get_nodes_in_group("Earthlings")
onready var city = get_node("/root/MainScene/City")
var current_target
var attack_cooldown: float = 0

# Called when the node enters the scene tree for the first time.
func _ready():
	current_target = null
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if main_scene.game_state == main_scene.game_states.running:
		scan_for_targets()
		update_ship_direction(delta)
		attack_target(delta)
		# For some reason move_and_slide isn't moving at all, that would be my preferred behavior though
		move_and_collide(alien_direction * speed * delta)
	pass

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
	# If we went through everything and non qualified, set current target to null
	current_target = null
				
# This function checks if the ship is currently headed toward the city, if not, then it will need to start turning
func update_ship_direction(delta):
	if city:
		# get the vector from the current position to the city, if we aren't pointed at the city, start turning
		# the default rotation for the ship will be along the positive z-axis, which is a rotation of:
		#       x: 0, y: -90, z: 0
		#       Only need to change the y rotation for direction, with + going toward +x-axis from here
		var direction_to_city: Vector3 = get_city_location() - self.get_global_transform().origin
		var facing_direction: Vector3 = Vector3(cos(self.rotation.y), 0, -1 * sin(self.rotation.y))
		# Compare the direction to city with the facing direction to see if they are the same
		# This will use the dot product to get the angle, then the cross product to narrow down if it's to the left or
		# right (if the angle isn't close to zero
		var angle: float = acos(direction_to_city.normalized().dot(facing_direction.normalized()))
		if angle > 0.001:
			var direction: float = facing_direction.cross(direction_to_city).y
			if direction > 0: # This case the turn should be to the right
				self.rotation.y += turn_speed * delta
			else:
				self.rotation.y -= turn_speed * delta
		alien_direction = Vector3(cos(self.rotation.y), 0, -1 * sin(self.rotation.y)).normalized()
		
# Fires a projectile at the alien ship's target, if it has one and attack is not on cooldown
func attack_target(delta):
	attack_cooldown -= delta
	if attack_cooldown <= 0:
		if current_target != null:
			attack_cooldown = attack_interval
			var newBullet := projectile.instance() as Bullet
			newBullet.bullet_damage = self.attack_damage
			newBullet.set_name("bullet")
			main_scene.add_child(newBullet)
			# Make sure the start location for the bullet is offset from the ship so it doesn't immediately collide with 
			# it's own attack
			var start_location: Vector3 = self.get_global_transform().origin
			start_location.y -= 1
			newBullet.translation = start_location
			# Get the direction from the ship to the target and set that as the bullet direction, I don't really
			# care about the bullet rotation for now
			var directionVector: Vector3
			directionVector = current_target.get_global_transform().origin - self.get_global_transform().origin
			directionVector = directionVector.normalized()
			newBullet.speed = projectile_speed
			newBullet.bulletDirection = directionVector
			
func initialize_direction():
	# This should find the direction to the city and start moving toward it
	# I don't like the magic number in alien direction, specifically the 30 in height, it's what I'm using
	# as the standard alien height right now though
	var spawn_pos: Vector3 = self.transform.origin
	var direction_to_city: Vector3 = get_city_location() - spawn_pos
	self.look_at(get_city_location(), Vector3(0,1,0))
	self.alien_direction = Vector3(direction_to_city.x, 0, direction_to_city.z).normalized()
	
# This is just to make my life easier in above functions when getting direction to the city
func get_city_location() -> Vector3:
	return Vector3(city.transform.origin.x, self.transform.origin.y, city.transform.origin.z)