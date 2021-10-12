using Godot;
using System;

public class AlienScout : CombatShip
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Export]
    protected float HoverDistance = 9f;

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

    public override void DestroySelf() {
        base.DestroySelf();
        EndTargeting();
    }

    protected void EndTargeting() {
        
    }

    protected override void HoverDestinationReached() {
        // Start up the impedence
        base.HoverDestinationReached();
        StartTargeting();
    }

    protected override void SetHoverLocation()
    {
        if(CurrentTarget != null & Godot.Object.IsInstanceValid(CurrentTarget)) {
            base.SetHoverLocation();
            Vector3 hoverOffsetDirection = (GlobalTransform.origin - CurrentTarget.GlobalTransform.origin).Normalized();
            HoverLocation = CurrentTarget.GlobalTransform.origin + HoverDistance * hoverOffsetDirection;
        }
    }

    protected void StartTargeting() {
        
    }
}
