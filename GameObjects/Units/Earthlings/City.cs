using Godot;
using System;

public class City : Spatial
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	[Export]
	private int TotalPopulation = 1000;

	private bool[] DestroyedBuildings = new bool[4] {false, false, false, false};
	private int[] LowestBuildingHealth;
	private int TotalMaxHealth = 0;
	
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
		LowestBuildingHealth = new int[GetChildCount() - 2];
		Godot.Collections.Array<Node> children = new Godot.Collections.Array<Node>(GetChildren());
		int index = 0;
		for(int i = 0; i < children.Count; i++) {
			if(children[i] is Destructible building) {
				LowestBuildingHealth[index] = building.GetMaxHealth();
				index++;
			}
		}
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
		if(newValue < LowestBuildingHealth[buildingPosition]) {
			LowestBuildingHealth[buildingPosition] = newValue;
		}
		if(newValue <= 0) {
			DestroyedBuildings[buildingPosition] = true;
			if(!Array.Exists(DestroyedBuildings, element => element == false)) {
				// The player has lost
				EmitSignal(nameof(PlayerDefeated));
			}
		}
	}
	
	public void WaveTimeFinished() {
		// This method should also calculate the final population that evacuated.
		int remainingHealth = 0;
		for(int i = 0; i < LowestBuildingHealth.Length; i++) {
			remainingHealth += LowestBuildingHealth[i];
		}
		// Here let the campaign tracker know how many points were earned
		int remainingPopulation = TotalPopulation - ((TotalPopulation * remainingHealth) / TotalMaxHealth);
		GetNode<CampaignTracker>("/root/CampaignTrackerAL").SetLevelPoints(remainingPopulation);
		if(Array.Exists(DestroyedBuildings, element => element == false)) {
			EmitSignal(nameof(PlayerVictory));
		}
	}
}
