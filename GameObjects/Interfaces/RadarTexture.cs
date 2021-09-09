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
		Godot.Collections.Array<Node> enemyIndicators = new Godot.Collections.Array<Node>(GetTree().GetNodesInGroup("EnemyIndicators"));
		Godot.Collections.Array<Node> allyIndicators = new Godot.Collections.Array<Node>(GetTree().GetNodesInGroup("AllyIndicators"));
		for(int i = 0; i < enemyIndicators.Count; i++) {
			Node indicatorParent = enemyIndicators[i].GetParent();
			if(indicatorParent != null) {
				indicatorParent.RemoveChild(enemyIndicators[i]);
				SpareEnemyIndicators.Push(enemyIndicators[i]);
			}
		}
		for(int i = 0; i < allyIndicators.Count; i++) {
			Node indicatorParent = allyIndicators[i].GetParent();
			if(indicatorParent != null) {
				indicatorParent.RemoveChild(allyIndicators[i]);
				SpareAllyIndicators.Push(allyIndicators[i]);
			}
		}
	}

	private void PopulateIndicators() {

	}

	private Vector2 PositionIndicator(Vector3 indicatorPosition3D) {
		Vector2 position = new Vector2();
		return position;
	}
}
