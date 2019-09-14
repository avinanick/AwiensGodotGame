extends KinematicBody
class_name Projectile


var speed: float = 0
var bulletDirection := Vector3()
var timer: float = 10
var bullet_damage: int = 1
onready var main_scene = get_node("/root/MainScene")

# Called when the node enters the scene tree for the first time.
func _ready():
	print("projectile created")

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if main_scene.game_state == main_scene.game_states.running:
		timer -= delta
		if timer <= 0:
			print("destroying projectile")
			self.queue_free()
		# moves the bullet and returns a collision if there is one
		handle_impact(move_and_collide(bulletDirection * speed * delta))

# This should be overwritten in anything inheriting from this class
func handle_impact(var collision: KinematicCollision):
	self.queue_free()
	pass
