using Godot;
using System;

public class RadarTexture : TextureRect
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	[Export]
	private PackedScene IndicatorScene = (PackedScene)GD.Load("res://GameObjects/Interfaces/RadarIndicator.tscn");

	private Light2D PlayerView;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		PlayerView = GetNode<Light2D>("PlayerView");
		CallDeferred("PopulateIndicators");
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

	public void DestructibleSpawned(Destructible newUnit) {
		RadarIndicator newIndicator = (RadarIndicator)IndicatorScene.Instance();
		AddChild(newIndicator);
		newIndicator.AssignUnit(newUnit);
	}

	private void PopulateIndicators() {
		Godot.Collections.Array<Destructible> units = new Godot.Collections.Array<Destructible>(GetTree().GetNodesInGroup("Destructible"));
		for(int i = 0; i < units.Count; i++) {
			DestructibleSpawned(units[i]);
		}
	}
}
