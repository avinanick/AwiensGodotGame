using Godot;
using System;

public class Destructible : KinematicBody
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	[Export]
	protected int Health = 10;

	[Export]
	protected int MaxHealth = 10;

	protected double DamageModifier = 1.0;
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

	public void AddHealthModifier(float modifier) {
		Health = (int)(Health * modifier);
		MaxHealth = (int)(MaxHealth * modifier);
	}

	public void CompleteDestruction() {
		QueueFree();
	}

	public virtual void DestroySelf() {
		EmitSignal(nameof(Destroyed));
		GetNode<AnimationPlayer>("AnimationPlayer").Play("Destroy");
	}

	public int GetMaxHealth() {
		return MaxHealth;
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

	public virtual void TakeDamage(int amount) {
		Health -= (int)((double)amount * DamageModifier);
		EmitSignal(nameof(HealthChanged), Health);
		if(Health <= 0) {
			DestroySelf();
		}
	}
}
