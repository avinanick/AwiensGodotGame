extends KinematicBody
class_name Bullet
# Declare member variables here. Examples:
# var a = 2
# var b = "text"
# rotation should be modified as well, by default this is alligned with the z-axis
var speed: float = 0
var bulletDirection := Vector3()
var timer: float = 10
var bullet_damage: int = 1
onready var main_scene = get_node("/root/MainScene") as GameManager

# Called when the node enters the scene tree for the first time.
func _ready():
	print("bullet created")

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if main_scene.game_state == main_scene.game_states.running:
		timer -= delta
		if timer <= 0:
			print("destroying bullet")
			self.queue_free()
		# moves the bullet and returns a collision if there is one
		var collision := move_and_collide(bulletDirection * speed * delta)
		if collision:
			print("collision detected, destroying bullet")
			if collision.collider is Destructible:
				print("damaging target")
				collision.collider.take_damage(bullet_damage)
			self.queue_free()

# Add code to self destruct after some amount of time or on collision
