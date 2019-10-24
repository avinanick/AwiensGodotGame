extends Destructible

class_name Structure

export var position: String = ""

var combatant = false
signal damaged

# Called when the node enters the scene tree for the first time.
func _ready():
	add_to_group("Earthlings")
	self.connect("damaged", get_node("../../MainOverlay"), "structure_damaged")

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func take_damage(var amount: int):
	health -= amount
	emit_signal("damaged", self)
	if health <= 0:
		destroy_self()
