using Godot;
using System;

public class AAGunModel : Spatial
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

	public void Sight(float XRotation, float YRotation) {
		Spatial shouldersBone = GetNode<Spatial>("Bone0/Bone1");
		Spatial headBone = GetNode<Spatial>("Bone0/Bone2");
		Spatial baseBone = GetNode<Spatial>("Bone0");
		
		Vector3 horizontalRotation = baseBone.RotationDegrees;
		horizontalRotation.x -= XRotation;
		Vector3 verticalRotation = shouldersBone.RotationDegrees;
		verticalRotation.y -= YRotation;
		
		baseBone.RotationDegrees = horizontalRotation;
		shouldersBone.RotationDegrees = verticalRotation;
		headBone.RotationDegrees = verticalRotation;
	}
	
	public void ResetSights() {
		Spatial shouldersBone = GetNode<Spatial>("Bone0/Bone1");
		Spatial headBone = GetNode<Spatial>("Bone0/Bone2");
		Spatial baseBone = GetNode<Spatial>("Bone0");
		
		shouldersBone.RotationDegrees = new Vector3(0,0,0);
		headBone.RotationDegrees = new Vector3(0,0,0);
		baseBone.RotationDegrees = new Vector3(0,0,0);
	}
}
