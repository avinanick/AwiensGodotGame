extends Node

var current_level: int = 1
var total_points: int = 0
var turret_unlocks := {
	"Flak Cannon": false
}
var upgrade_unlocks := {
	"Shaped Blast": false
}

# variables for calculating the vector3 directions when bursting a flak shell
var burst_amount: float = 0.2
var burst_start_radius: float = 0.1
var burst_shrapnel_vectors := []

# Called when the node enters the scene tree for the first time.
func _ready():
	# code to calculate flak shell burst shrapnel
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
			burst_shrapnel_vectors.append(burst_start_radius * 
										Vector3(sin(theta) * cos(phi), 
												sin(theta) * sin(phi), 
												cos(theta)))
	print(burst_shrapnel_vectors.size())

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func reset_all():
	current_level = 1
	total_points = 0
	reset_unlocks()

func reset_unlocks():
	for key in turret_unlocks:
		turret_unlocks[key] = false
	for key in upgrade_unlocks:
		upgrade_unlocks[key] = false
	
