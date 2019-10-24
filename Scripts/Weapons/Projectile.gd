extends KinematicBody
class_name Projectile


export var speed: float = 0
var bulletDirection := Vector3()
export var timer: float = 10
export var bullet_damage: int = 1
onready var main_scene = get_node("/root/MainScene")
var damages_earthlings: bool = true
var damages_aliens: bool = true

# Called when the node enters the scene tree for the first time.
func _ready():
	print("projectile created")

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if main_scene.game_state == main_scene.game_states.running:
		timer -= delta
		if timer <= 0:
			print("destroying projectile")
			destroy_self()
		# moves the bullet and returns a collision if there is one
		handle_impact(move_and_collide(bulletDirection * speed * delta))

# This should be overwritten in anything inheriting from this class
func handle_impact(var collision: KinematicCollision):
	destroy_self()
	
# This function can be overridden to play effects or 
func destroy_self():
	self.queue_free()
