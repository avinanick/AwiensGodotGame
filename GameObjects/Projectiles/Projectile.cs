using Godot;
using System;

public class Projectile : KinematicBody
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	protected Vector3 DirectionVector;
	protected int Damage;
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

	private void DestroySelf() {
		QueueFree();
	}

	private void HandleImpact(KinematicCollision collision) {
		if(collision != null) {
			if(collision.Collider is Destructible destructible) {
				destructible.TakeDamage(Damage);
			}
			DestroySelf();
		}
	}

	public void SetDamage(int damageAmount) {
		Damage = damageAmount;
	}

	public void SetDirection(Vector3 newDirection) {
		DirectionVector = newDirection.Normalized();
	}
}
