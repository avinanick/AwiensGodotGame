using Godot;
using System;

public class InterfaceManager : MarginContainer
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

	public void BuildingDamaged(int position) {
		
	}
	
	public void GamePaused() {
		GD.Print("Receiving game pause signal");
		GetNode<PauseMenu>("PauseMenu").OnPause();
	}

	public void PlayerDefeat() {
		
	}

	public void PlayerVictory() {
		
	}
	
	public void TurretDamaged(int position) {
		
	}
}
