using Godot;
using System;

public class RepairDrone : CombatShip
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    protected override void SetHoverLocation() {
        if(CurrentTarget != null & Godot.Object.IsInstanceValid(CurrentTarget)) {
            Vector3 location = CurrentTarget.GlobalTransform.origin;
            location.y += 10;
            HoverLocation = location;
        }
    }
}
