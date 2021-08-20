using Godot;
using System;

public class AAGunModel : Spatial
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	Spatial ShouldersBone;
	Spatial HeadBone;
	Spatial BaseBone;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ShouldersBone = GetNode<Spatial>("Bone0/Bone1");
		HeadBone = GetNode<Spatial>("Bone0/Bone2");
		BaseBone = GetNode<Spatial>("Bone0");
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

	public void Sight(float XRotation, float YRotation) {
		Vector3 horizontalRotation = BaseBone.RotationDegrees;
		horizontalRotation.x -= XRotation;
		Vector3 verticalRotation = ShouldersBone.RotationDegrees;
		verticalRotation.y -= YRotation;
		
		BaseBone.RotationDegrees = horizontalRotation;
		ShouldersBone.RotationDegrees = verticalRotation;
		HeadBone.RotationDegrees = verticalRotation;
	}
	
	public void ResetSights() {		
		ShouldersBone.RotationDegrees = new Vector3(0,0,0);
		HeadBone.RotationDegrees = new Vector3(0,0,0);
		BaseBone.RotationDegrees = new Vector3(0,0,0);
	}
}
