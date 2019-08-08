extends Spatial

# Declare member variables here. Examples:
# var a = 2
# var b = "text"
var cooldown = 0.3 # .3 seconds between each shot
var timer = 0
onready var bullet = preload("res://Bullet.tscn") # placeholder until I start setting up the turret logic
export var bulletSpeed = 1

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if timer <= cooldown:
		timer += delta
	if Input.is_action_pressed("fire_one"):
		#set up some logic to fire the turret
		if timer >= cooldown:
			timer = 0
			var newBullet = bullet.instance()
			newBullet.set_name("bullet")
			# so far I've only been able to find the bullet when I set it as a child of self, but then
			# whenever I rotate the camera, the bullet goes with it
			get_node("/root/MainScene").add_child(newBullet)
			newBullet.translation = self.to_global(self.translation)
			# Get the camera's rotation (in radians) and use that to calculate the direction vector for the
			# bullet
			var polarCoords = self.get_parent().rotation
			var directionVector = Vector3()
			directionVector.x = (-1) * sin(polarCoords.y) * cos(polarCoords.x)
			directionVector.y = sin(polarCoords.x)
			directionVector.z = (-1) * cos(polarCoords.y) * cos(polarCoords.x)
			directionVector = directionVector.normalized()
			newBullet.speed = bulletSpeed
			newBullet.rotation = polarCoords
			newBullet.bulletDirection = directionVector

