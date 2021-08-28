using Godot;
using System;

public class AAGunModel : TurretModel
{
	// Note: I may want to make a base "GunModel" class that others inherit 
	// from, for ease of use in the turret scene. Either that or an interface
	protected Spatial HeadBone;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GunBone = GetNode<Spatial>("Bone0/Bone1");
		BaseBone = GetNode<Spatial>("Bone0");
		HeadBone = GetNode<Spatial>("Bone0/Bone2");
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

	public override void Sight(float XRotation, float YRotation) {
		Vector3 horizontalRotation = BaseBone.RotationDegrees;
		horizontalRotation.x -= XRotation;
		Vector3 verticalRotation = GunBone.RotationDegrees;
		verticalRotation.y -= YRotation;
		
		BaseBone.RotationDegrees = horizontalRotation;
		GunBone.RotationDegrees = verticalRotation;
		HeadBone.RotationDegrees = verticalRotation;
	}
	
	public override void ResetSights() {		
		GunBone.RotationDegrees = new Vector3(0,0,0);
		HeadBone.RotationDegrees = new Vector3(0,0,0);
		BaseBone.RotationDegrees = new Vector3(0,0,0);
	}
}
