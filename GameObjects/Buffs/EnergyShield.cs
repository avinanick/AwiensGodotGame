using Godot;
using System;

public class EnergyShield : Destructible
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Export]
    protected float OverloadTime = 5f;
    [Export]
    protected float RecoveryTime = 1f;
    [Export]
    protected float RecoveryPerSecond = 1f;
    protected bool Overloaded = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public void EnableShield() {
        GetNode<CollisionShape>("CollisionShape").Disabled = false;
    }

    public void OverloadRecoveryCompleted() {
        Health = MaxHealth;
        Overloaded = false;
        GetNode<Timer>("RecoveryPeriodTimer").Start(RecoveryTime);
        GetNode<CollisionShape>("CollisionShape").Disabled = false;
    }

    public void RecoveryPeriod() {
        if(!Overloaded) {
            if(Health + RecoveryPerSecond <= MaxHealth) {
                Health += (int)RecoveryPerSecond;
            }
            GetNode<Timer>("RecoveryPeriodTimer").Start(RecoveryTime);
        }
    }

    public void SetHealthModifier(float modifier) {
        Health = (int)(Health * 0.8);
        MaxHealth = (int)(MaxHealth * 0.8);
    }

    public override void TakeDamage(int amount) {
        Health -= (int)((double)amount * DamageModifier);
		EmitSignal(nameof(HealthChanged), Health);
		if(Health <= 0) {
			Overloaded = true;
            GetNode<Timer>("OverloadTimer").Start(OverloadTime);
            // Here I'll need to play the breaking animation
            GetNode<AnimationPlayer>("AnimationPlayer").Play("ShieldShatter");
            GetNode<CollisionShape>("CollisionShape").Disabled = true;
		}
        else {
            // play the damage taken animation
            GetNode<AnimationPlayer>("AnimationPlayer").Play("DamageImpact");
        }
    }
}
