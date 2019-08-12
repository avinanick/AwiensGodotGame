extends Alien

class_name AlienMissile

export var speed = 5
export var missile_damage = 3
var missile_direction = Vector3()
var timer = 60

# Declare member variables here. Examples:
# var a = 2
# var b = "text"

# Called when the node enters the scene tree for the first time.
func _ready():
	point_value = 1
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if main_scene.game_state == main_scene.game_states.running:
		timer -= delta
		if timer <= 0:
			print("destroying missile")
			self.queue_free()
		# Not sure why but the missile isn't moving for some reason
		var collision = move_and_collide(missile_direction * speed * delta)
		if collision:
			print("collision detected, destroying missile")
			if collision.collider is Destructible:
				print("damaging target")
				collision.collider.take_damage(missile_damage)
			self.queue_free()
