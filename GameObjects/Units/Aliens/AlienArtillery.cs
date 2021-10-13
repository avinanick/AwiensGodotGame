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
        StartBombardment();
    }

    protected override void SetHoverLocation()
    {
        base.SetHoverLocation();
        // Move away from the city a short distance, and lower to height 15
        Vector3 vectorFromCity = (GlobalTransform.origin - new Vector3(0,GlobalTransform.origin.y, 0)).Normalized();
        HoverLocation = vectorFromCity * 10 + new Vector3(0,15,0);
    }

    protected void StartBombardment() {
        Bombarding = true;
        GetNode<Timer>("MissileLaunchTimer").Start();
    }
}
