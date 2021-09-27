using Godot;
using System;

public class CombatShip : Ship
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    protected enum AttackPatternType {
        Orbit,
        Strafe,
        Hover
    }

    [Export]
    protected AttackPatternType AttackPattern = AttackPatternType.Orbit;
    [Export]
    protected float AttackRange = 40;
    [Export]
    protected string[] TargetGroups;

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

    public override void SpawnShip() {

    }

    protected void UpdateShipDirection() {

    }

    protected void UpdateTarget() {

    }
}
