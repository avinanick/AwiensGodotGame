using Godot;
using System;

public class City : Spatial
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	[Export]
	private int TotalPopulation = 1000;

	bool[] DestroyedBuildings = new bool[4] {false, false, false, false};
	
	[Signal]
	public delegate void BuildingHealthChanged(int newValue, int buildingPosition);
	
	[Signal]
	public delegate void PlayerDefeated();
	
	[Signal]
	public delegate void PlayerVictory();

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

	public void BuildingDestroyed() {
		
	}
	
	public void BuildingHealthUpdated(int newValue, int buildingPosition) {
		EmitSignal(nameof(BuildingHealthChanged), newValue, buildingPosition);
		if(newValue <= 0) {
			// Handle some sort of death tally here
			DestroyedBuildings[buildingPosition] = true;
			if(!Array.Exists(DestroyedBuildings, element => element == false)) {
				// The player has lost
				EmitSignal(nameof(PlayerDefeated));
			}
		}
	}
	
	public void WaveTimeFinished() {
		GD.Print("Level time is up!");
		if(Array.Exists(DestroyedBuildings, element => element == false)) {
			GD.Print("Player should have won");
			EmitSignal(nameof(PlayerVictory));
		}
	}
}
