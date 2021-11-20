using Godot;
using System;

public class AutoTurret : Turret
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Export]
    private int AttackDamage = 1;
    private Ship CurrentTarget = null;
    private bool AttackReady = true;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();
    }
    
    public override void _Process(float delta) {
        base._Process(delta);
        if(CurrentTarget != null & Godot.Object.IsInstanceValid(CurrentTarget)) {
            TrackTarget();
            AttemptAttack();
        }
        else {
            FindTarget();
        }
    }

    public void AttackCooldownComplete() {
        AttackReady = true;
    }

    protected void AttemptAttack() {
        // If there is a target, cast a ray toward it and make sure there's nothing in
        // the way, if there is, either wait or try to find a new target. If there isn't,
        // deal damage, it will just be a laser.
        if(AttackReady & CurrentTarget != null) {
            Destructible collision = GetNode<RayCast>("RayCast").GetCollider() as Destructible;
            // The if statement checks if it's hitting either the current target or it's shield
            if(collision != null) {
                if((collision == CurrentTarget) | (collision.GetParent() == CurrentTarget)) {
                    AttackReady = false;
                    GetNode<Timer>("AttackCooldownTimer").Start();
                    // Play some sort of animation
                    collision.TakeDamage(AttackDamage);                    
                }
            }
        }
    }

    protected void FindTarget() {
        // Search for a new target in range.
        Godot.Collections.Array potentialTargets = GetNode<Area>("AttackRange").GetOverlappingBodies();
        for(int i = 0; i < potentialTargets.Count; i++) {
            if(potentialTargets[i] is Ship shipTarget)
            {
                if(Godot.Object.IsInstanceValid(shipTarget) & shipTarget.IsInGroup("Alien") & shipTarget != CurrentTarget) {
                    GD.Print("Target acquired");
                    CurrentTarget = shipTarget;
                    return;
                }
                else {
                    GD.Print("Target ship not valid");
                }
            }
        }
    }

    public void OnTargetEnterRange(Node body) {
        if(body.IsInGroup("Aliens") & (!Godot.Object.IsInstanceValid(CurrentTarget) || CurrentTarget == null)) {
            CurrentTarget = (Ship)body;
        }
    }

    protected void TrackTarget() {
        // Get rotation to the target and apply that to the gun model and raycast
        if(CurrentTarget != null) {
            Vector3 directionToTarget = CurrentTarget.GlobalTransform.origin - GlobalTransform.origin;
            float yRotationRadians = Mathf.Atan(directionToTarget.x / directionToTarget.z);
            float xRotationRadians = Mathf.Atan(directionToTarget.y / (new Vector2(directionToTarget.x, directionToTarget.z)).Length());
            if(directionToTarget.z > 0) {
                yRotationRadians += Mathf.Pi;
            }
            GetNode<RayCast>("RayCast").LookAt(CurrentTarget.GlobalTransform.origin, new Vector3(0,1,0));
            GetNode<TurretModel>("GunModel").SetSight(CurrentTarget.GlobalTransform.origin); // This is wrong, I may need a new method in the turret model.
        }
    }
}