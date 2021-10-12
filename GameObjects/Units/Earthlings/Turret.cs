using Godot;
using System;

public class Turret : Destructible
{

	protected AttackerComponent Attacker;
	protected bool TurretDisabled = false;
	protected TurretModel Model;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		Attacker = GetNode<AttackerComponent>("AttackerComponent");
		Model = GetNode<TurretModel>("GunModel");
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

	public void AttemptAttack(Vector3 attackSpawn, Vector3 attackRotation) {
		if(!TurretDisabled) {
			Attacker.Fire(attackSpawn, attackRotation);
		}
	}

	public void DisableTurret() {
		TurretDisabled = true;
		// May want to add an animation?
	}

	public void EnableTurret() {
		TurretDisabled = false;
	}
	
	public void ResetSights() {
		Model.ResetSights();
	}
	
	public void Sight(float xRotation, float yRotation) {
		if(!TurretDisabled) {
			Model.Sight(xRotation, yRotation);
		}
	}
}
