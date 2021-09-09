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

	public void BuildingDamaged(int newValue, int position) {
		GetNode<MainOverlay>("MainOverlay").StructureHealthChanged(newValue, position);
	}

	public void OnLevelEnded() {

	}

	public void OnLevelStarted() {
		GetNode<MainOverlay>("MainOverlay").StartLevel();
	}

	public void PlayerDefeat() {
		GetNode<DefeatInterface>("DefeatInterface").PlayerDefeated();
	}

	public void PlayerVictory() {
		GetNode<VictoryInterface>("VictoryInterface").PlayerVictory();
	}
	
	public void TurretDamaged(int newValue, int position) {
		GetNode<MainOverlay>("MainOverlay").TurretHealthChanged(newValue, position);
	}

	public void UnitSpawned(Destructible newUnit) {
		GetNode<MainOverlay>("MainOverlay").UnitSpawned(newUnit);
	}
}
