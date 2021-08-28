using Godot;
using System;

public class Turret : Destructible
{

	protected AttackerComponent Attacker;
	protected TurretModel Model;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Attacker = GetNode<AttackerComponent>("AttackerComponent");
		Model = GetNode<TurretModel>("GunModel");
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

	public void AttemptAttack(Vector3 attackSpawn, Vector3 attackRotation) {
		Attacker.Fire(attackSpawn, attackRotation);
	}
	
	public void ResetSights() {
		Model.ResetSights();
	}
	
	public void Sight(float xRotation, float yRotation) {
		Model.Sight(xRotation, yRotation);
	}
}
