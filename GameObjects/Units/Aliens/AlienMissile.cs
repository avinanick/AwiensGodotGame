using Godot;
using System;

public class AlienMissile : Ship
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	[Export]
	private int MissileDamage = 1;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
//	public override void _Process(float delta)
//	{
//		base._Process(delta);
//	}

	public override void HandleCollision(KinematicCollision collision) {
		if(collision != null) {
			if(collision.Collider is Destructible destructible) {
				destructible.TakeDamage(MissileDamage);
			}
			DestroySelf();
		}
	}
	
	public override void SpawnShip() {
		// Here I should pick a random earthling and find the direction to it
		Godot.Collections.Array earthlings = GetTree().GetNodesInGroup("Earthling");
		Godot.Collections.Array<Spatial> targets = new Godot.Collections.Array<Spatial>(earthlings);
		int randomIndex = RNG.Next(0, targets.Count);
		InitializeDirection(targets[randomIndex].GlobalTransform.origin - GlobalTransform.origin);		
	}
}
