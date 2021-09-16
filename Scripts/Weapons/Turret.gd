extends Structure

class_name Turret

export var fire_cooldown: float = 0.3
export var projectile = preload("res://GameObjects/Projectiles/Bullet.tscn")

var timer: float = 0

onready var main_scene := get_node("/root/MainScene")

# Called when the node enters the scene tree for the first time.
func _ready():
	self.combatant = true
	var mount_name = self.get_parent().name
	if "North" in mount_name:
		self.position = "North"
	if "South" in mount_name:
		self.position = "South"
	if "West" in mount_name:
		self.position = "West"
	if "East" in mount_name:
		self.position = "East"

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if timer < fire_cooldown:
		timer += delta

func fire(start_location : Vector3, start_rotation : Vector3):
	if is_alive and timer >= fire_cooldown:
		timer = 0
		var fire_audio = get_node("TurretFireAudio")
		if fire_audio:
			fire_audio.play()
		var newBullet = projectile.instance()
		# Get the camera's rotation (in radians) and use that to calculate the direction vector for the
		# bullet
		var directionVector := Vector3()
		directionVector.x = (-1) * sin(start_rotation.y) * cos(start_rotation.x)
		directionVector.y = sin(start_rotation.x)
		directionVector.z = (-1) * cos(start_rotation.y) * cos(start_rotation.x)
		directionVector = directionVector.normalized()
		# If the turret has an active shield, set the bullet to ignore the shield
		var shield = get_parent().get_shield()
		if shield:
			newBullet.add_collision_exception_with(shield)
		newBullet.set_name("bullet")
		newBullet.translation = start_location
		newBullet.rotation = start_rotation
		newBullet.bulletDirection = directionVector
		main_scene.add_child(newBullet)
	if not is_alive:
		print("attempting to fire dead turret")
	
func make_connections():
	self.connect("health_changed", get_node("../../../MainOverlay"), "structure_health_changed")
	self.connect("health_changed", Global, "on_structure_health_changed")
	
func reset_sights():
	var model = get_node("TurretModel")
	if model:
		model.reset_sights()
	
func sight(var x_rotation: float, var y_rotation: float):
	var model = get_node("TurretModel")
	if model:
		model.sight(x_rotation, y_rotation)


func _on_AnimationPlayer_animation_finished(anim_name):
	if anim_name == "Destroy":
		self.finish_death()
