extends Alien

class_name AlienMissile

export var missile_damage: int = 2
export var x_target_range: float = 20
export var z_target_range: float = 20
var timer: float = 60
var possible_targets = []

# Called when the node enters the scene tree for the first time.
func _ready():
	point_value = 1
	speed = 5
	var earthlings = get_tree().get_nodes_in_group("Earthlings")
	for earthling in earthlings:
		possible_targets.append(earthling.get_global_transform().origin)

# Move the missile and respond to any collisions
func _process(delta):
	if main_scene.game_state == main_scene.game_states.running:
		timer -= delta
		if timer <= 0:
			self.queue_free()
		var collision = move_and_collide(alien_direction * speed * delta)
		if collision:
			if collision.collider is Destructible:
				collision.collider.take_damage(missile_damage)
			self.destroy_self()

func initialize_direction():
	# This will pick a random coordinate from the earthling positions and set that as the target direction
	var spawn_pos = self.transform.origin
	var target_pos = possible_targets[randi() % possible_targets.size()]
	var direction_vector = Vector3(target_pos.x - spawn_pos.x, target_pos.y - spawn_pos.y,
			target_pos.z - spawn_pos.z)
	direction_vector = direction_vector.normalized()
	self.look_at(target_pos, Vector3(0,1,0))
	self.alien_direction = direction_vector
