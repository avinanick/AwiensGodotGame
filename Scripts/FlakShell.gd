extends Projectile

export var burst_projectile = preload("res://Scenes/Bullet.tscn")
export var burst_amount: int = 30
export var burst_start_radius: float = 0.1

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
	var burst_shrapnel_vectors = Global.burst_shrapnel_vectors
	for shrapnel_vector in burst_shrapnel_vectors:
		var new_shrapnel = burst_projectile.instance()
	self.queue_free()
	pass