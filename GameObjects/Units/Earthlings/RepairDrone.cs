using Godot;
using System;

public class RepairDrone : CombatShip
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Export]
    protected int HealAmount = 1;

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

    protected void EndRepairChannel() {
        // End the animation and search for a new target
    }

    protected override void HoverDestinationReached()
    {
        base.HoverDestinationReached();
        StartRepairChannel();
    }

    protected void PulseHeal() {
        if(HoverLocationReached) {
            if(CurrentTarget != null & Godot.Object.IsInstanceValid(CurrentTarget)) {
                if(CurrentTarget is Destructible destructible) {
                    destructible.RepairDamage(HealAmount);
                }
                if(CurrentTarget.IsFullHealth()) {
                    // Choose a new target
                    EndRepairChannel();
                }
                else {
                    GetNode<Timer>("Timer").Start();
                }
            }
        }
    }

    protected override void SetHoverLocation() {
        if(CurrentTarget != null & Godot.Object.IsInstanceValid(CurrentTarget)) {
            Vector3 location = CurrentTarget.GlobalTransform.origin;
            location.y += 10;
            HoverLocation = location;
            EndRepairChannel();
        }
    }

    protected void StartRepairChannel() {
        PulseHeal();
        // Should also start some animation
    }

    protected override void UpdateTarget()
    {
        if(CurrentTarget == null || !Godot.Object.IsInstanceValid(CurrentTarget) || CurrentTarget.IsFullHealth()) {
            HoverLocationReached = false;
            if(PotentialTargets.Count > 0) {
                CurrentTarget = PickTarget(PotentialTargets);
                if(CurrentTarget != null) {
                    if(AttackPattern == AttackPatternType.Hover) {
                        SetProcess(true);
                        SetHoverLocation();
                    }
                }
                else {
                    GetNode<Timer>("RetryUpdateTimer").Start();
                    SetProcess(false);
                }
                
            }
        }
    }

    protected Destructible PickTarget(Godot.Collections.Array<Destructible> potentialTargets) {
        potentialTargets.Shuffle();
        for(int i = 0; i < potentialTargets.Count; i++) {
            if(potentialTargets[i] != null & Godot.Object.IsInstanceValid(potentialTargets[i]) &!potentialTargets[i].IsFullHealth()) {
                return potentialTargets[i];
            }
        }
        return null; // STUB
    }
}