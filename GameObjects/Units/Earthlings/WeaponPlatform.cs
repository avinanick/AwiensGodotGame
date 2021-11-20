using Godot;
using System;

public class WeaponPlatform : Spatial
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	private Turret CurrentWeapon;
	
	[Signal]
	public delegate void WeaponHealthChanged(int newValue);

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
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

	public void DeployAddon(Turret addon) {
		// If there is already an addon deployed, remove it before deploying this one
		Spatial addonSlot = GetNode<Spatial>("AddonSlot");
		if(addonSlot.GetChildCount() > 0) {
			addonSlot.GetChild(0).QueueFree();
		}
		addonSlot.AddChild(addon);
		addon.GlobalTransform = addonSlot.GlobalTransform;
	}
	
	public void ResetSights() {
		CurrentWeapon.ResetSights();
	}
	
	public void Sight(float xRotation, float yRotation) {
		CurrentWeapon.Sight(xRotation, yRotation);
	}

	public void SwitchWeapon(string weaponPath) {
		
	}
	
	public void OnWeaponHealthChanged(int newValue) {
		EmitSignal(nameof(WeaponHealthChanged), newValue);
	}
}
