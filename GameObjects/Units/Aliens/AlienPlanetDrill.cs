using Godot;
using System;

public class AlienPlanetDrill : CombatShip
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

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

    public void EndAttackChannel() {
        
    }

    public void PulseDamage() {

    }

    protected override void SetHoverLocation() {
        if(CurrentTarget != null & Godot.Object.IsInstanceValid(CurrentTarget)) {
            Vector3 location = CurrentTarget.GlobalTransform.origin;
            location.y += 20;
            HoverLocation = location;
        }
    }

    public void StartAttackChannel() {

    }
}
