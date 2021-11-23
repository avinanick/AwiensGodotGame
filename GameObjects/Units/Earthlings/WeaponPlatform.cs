using Godot;
using System;

public class WeaponPlatform : Spatial
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	private Turret CurrentWeapon;
	// May get rid of this weapon scene variable and do some other stuff with just moving
	// current weapon in and out of visibility
	private PackedScene WeaponScene = GD.Load<PackedScene>("res://GameObjects/Units/Earthlings/AntiAirTurret.tscn");
	
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

	public bool RespawnWeapon() {
		// This should check if the current weapon is destroyed first
		if(CurrentWeapon == null | !Godot.Object.IsInstanceValid(CurrentWeapon)) {
			Turret newWeapon = WeaponScene.Instance<Turret>();
			AddChild(newWeapon);
			newWeapon.GlobalTransform = GlobalTransform;
			CurrentWeapon = newWeapon;
			return true;
		}
		return false;
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
