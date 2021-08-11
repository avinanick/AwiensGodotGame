extends "res://Components/Basic/Component.gd"


export var projectile: PackedScene = preload("res://Scenes/Bullet.tscn")
export var projectile_damage: int = 2
export var attack_cooldown: float = 1.0
var ready_to_fire: bool = true

signal primary_attack


# Called when the node enters the scene tree for the first time.
func _ready():
	self.connect("primary_attack", self, "attempt_fire")


# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func attempt_fire(var direction: Vector3):
	if ready_to_fire:
		# Fire and start cooldown
		var spawn_position: Vector3 = get_parent().global_position + direction.normalized()
		ready_to_fire = false
		$CooldownTimer.start(attack_cooldown)
		var newProjectile = projectile.instance()
		get_tree().current_scene.add_child(newProjectile)
		newProjectile.translation = spawn_position

func on_cooldown_timeout():
	ready_to_fire = true

func process_message(var message: String, var args: Dictionary):
	if message == "fire_primary":
		emit_signal("primary_attack", args["direction"], args["position"])
