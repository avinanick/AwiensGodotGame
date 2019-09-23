extends Projectile

export var burst_projectile = preload("res://Scenes/Bullet.tscn")

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

# In this version, on impact the projectile should burst into a larger number of smaller projectiles 
# that fire outward in a sphere shape
# Using an algorithm from https://www.cmu.edu/biolphys/deserno/pdf/sphere_equi.pdf
func handle_impact(var collision: KinematicCollision):
	destroy_self()
	pass
	
func destroy_self():
	var burst_shrapnel_vectors = Global.burst_shrapnel_vectors
	for shrapnel_vector in burst_shrapnel_vectors:
		var new_shrapnel := burst_projectile.instance() as Projectile
		var spawn_position: Vector3 = self.get_global_transform().origin + shrapnel_vector
		new_shrapnel.translation = spawn_position
		var direction_vector: Vector3 = spawn_position - self.get_global_transform().origin
		new_shrapnel.bulletDirection = direction_vector.normalized()
		new_shrapnel.bullet_damage = 1
		new_shrapnel.speed = 80
		new_shrapnel.timer = 0.1
		if Global.unlocks["shaped_blast"]:
			new_shrapnel.damages_earthlings = false
		main_scene.add_child(new_shrapnel)
	self.queue_free()