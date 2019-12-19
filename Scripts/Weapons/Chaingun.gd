extends Turret

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func make_connections():
	get_node("../../..").connect("start_transition", self, "upgrade_projectile")
	.make_connections()

func upgrade_projectile():
	if Global.upgrade_unlocks["Incendiary Rounds"]:
		print("Upgrading to incendiary rounds")
		self.projectile = load("res://Scenes/IncendiaryRound.tscn")
	else:
		self.projectile = load("res://Scenes/Bullet.tscn")

