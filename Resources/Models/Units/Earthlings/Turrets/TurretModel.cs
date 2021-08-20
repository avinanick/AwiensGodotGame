using Godot;
using System;

public class TurretModel : Spatial
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	private Spatial BaseBone;
	private Spatial GunBone;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GunBone = GetNode<Spatial>("Bone0/Bone1");
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
		Vector3 verticalRotation = GunBone.RotationDegrees;
		verticalRotation.y -= YRotation;
		
		BaseBone.RotationDegrees = horizontalRotation;
		GunBone.RotationDegrees = verticalRotation;
	}
	
	public void ResetSights() {		
		GunBone.RotationDegrees = new Vector3(0,0,0);
		BaseBone.RotationDegrees = new Vector3(0,0,0);
	}
}
