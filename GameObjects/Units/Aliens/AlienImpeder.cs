using Godot;
using System;

public class AlienImpeder : CombatShip
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Export]
    protected float HoverDistance = 5f;

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
        EndImpedence();
    }

    protected void EndImpedence() {
        // I'll probably call this in the destroy animation player
        if(CurrentTarget != null & Godot.Object.IsInstanceValid(CurrentTarget)) {
            if(CurrentTarget is Turret turretTarget) {
                turretTarget.EnableTurret();
                // Need some sort of visual/animation
            }
        }
    }

    protected override void HoverDestinationReached() {
        // Start up the impedence
        base.HoverDestinationReached();
        StartImpedence();
    }

    protected override void SetHoverLocation()
    {
        if(CurrentTarget != null & Godot.Object.IsInstanceValid(CurrentTarget)) {
            base.SetHoverLocation();
            Vector3 hoverOffsetDirection = (GlobalTransform.origin - CurrentTarget.GlobalTransform.origin).Normalized();
            HoverLocation = CurrentTarget.GlobalTransform.origin + HoverDistance * hoverOffsetDirection;
        }
    }

    protected void StartImpedence() {
        if(CurrentTarget != null & Godot.Object.IsInstanceValid(CurrentTarget)) {
            if(CurrentTarget is Turret turretTarget) {
                turretTarget.DisableTurret();
                // Need some sort of visual/animation
                GetNode<AnimationPlayer>("AnimationPlayer").Play("StartDisable");
            }
        }
    }
}
