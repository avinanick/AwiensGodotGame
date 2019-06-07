extends KinematicBody

# Declare member variables here. Examples:
# var a = 2
# var b = "text"
# rotation should be modified as well, by default this is alligned with the z-axis
var speed = 0
var bulletDirection = Vector3()
var timer = 10
var bullet_damage = 1

# Called when the node enters the scene tree for the first time.
func _ready():
	print("bullet created")
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	timer -= delta
	if timer <= 0:
		print("destroying bullet")
		self.queue_free()
	# moves the bullet and returns a collision if there is one
	var collision = move_and_collide(bulletDirection * speed * delta)
	if collision:
		print("collision detected, destroying bullet")
		if collision.collider is Destructible:
			print("damaging target")
			collision.collider.take_damage(bullet_damage)
		self.queue_free()
	pass

# Add code to self destruct after some amount of time or on collision
