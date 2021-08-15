using Godot;
using System;

public class HealthComponent : Component
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	[Export]
	private int healthPoints = 10;
	
	[Export]
	private int maxHealth = 10;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

	public void RepairDamage(int amount) {
		healthPoints += amount;
		if(healthPoints > maxHealth) {
			healthPoints = maxHealth;
		}
	}
	
	public void TakeDamage(int amount) {
		healthPoints -= amount;
		if(healthPoints <= 0) {
			GD.Print("Figure out how I want this connected?")
		}
	}
}
