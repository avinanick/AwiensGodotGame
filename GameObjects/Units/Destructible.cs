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
	
	[Signal]
	public delegate void HealthChanged(int newValue);
	[Signal]
	public delegate void Destroyed();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

	public void CompleteDestruction() {
		QueueFree();
	}

	public void DestroySelf() {
		EmitSignal(nameof(Destroyed));
		GetNode<AnimationPlayer>("AnimationPlayer").Play("Destroy");
	}
	
	public void RepairDamage(int amount) {
		Health += amount;
		if(Health > MaxHealth) {
			Health = MaxHealth;
		}
		EmitSignal(nameof(HealthChanged), Health);
	}
	
	public void RepairFull() {
		RepairDamage(MaxHealth);
	}

	public void TakeDamage(int amount) {
		Health -= amount;
		EmitSignal(nameof(HealthChanged), Health);
		if(Health <= 0) {
			DestroySelf();
		}
	}
}
