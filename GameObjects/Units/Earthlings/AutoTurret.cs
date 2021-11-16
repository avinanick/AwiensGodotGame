using Godot;
using System;

public class AutoTurret : Turret
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private Ship CurrentTarget = null;
    private bool AttackReady = true;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();
    }
    
    public override void _Process(float delta) {
        base._Process(delta);
    }

    protected void AttemptAttack() {
        // If there is a target, cast a ray toward it and make sure there's nothing in
        // the way, if there is, either wait or try to find a new target. If there isn't,
        // deal damage, it will just be a laser.
        if(AttackReady & CurrentTarget != null) {

        }
    }

    protected void FindTarget() {
        // Search for a new target in range.
        Godot.Collections.Array<Ship> potentialTargets = new Godot.Collections.Array<Ship>(GetNode<Area>("AttackRange").GetOverlappingBodies());
        for(int i = 0; i < potentialTargets.Count; i++) {
            if(Godot.Object.IsInstanceValid(potentialTargets[i]) & potentialTargets[i].IsInGroup("Aliens") & potentialTargets[i] != CurrentTarget) {
                CurrentTarget = potentialTargets[i];
                return;
            }
        }
    }

    public void OnTargetEnterRange(Node body) {
        if(body.IsInGroup("Aliens") & (!Godot.Object.IsInstanceValid(CurrentTarget) || CurrentTarget == null)) {
            CurrentTarget = (Ship)body;
        }
    }

    protected void TrackTarget() {

    }
}