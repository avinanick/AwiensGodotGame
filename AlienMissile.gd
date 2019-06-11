extends Alien

class_name AlienMissile

export var speed = 10
export var missile_damage = 3
var missile_direction = Vector3()
var timer = 60

# Declare member variables here. Examples:
# var a = 2
# var b = "text"

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	timer -= delta
	if timer <= 0:
		print("destroying bullet")
		self.queue_free()
	# Not sure why but the missile isn't moving for some reason
	var collision = move_and_collide(missile_direction * speed * delta)
	if collision:
		print("collision detected, destroying missile")
		if collision.collider is Destructible:
			print("damaging target")
			collision.collider.take_damage(missile_damage)
		self.queue_free()
	pass
	pass
