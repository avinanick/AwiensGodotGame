extends Projectile

var burning_node = preload("res://Scenes/Burn.tscn")

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

# Damage the target and destroy self
func handle_impact(var collision: KinematicCollision):
	# For the incendiary rounds, if the impact target is not an energy shield, attach a burning
	# damage over time to the target
	if collision:
		if collision.collider is Destructible:
			if (collision.collider.is_in_group("Earthlings") and damages_earthlings) \
				or (collision.collider.is_in_group("Aliens") and damages_aliens):
					collision.collider.take_damage(bullet_damage)
					if not (collision.collider is EnergyShield):
						var new_burn = burning_node.instance()
						collision.collider.add_child(new_burn)
		destroy_self()
