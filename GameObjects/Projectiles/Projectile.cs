using Godot;
using System;

public class Projectile : KinematicBody
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	protected Vector3 DirectionVector;
	protected float Damage;
	[Export]
	protected float ProjectileSpeed = 5.0f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

	public void SetDamage(float damageAmount) {
		Damage = damageAmount;
	}

	public void SetDirection(Vector3 newDirection) {
		DirectionVector = newDirection.Normalized();
	}
}
