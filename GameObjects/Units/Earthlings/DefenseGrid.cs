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

	public void SelectWeapon() {
		
	}
	
	public void WeaponDamaged() {
		
	}
	
	public void WeaponDestroyed() {
		
	}
}
