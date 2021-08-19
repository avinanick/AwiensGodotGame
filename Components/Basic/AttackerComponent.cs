using Godot;
using System;

public class AttackerComponent : Component
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	[Export]
	private int Damage = 1;
	
	[Export]
	private PackedScene Projectile;
	
	[Export]
	private float Cooldown = 1.0f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

	public void Fire(Vector3 startPosition, Vector3 startRotation) {
		
	}
}
