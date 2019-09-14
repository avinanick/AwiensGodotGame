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
	var a: float = 4 * PI * burst_start_radius * burst_start_radius / burst_amount
	var d: float = sqrt(a)
	var m_theta: float = round(PI / d)
	var d_theta: float = PI / m_theta
	var d_phi: float = a / d_theta
	for m in range(m_theta as int):
		var theta: float = PI * (m + 0.5) / m_theta
		var m_phi: float = round(2 * PI * sin(theta) / d_phi)
		for n in range(m_phi as int):
			var phi: float = 2 * PI * n / m_phi
			# Create point using theta, phi, and radius values
	self.queue_free()
	pass