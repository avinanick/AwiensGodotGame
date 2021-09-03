using Godot;
using System;

public class Ship : Destructible
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	[Export]
	protected float ShipSpeed = 5.0f;
	
	protected Vector3 DirectionVector;
	protected Random RNG;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		RNG = new Random();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		base._Process(delta);
		HandleCollision(MoveAndCollide(DirectionVector * ShipSpeed * delta));
	}
	
	public virtual void HandleCollision(KinematicCollision collision) {
		// The base implimentation will do nothing, in the missile version it 
		// explode and damage the target if possible
	}

	public void InitializeDirection(Vector3 direction) {
		DirectionVector = direction.Normalized();
	}
	
	public virtual void SpawnShip() {
		// Base implimentation will do nothing
	}
}
