extends Destructible

class_name Alien

# I think the accuracy logic should be somewhere in here? or in the bullet
export var alien_name: String = ""
export var point_value: int = 1
export var speed: float = 5
onready var main_scene = get_node("/root/MainScene")
var alien_direction: Vector3
var moving: bool = false

signal alien_destroyed
signal alien_damaged

# Called when the node enters the scene tree for the first time.
func _ready():
	add_to_group("Aliens")
	self.connect("alien_destroyed", get_node("/root/MainScene"), "enemy_destroyed")
	self.connect("alien_destroyed", get_node("/root/MainScene/Victory_interface"), "enemy_destroyed")
	main_scene.connect("player_victory", self, "victory_retreat")
	get_parent().connect("player_defeat", self, "retreat")
	get_node("AlienModel").visible = false
		
# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func destroy_self():
	emit_signal("alien_destroyed", self.point_value, self.alien_name)
	get_node("CollisionShape").disabled = true
	var explosion_effect = get_node("AlienExplosion")
	if explosion_effect:
		explosion_effect.explode()
	else:
		self.queue_free()
		
func handle_damage(var amount: int):
	emit_signal("alien_damaged")
	.handle_damage(amount)

func initialize_direction():
	pass
	
func on_explosion_finished():
	self.queue_free()
	
func on_warp_completed():
	self.moving = true
	
func on_warp_expanded():
	get_node("AlienModel").visible = true
	
func on_warp_out_completed():
	self.queue_free()
	
func on_warp_out_expanded():
	get_node("AlienModel").visible = false
	
# Later this will be updated to do a sort of warp out
func retreat():
	self.moving = false
	get_node("AlienWarp").start_warp_out()
		
func victory_retreat(var points: int):
	self.retreat()
	
func warp_in():
	self.initialize_direction()
	get_node("AlienWarp").start_warp_in()
