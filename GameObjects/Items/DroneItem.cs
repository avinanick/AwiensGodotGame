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
        CombatShip drone = GetNode<CombatShip>("Drone");
        Transform droneTransform = drone.GlobalTransform;
        RemoveChild(drone);
        GetTree().CurrentScene.AddChild(drone);
        drone.GlobalTransform = droneTransform;
        drone.SpawnShip();
        QueueFree();
    }
}
