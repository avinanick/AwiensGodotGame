using Godot;
using System;

public class Avatar : Spatial
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	private WeaponPlatform SelectedWeapon;
	
	[Signal]
	public delegate void TurretSelect(int selectionPosition);

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		Input.SetMouseMode(Input.MouseMode.Captured);
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		if(Input.IsActionPressed("fire_one")) {
			SelectedWeapon.AttemptAttack(GlobalTransform.origin, Rotation);
		}
	}

	public override void _Input(InputEvent inputEvent) {
		if(inputEvent.IsActionPressed("select_north")) {
			EmitSignal(nameof(TurretSelect), DefenseGrid.NORTH);
		}
		if(inputEvent.IsActionPressed("select_east")) {
			EmitSignal(nameof(TurretSelect), DefenseGrid.EAST);
		}
		if(inputEvent.IsActionPressed("select_west")) {
			EmitSignal(nameof(TurretSelect), DefenseGrid.WEST);
		}
		if(inputEvent.IsActionPressed("select_south")) {
			EmitSignal(nameof(TurretSelect), DefenseGrid.SOUTH);
		}

		if(inputEvent is InputEventMouseMotion motionEvent) {
			// Will also need to send this to the selected turret
			Vector3 rotation = RotationDegrees;
			if(rotation.x - (motionEvent.Relative.y) > 90) {
				rotation.x = 90;
			}
			else if(rotation.x - (motionEvent.Relative.y) < -90) {
				rotation.x = -90;
			}
			else {
				rotation.x -= motionEvent.Relative.y;
			}
			rotation.y -= motionEvent.Relative.x;
			
			RotationDegrees = rotation;
		}
	}
	
	public void SetWeapon(WeaponPlatform newWeapon) {
		SelectedWeapon = newWeapon;
	}
}
