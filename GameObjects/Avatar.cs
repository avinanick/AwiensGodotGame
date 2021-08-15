using Godot;
using System;

public class Avatar : Spatial
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Input.SetMouseMode(Input.MouseMode.Captured);
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

	public override void _Input(InputEvent inputEvent) {
		if(inputEvent.IsActionPressed("select_north")) {
			GD.Print("Select the north turret");
		}
		if(inputEvent.IsActionPressed("select_east")) {
			GD.Print("Select the east turret");
		}
		if(inputEvent.IsActionPressed("select_west")) {
			GD.Print("Select the west turret");
		}
		if(inputEvent.IsActionPressed("select_south")) {
			GD.Print("Select the south turret");
		}

		if(inputEvent is InputEventMouseMotion motionEvent) {
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

		if(inputEvent.IsActionPressed("pause")) {
			GD.Print("Pause the game");
		}
	}
}
