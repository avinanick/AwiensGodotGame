using Godot;
using System;

public class WeaponPlatform : Spatial
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	private Turret CurrentWeapon;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		CurrentWeapon = GetNode<Turret>("AntiAirTurret");
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

	public void AttemptAttack(Vector3 attackSpawn, Vector3 attackRotation) {
		CurrentWeapon.AttemptAttack(attackSpawn, attackRotation);
	}
	
	public void ResetSights() {
		CurrentWeapon.ResetSights();
	}
	
	public void Sight(float xRotation, float yRotation) {
		CurrentWeapon.Sight(xRotation, yRotation);
	}

	public void SwitchWeapon(string weaponPath) {
		
	}
}
