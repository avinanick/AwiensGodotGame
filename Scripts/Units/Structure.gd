extends Destructible

class_name Structure

export var position: String = ""

var combatant = false

signal building_destroyed
signal health_changed

# Called when the node enters the scene tree for the first time.
func _ready():
	add_to_group("Earthlings")

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func destroy_self():
	emit_signal("building_destroyed")
	var animation_player = get_node("AnimationPlayer")
	var collapse_audio = get_node("CollapseAudioPlayer")
	if collapse_audio:
		collapse_audio.play()
	if animation_player:
		animation_player.play("Destroy")
		self.remove_from_group("Earthlings")
	else:
		self.queue_free()
	
func finish_death():
	self.queue_free()
	
func load_data():
	match self.position:
		"North":
			self.health = Global.north_turret_health
		"East":
			self.health = Global.east_turret_health
		"West":
			self.health = Global.west_turret_health
		"South":
			self.health = Global.south_turret_health
		"NorthEast":
			self.health = Global.north_east_building_health
		"NorthWest":
			self.health = Global.north_west_building_health
		"SouthEast":
			self.health = Global.south_east_building_health
		"SouthWest":
			self.health = Global.south_west_building_health
	emit_signal("health_changed", self)
	if self.health <= 0:
		self.queue_free()

func make_connections():
	self.connect("building_destroyed", get_parent(), "building_lost")
	self.connect("health_changed", get_node("../../MainOverlay"), "structure_health_changed")
	self.connect("health_changed", Global, "on_structure_health_changed")
	
func repair_structure():
	health = self.max_health
	emit_signal("health_changed", self)

func handle_damage(var amount: int):
	health -= (amount * damage_modifier)
	emit_signal("health_changed", self)
	if health <= 0:
		destroy_self()
