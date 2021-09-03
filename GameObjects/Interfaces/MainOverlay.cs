using Godot;
using System;

public class MainOverlay : MarginContainer
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	private TextureProgress NorthCityBar;
	private TextureProgress EastCityBar;
	private TextureProgress WestCityBar;
	private TextureProgress SouthCityBar;
	private TextureProgress NorthTurretBar;
	private TextureProgress EastTurretBar;
	private TextureProgress WestTurretBar;
	private TextureProgress SouthTurretBar;
	private TextureProgress CityShieldBar;
	private TextureProgress NorthTurretShieldBar;
	private TextureProgress EastTurretShieldBar;
	private TextureProgress WestTurretShieldBar;
	private TextureProgress SouthTurretShieldBar;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		
		NorthTurretBar = GetNode<TextureProgress>("HealthSpacing/HealthBars/CentralBars/NorthTurretHealthBar");
		EastTurretBar = GetNode<TextureProgress>("HealthSpacing/HealthBars/EastTurretHealthBar");
		WestTurretBar = GetNode<TextureProgress>("HealthSpacing/HealthBars/WestTurretHealthBar");
		SouthTurretBar = GetNode<TextureProgress>("HealthSpacing/HealthBars/CentralBars/SouthTurretHealthBar");
		
		NorthCityBar = GetNode<TextureProgress>("HealthSpacing/HealthBars/CentralBars/CityShieldHealthBar/NorthWestCityBar");
		EastCityBar = GetNode<TextureProgress>("HealthSpacing/HealthBars/CentralBars/CityShieldHealthBar/NorthEastCityBar");
		WestCityBar = GetNode<TextureProgress>("HealthSpacing/HealthBars/CentralBars/CityShieldHealthBar/SouthWestCityBar");
		SouthCityBar = GetNode<TextureProgress>("HealthSpacing/HealthBars/CentralBars/CityShieldHealthBar/SouthEastCityBar");
		
		NorthTurretShieldBar = GetNode<TextureProgress>("HealthSpacing/HealthBars/CentralBars/NorthTurretHealthBar/NorthShieldHealthBar");
		EastTurretShieldBar = GetNode<TextureProgress>("HealthSpacing/HealthBars/EastTurretHealthBar/EastShieldHealthBar");
		WestTurretShieldBar = GetNode<TextureProgress>("HealthSpacing/HealthBars/WestTurretHealthBar/WestShieldHealthBar");
		SouthTurretShieldBar = GetNode<TextureProgress>("HealthSpacing/HealthBars/CentralBars/SouthTurretHealthBar/SouthShieldHealthBar");
		CityShieldBar = GetNode<TextureProgress>("HealthSpacing/HealthBars/CentralBars/CityShieldHealthBar");
		
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

	public void EndLevel() {
		// This function is mainly used for just the visible timer
	}
	
	public void StartLevel() {
		// This function is mainly used for just the visible timer, and possibly
		// for resetting the health bars
	}
	
	public void StructureHealthChanged(int newValue, int position) {
		// Should I set up a tween for this?
		// also these values are likely off
		switch(position) {
			case DefenseGrid.NORTH:
				NorthCityBar.Value = newValue;
				GD.Print("Modifying north city building bar");
				break;
			case DefenseGrid.EAST:
				EastCityBar.Value = newValue;
				GD.Print("Modifying east city building bar");
				break;
			case DefenseGrid.WEST:
				WestCityBar.Value = newValue;
				GD.Print("Modifying west city building bar");
				break;
			case DefenseGrid.SOUTH:
				SouthCityBar.Value = newValue;
				GD.Print("Modifying south city building bar");
				break;
		}
	}
	
	public void TurretHealthChanged(int newValue, int position) {
		// Should I set up a tween for this?
		// also these values are likely off
		switch(position) {
			case DefenseGrid.NORTH:
				NorthTurretBar.Value = newValue;
				GD.Print("Modifying north turret building bar");
				break;
			case DefenseGrid.EAST:
				EastTurretBar.Value = newValue;
				GD.Print("Modifying east turret building bar");
				break;
			case DefenseGrid.WEST:
				WestTurretBar.Value = newValue;
				GD.Print("Modifying west turret building bar");
				break;
			case DefenseGrid.SOUTH:
				SouthTurretBar.Value = newValue;
				GD.Print("Modifying south turret building bar");
				break;
		}
	}
}
