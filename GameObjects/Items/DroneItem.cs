using Godot;
using System;

public class DroneItem : Item
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

    protected override void Activate() {
        base.Activate();
        RepairDrone repairDrone = GetNode<RepairDrone>("Drone");
        Transform droneTransform = repairDrone.GlobalTransform;
        RemoveChild(repairDrone);
        GetTree().CurrentScene.AddChild(repairDrone);
        repairDrone.GlobalTransform = droneTransform;
        repairDrone.SpawnShip();
        QueueFree();
    }
}
