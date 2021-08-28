using Godot;
using System;

public class DefenseGrid : Spatial
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	private Vector3 NorthPosition;
	private Vector3 EastPosition;
	private Vector3 WestPosition;
	private Vector3 SouthPosition;
	private Avatar PlayerAvatar;
	
	private WeaponPlatform NorthPlatform;
	private WeaponPlatform EastPlatform;
	private WeaponPlatform WestPlatform;
	private WeaponPlatform SouthPlatform;
	
	public const int NORTH = 0;
	public const int EAST = 1;
	public const int WEST = 2;
	public const int SOUTH = 3;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		NorthPosition = new Vector3(0,2,15);
		EastPosition = new Vector3(15,2,0);
		WestPosition = new Vector3(-15,2,0);
		SouthPosition = new Vector3(0,2,-15);
		PlayerAvatar = GetNode<Avatar>("Avatar");
		NorthPlatform = GetNode<WeaponPlatform>("WeaponPlatform3");
		EastPlatform = GetNode<WeaponPlatform>("WeaponPlatform");
		WestPlatform = GetNode<WeaponPlatform>("WeaponPlatform2");
		SouthPlatform = GetNode<WeaponPlatform>("WeaponPlatform4");
		// Default selection should be south platform
		SelectWeapon(SOUTH);
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

	public void SelectWeapon(int weaponPosition) {
		switch(weaponPosition) {
			case NORTH:
				PlayerAvatar.Translation = NorthPosition;
				PlayerAvatar.RotationDegrees = new Vector3(0,180,0);
				NorthPlatform.ResetSights();
				PlayerAvatar.SetWeapon(NorthPlatform);
				break;
			case EAST:
				PlayerAvatar.Translation = EastPosition;
				PlayerAvatar.RotationDegrees = new Vector3(0,-90,0);
				EastPlatform.ResetSights();
				PlayerAvatar.SetWeapon(EastPlatform);
				break;
			case WEST:
				PlayerAvatar.Translation = WestPosition;
				PlayerAvatar.RotationDegrees = new Vector3(0,90,0);
				WestPlatform.ResetSights();
				PlayerAvatar.SetWeapon(WestPlatform);
				break;
			case SOUTH:
				PlayerAvatar.Translation = SouthPosition;
				PlayerAvatar.RotationDegrees = new Vector3(0,0,0);
				SouthPlatform.ResetSights();
				PlayerAvatar.SetWeapon(SouthPlatform);
				break;
		}
		
	}
	
	public void WeaponDamaged(int weaponPosition) {
		switch(weaponPosition) {
			case NORTH:
				break;
			case EAST:
				break;
			case WEST:
				break;
			case SOUTH:
				break;
		}
	}
	
	public void WeaponDestroyed(int weaponPosition) {
		switch(weaponPosition) {
			case NORTH:
				break;
			case EAST:
				break;
			case WEST:
				break;
			case SOUTH:
				break;
		}
	}
}
