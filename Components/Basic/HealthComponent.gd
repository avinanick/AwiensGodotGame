extends "res://Components/Basic/Component.gd"


export var health_points: int = 10
export var max_health: int = 10

signal incoming_damage
signal incoming_repair


# Called when the node enters the scene tree for the first time.
func _ready():
	self.connect("incoming_damage", self, "take_damage")
	self.connect("incoming_repair", self, "repair_damage")


# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func process_message(var message: String, var args: Dictionary):
	if message == "deal_damage":
		emit_signal("incoming_damage", args["damage"])
	if message == "repair_damage":
		emit_signal("incoming_repair", args["repair"])

func repair_damage(var amount: int):
	self.health_points += amount
	if self.health_points > self.max_health:
		self.health_points = max_health

func take_damage(var amount: int):
	self.health_points -= amount
	if self.health_points <= 0:
		get_parent().propagate_message("self_destroyed", {})
