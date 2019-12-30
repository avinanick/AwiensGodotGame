extends AudioStreamPlayer3D

# Declare member variables here. Examples:
# var a = 2
# var b = "text"

# Called when the node enters the scene tree for the first time.
func _ready():
	self.unit_db = GameOptions.master_volume
	GameOptions.connect("volume_changed", self, "update_volume")
	
# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func update_volume(var new_value):
	self.unit_db = new_value