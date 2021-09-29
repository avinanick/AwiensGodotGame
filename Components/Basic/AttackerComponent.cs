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
	private PackedScene AttackProjectile;
	
	[Export]
	private float Cooldown = 1.0f;
	
	private bool ReadyToFire = true;

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

	public void CooldownFinished() {
		ReadyToFire = true;
	}

	public void Fire(Vector3 startPosition, Vector3 startRotation) {
		if(ReadyToFire) {
			ReadyToFire = false;
			GetNode<Timer>("CooldownTimer").Start(Cooldown);
			Projectile newBullet = (Projectile)AttackProjectile.Instance();
			
			Vector3 directionVector = new Vector3();
			directionVector.x = (float)((-1) * Mathf.Sin(startRotation.y) * Mathf.Cos(startRotation.x));
			directionVector.y = (float)Mathf.Sin(startRotation.x);
			directionVector.z = (float)((-1) * Mathf.Cos(startRotation.y) * Mathf.Cos(startRotation.x));
			
			newBullet.Translation = startPosition + 2 * directionVector.Normalized();
			newBullet.Rotation = startRotation;
			newBullet.SetDirection(directionVector.Normalized());
			newBullet.SetDamage(Damage);
			
			GetTree().CurrentScene.AddChild(newBullet);
		}
	}

	public void FireAt(Vector3 targetPosition, Vector3 unitPosition) {
		if(ReadyToFire) {
			ReadyToFire = false;
			GetNode<Timer>("CooldownTimer").Start(Cooldown);
			Projectile newBullet = (Projectile)AttackProjectile.Instance();

			Vector3 directionVector = (targetPosition - unitPosition).Normalized();
			Vector3 spawnPosition = unitPosition + (2 * directionVector);
			newBullet.Translation = spawnPosition;
			newBullet.SetDirection(directionVector);
			newBullet.SetDamage(Damage);

			GetTree().CurrentScene.AddChild(newBullet);
		}
	}
}
