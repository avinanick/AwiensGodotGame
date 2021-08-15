using Godot;
using System;

public class Destructible : KinematicBody
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	[Export]
	private int Health = 10;

	[Export]
	private int MaxHealth = 10;

	private double DamageModifier = 1.0;
	private bool IsAlive = true;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

	public void DestroySelf() {
		// STUB
		QueueFree();
	}
	
	public void RepairDamage(int amount) {
		// STUB
		Health += amount;
		if(Health > MaxHealth) {
			Health = MaxHealth;
		}
	}

	public void TakeDamage(int amount) {
		// STUB
		Health -= amount;
		if(Health <= 0) {
			DestroySelf()
		}
	}
}
