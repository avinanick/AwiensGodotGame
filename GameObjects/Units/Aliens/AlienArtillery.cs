using Godot;
using System;

public class AlienArtillery : CombatShip
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    protected PackedScene MissileScene = GD.Load<PackedScene>("res://GameObjects/Units/Aliens/AlienMissile.tscn");
    protected bool Bombarding = false;

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

    protected void EndBombardment() {

    }

    protected void FireOrdinance() {

    }

    protected override void HoverDestinationReached() {
        base.HoverDestinationReached();
    }

    protected override void SetHoverLocation()
    {
        base.SetHoverLocation();
    }

    protected void StartBombardment() {

    }
}
