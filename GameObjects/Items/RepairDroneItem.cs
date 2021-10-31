using Godot;
using System;

public class RepairDroneItem : Item
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    PackedScene RepairDroneScene = GD.Load<PackedScene>("res://GameObjects/Units/Earthlings/RepairDrone.tscn");

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    protected override void Activate() {
        base.Activate();
    }
}
