extends Projectile
class_name Bullet

# rotation should be modified as well, by default this is alligned with the z-axis

# Called when the node enters the scene tree for the first time.
func _ready():
	pass

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

# Damage the target and destroy self
func handle_impact(var collision: KinematicCollision):
	if collision:
		print("collision detected, destroying bullet")
		if collision.collider is Destructible:
			if (collision.collider.is_in_group("Earthlings") and damages_earthlings) or (collision.collider.is_in_group("Aliens") and damages_aliens):
				print("damaging target")
				collision.collider.take_damage(bullet_damage)
		destroy_self()