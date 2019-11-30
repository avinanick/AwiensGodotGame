extends Structure
class_name EnergyShield

export var recovery_per_second: float = 1
var recovery_timer: float = 0
var time_per_recover: float
var overloaded: bool = false

# Called when the node enters the scene tree for the first time.
func _ready():
	time_per_recover = 1 / recovery_per_second
	self.combatant = true
	var mount_name = self.get_parent().name
	if "North" in mount_name:
		self.position = "NorthShield"
	if "South" in mount_name:
		self.position = "SouthShield"
	if "West" in mount_name:
		self.position = "WestShield"
	if "East" in mount_name:
		self.position = "EastShield"
	if self.position == "":
		self.position = "CityShield"
	emit_signal("health_changed", self)

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if health < max_health:
		recovery_timer += delta
	if overloaded:
		if recovery_timer >= (self.max_health * time_per_recover):
			recovery_timer = 0
			overloaded = false
			self.health = self.max_health
			get_node("CollisionShape").disabled = false
	elif recovery_timer >= time_per_recover:
		recovery_timer = 0
		if health < max_health:
			health += 1
			emit_signal("health_changed", self)
			
func make_connections():
	self.connect("health_changed", get_node("../../../MainOverlay"), "structure_health_changed")
			
func take_damage(var amount: int):
	# For now, this will always play a flicker/fade animation for the shield, I'd like to modify this
	# to play a shattering animation when the shield breaks
	self.health -= amount
	emit_signal("health_changed", self)
	get_node("AnimationPlayer").play("DamageImpact")
	if self.health <= 0:
		overloaded = true
		get_node("CollisionShape").disabled = true
