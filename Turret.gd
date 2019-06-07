extends Structure

class_name Turret
# Declare member variables here. Examples:
# var a = 2
# var b = "text"
export var turret_type = "chaingun"
export var fire_cooldown = 0.3
export var projectile = preload("res://Bullet.tscn")
export var projectile_speed = 100
var timer = 0

# Called when the node enters the scene tree for the first time.
func _ready():
	combatant = true
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if timer < fire_cooldown:
		timer += delta
	pass

func fire():
	if timer >= fire_cooldown:
		timer = 0
		var newBullet = projectile.instance()
		newBullet.set_name("bullet")
		# so far I've only been able to find the bullet when I set it as a child of self, but then
		# whenever I rotate the camera, the bullet goes with it
		get_node("/root/MainScene").add_child(newBullet)
		newBullet.translation = get_node("/root/MainScene/Camera").translation
		# Get the camera's rotation (in radians) and use that to calculate the direction vector for the
		# bullet
		var polarCoords = get_node("/root/MainScene/Camera").rotation
		var directionVector = Vector3()
		directionVector.x = (-1) * sin(polarCoords.y) * cos(polarCoords.x)
		directionVector.y = sin(polarCoords.x)
		directionVector.z = (-1) * cos(polarCoords.y) * cos(polarCoords.x)
		directionVector = directionVector.normalized()
		newBullet.speed = projectile_speed
		newBullet.rotation = polarCoords
		newBullet.bulletDirection = directionVector
	pass