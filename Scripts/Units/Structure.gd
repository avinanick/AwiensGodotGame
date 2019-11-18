extends Destructible

class_name Structure

export var position: String = ""

var combatant = false
signal health_changed

# Called when the node enters the scene tree for the first time.
func _ready():
	add_to_group("Earthlings")
	self.make_connections()

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func make_connections():
	self.connect("health_changed", get_node("../../MainOverlay"), "structure_health_changed")
	
func repair_structure():
	health = self.max_health
	emit_signal("health_changed")

func take_damage(var amount: int):
	health -= amount
	emit_signal("health_changed", self)
	if health <= 0:
		destroy_self()
