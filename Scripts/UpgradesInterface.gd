extends MarginContainer
class_name UpgradeInterface

onready var points_label = get_node("OuterOutline/TotalPointsLabel")
onready var main_scene = get_node("/root/MainScene")

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func update_points(var amount: int):
	points_label.text = str(amount)

func _on_ContinueButton_button_up():
	main_scene.start_level_preparation()
