extends Structure

class_name Turret

export var fire_cooldown: float = 0.3
export var projectile = preload("res://Scenes/Bullet.tscn")

var timer: float = 0

onready var main_scene := get_node("/root/MainScene")

# Called when the node enters the scene tree for the first time.
func _ready():
	self.combatant = true

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if timer < fire_cooldown:
		timer += delta

func fire(start_location : Vector3, start_rotation : Vector3):
	if is_alive and timer >= fire_cooldown:
		timer = 0
		var newBullet = projectile.instance()
		# Get the camera's rotation (in radians) and use that to calculate the direction vector for the
		# bullet
		var directionVector := Vector3()
		directionVector.x = (-1) * sin(start_rotation.y) * cos(start_rotation.x)
		directionVector.y = sin(start_rotation.x)
		directionVector.z = (-1) * cos(start_rotation.y) * cos(start_rotation.x)
		directionVector = directionVector.normalized()
		
		newBullet.set_name("bullet")
		newBullet.translation = start_location
		newBullet.rotation = start_rotation
		newBullet.bulletDirection = directionVector
		main_scene.add_child(newBullet)
	if not is_alive:
		print("attempting to fire dead turret")
	
func make_connections():
	self.connect("damaged", get_node("../../../MainOverlay"), "structure_damaged")
