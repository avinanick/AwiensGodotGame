using Godot;
using System;

public class AlienBomber : CombatShip
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    [Export]
    protected float BombCooldown = 2f;
    [Export]
    protected PackedScene BombScene = GD.Load<PackedScene>("res://GameObjects/Units/Aliens/AlienMissile.tscn");

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public void BombCooldownFinished() {

    }

    public void BombLaunched() {

    }
}
