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
	private Label CountdownLabel;
	private RadarTexture Radar;

	private float LevelTimeRemaining = 30f;
	private bool LevelRunning = false;

	[Signal]
	public delegate void LevelTimeEnded();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		
		NorthTurretBar = GetNode<TextureProgress>("VBoxContainer/HealthSpacing/HealthBars/CentralBars/NorthTurretHealthBar");
		EastTurretBar = GetNode<TextureProgress>("VBoxContainer/HealthSpacing/HealthBars/EastTurretHealthBar");
		WestTurretBar = GetNode<TextureProgress>("VBoxContainer/HealthSpacing/HealthBars/WestTurretHealthBar");
		SouthTurretBar = GetNode<TextureProgress>("VBoxContainer/HealthSpacing/HealthBars/CentralBars/SouthTurretHealthBar");
		
		NorthCityBar = GetNode<TextureProgress>("VBoxContainer/HealthSpacing/HealthBars/CentralBars/CityShieldHealthBar/NorthWestCityBar");
		EastCityBar = GetNode<TextureProgress>("VBoxContainer/HealthSpacing/HealthBars/CentralBars/CityShieldHealthBar/NorthEastCityBar");
		WestCityBar = GetNode<TextureProgress>("VBoxContainer/HealthSpacing/HealthBars/CentralBars/CityShieldHealthBar/SouthWestCityBar");
		SouthCityBar = GetNode<TextureProgress>("VBoxContainer/HealthSpacing/HealthBars/CentralBars/CityShieldHealthBar/SouthEastCityBar");
		
		NorthTurretShieldBar = GetNode<TextureProgress>("VBoxContainer/HealthSpacing/HealthBars/CentralBars/NorthTurretHealthBar/NorthShieldHealthBar");
		EastTurretShieldBar = GetNode<TextureProgress>("VBoxContainer/HealthSpacing/HealthBars/EastTurretHealthBar/EastShieldHealthBar");
		WestTurretShieldBar = GetNode<TextureProgress>("VBoxContainer/HealthSpacing/HealthBars/WestTurretHealthBar/WestShieldHealthBar");
		SouthTurretShieldBar = GetNode<TextureProgress>("VBoxContainer/HealthSpacing/HealthBars/CentralBars/SouthTurretHealthBar/SouthShieldHealthBar");
		CityShieldBar = GetNode<TextureProgress>("VBoxContainer/HealthSpacing/HealthBars/CentralBars/CityShieldHealthBar");
		
		CountdownLabel = GetNode<Label>("VBoxContainer/Counters/Counter/Background/HBoxContainer/TimerLabel");

		Radar = GetNode<RadarTexture>("VBoxContainer/HealthSpacing/HealthBars/RadarSetup/central_radar/RadarTexture");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		base._Process(delta);
		if(LevelRunning) {
			LevelTimeRemaining -= delta;
			CountdownLabel.Text = ((int)LevelTimeRemaining).ToString();
			if(LevelTimeRemaining <= 0) {
				EndLevel();
			}
		}
	}

	public void EndLevel() {
		// This function should emit a signal that will let everyone else know the level has ended
		LevelRunning = false;
		EmitSignal(nameof(LevelTimeEnded));
	}
	
	public void StartLevel() {
		// This function is mainly used for just the visible timer, and possibly
		// for resetting the health bars
		LevelRunning = true;
	}
	
	public void StructureHealthChanged(int newValue, int position) {
		// Should I set up a tween for this?
		// also these values are likely off
		switch(position) {
			case DefenseGrid.NORTH:
				NorthCityBar.Value = newValue * 10;
				break;
			case DefenseGrid.EAST:
				EastCityBar.Value = newValue * 10;
				break;
			case DefenseGrid.WEST:
				WestCityBar.Value = newValue * 10;
				break;
			case DefenseGrid.SOUTH:
				SouthCityBar.Value = newValue * 10;
				break;
		}
	}
	
	public void TurretHealthChanged(int newValue, int position) {
		// Should I set up a tween for this?
		// also these values are likely off
		switch(position) {
			case DefenseGrid.NORTH:
				NorthTurretBar.Value = newValue * 10;
				break;
			case DefenseGrid.EAST:
				EastTurretBar.Value = newValue * 10;
				break;
			case DefenseGrid.WEST:
				WestTurretBar.Value = newValue * 10;
				break;
			case DefenseGrid.SOUTH:
				SouthTurretBar.Value = newValue * 10;
				break;
		}
	}

	public void UnitSpawned(Destructible newUnit) {
		Radar.DestructibleSpawned(newUnit);
	}
}