using Godot;
using System;

public class RadarTexture : TextureRect
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	[Export]
	private PackedScene EnemyIndicator = (PackedScene)GD.Load("res://GameObjects/Interfaces/EnemyRadarIndicator.tscn");
	[Export]
	private PackedScene AllyIndicator = (PackedScene)GD.Load("res://GameObjects/Interfaces/AllyRadarIndicator.tscn");

	private Light2D PlayerView;
	private System.Collections.Stack SpareAllyIndicators = new System.Collections.Stack();
	private System.Collections.Stack SpareEnemyIndicators = new System.Collections.Stack();


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		PlayerView = GetNode<Light2D>("PlayerView");
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

	private void ClearIndicators() {

	}

	private void PopulateIndicators() {

	}

	private Vector2 PositionIndicator(Vector3 indicatorPosition3D) {
		Vector2 position = new Vector2();
		return position;
	}
}
