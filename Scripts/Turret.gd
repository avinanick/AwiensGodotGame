extends Structure

class_name Turret
# Declare member variables here. Examples:
# var a = 2
# var b = "text"
export var turret_type = "chaingun"
export var fire_cooldown = 0.3
export var projectile = preload("res://Scenes/Bullet.tscn")
export var projectile_speed = 100
var timer = 0

# Called when the node enters the scene tree for the first time.
func _ready():
	combatant = true

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if timer < fire_cooldown:
		timer += delta

func fire(start_location : Vector3, start_rotation : Vector3):
	if is_alive and timer >= fire_cooldown:
		timer = 0
		var newBullet = projectile.instance()
		newBullet.set_name("bullet")
		# so far I've only been able to find the bullet when I set it as a child of self, but then
		# whenever I rotate the camera, the bullet goes with it
		get_node("/root/MainScene").add_child(newBullet)
		get_node("/root/MainScene").shots += 1
		newBullet.translation = start_location
		# Get the camera's rotation (in radians) and use that to calculate the direction vector for the
		# bullet
		var directionVector = Vector3()
		directionVector.x = (-1) * sin(start_rotation.y) * cos(start_rotation.x)
		directionVector.y = sin(start_rotation.x)
		directionVector.z = (-1) * cos(start_rotation.y) * cos(start_rotation.x)
		directionVector = directionVector.normalized()
		newBullet.speed = projectile_speed
		newBullet.rotation = start_rotation
		newBullet.bulletDirection = directionVector
	if not is_alive:
		print("attempting to fire dead turret")