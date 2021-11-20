using Godot;
using System;

public class TurretModel : Spatial
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	protected Spatial BaseBone;
	protected Spatial GunBone;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		GunBone = GetNode<Spatial>("Bone0/Bone1");
		BaseBone = GetNode<Spatial>("Bone0");
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

	public virtual void SetSight(Vector3 globalPosition) {
		//BaseBone.RotationDegrees = new Vector3(0, YRotation, 0);
		BaseBone.LookAt(new Vector3(globalPosition.x, BaseBone.GlobalTransform.origin.y, globalPosition.z), new Vector3(0,1,0));
		//GunBone.RotationDegrees = new Vector3(XRotation, 0, 0);
		GunBone.LookAt(globalPosition, new Vector3(0,1,0));
	}

	public virtual void Sight(float XRotation, float YRotation) {
		Vector3 horizontalRotation = BaseBone.RotationDegrees;
		horizontalRotation.x -= XRotation;
		Vector3 verticalRotation = GunBone.RotationDegrees;
		verticalRotation.y -= YRotation;
		
		BaseBone.RotationDegrees = horizontalRotation;
		GunBone.RotationDegrees = verticalRotation;
	}
	
	public virtual void ResetSights() {		
		GunBone.RotationDegrees = new Vector3(0,0,0);
		BaseBone.RotationDegrees = new Vector3(0,0,0);
	}
}
