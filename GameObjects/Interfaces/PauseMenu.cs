using Godot;
using System;

public class PauseMenu : PanelContainer
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		Visible = false;
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

	public void OnAbandonCity() {
		// Here, the current save file should be deleted and the player taken to
		// the main menu
		GD.Print("Not yet functional");
	}

	public void OnPause() {
		Visible = true;
		GetTree().Paused = true;
		Input.SetMouseMode(Input.MouseMode.Visible);
	}
	
	public void OnUnpause() {
		Visible = false;
		GetTree().Paused = false;
		Input.SetMouseMode(Input.MouseMode.Captured);
	}
}
