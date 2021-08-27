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
		NorthPosition = new Vector3(0,0,15);
		EastPosition = new Vector3(15,0,0);
		WestPosition = new Vector3(-15,0,0);
		SouthPosition = new Vector3(0,0,-15);
		PlayerAvatar = GetNode<Avatar>("Avatar");
		NorthPlatform = GetNode<WeaponPlatform>("WeaponPlatform3");
		EastPlatform = GetNode<WeaponPlatform>("WeaponPlatform");
		WestPlatform = GetNode<WeaponPlatform>("WeaponPlatform2");
		SouthPlatform = GetNode<WeaponPlatform>("WeaponPlatform4");
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
				break;
			case EAST:
				PlayerAvatar.Translation = EastPosition;
				break;
			case WEST:
				PlayerAvatar.Translation = WestPosition;
				break;
			case SOUTH:
				PlayerAvatar.Translation = SouthPosition;
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
