using Godot;
using System;

public class CombatShip : Ship
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    protected enum AttackPatternType {
        Orbit,
        Strafe,
        Hover
    }

    [Export]
    protected AttackPatternType AttackPattern = AttackPatternType.Orbit;
    [Export]
    protected float TurnSpeed = 4f;
    [Export]
    protected string[] TargetGroups;
    protected AttackerComponent Weapon;
    protected Destructible CurrentTarget;
    protected Godot.Collections.Array<Destructible> PotentialTargets = new Godot.Collections.Array<Destructible>();
    protected bool ApproachingTarget = true;
    protected float TurnaroundDistance = 40;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();
        Weapon = GetNode<AttackerComponent>("AttackerComponent");
        InitialEnemyPopulation();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        base._Process(delta);
        UpdateShipDirectionOrbit();
        UpdateTarget();
        if(CurrentTarget != null) {
            Weapon.FireAt(CurrentTarget.GetGlobalTransform().origin, GetGlobalTransform().origin);
        }
    }

    public void BodyEnteringRange(Node body) {
        if(body is Destructible potentialTarget) {
            if(potentialTarget == CurrentTarget) {
                CurrentTarget = null;
                return;
            }
            for(int i = 0; i < TargetGroups.Length; i++) {
                if(potentialTarget.IsInGroup(TargetGroups[i])) {
                    PotentialTargets.Add(potentialTarget);
                    return;
                }
            }
        }
    }

    public void BodyExitingRange(Node body) {
        if(body is Destructible potentialTarget) {
            for(int i = 0; i < TargetGroups.Length; i++) {
                if(potentialTarget.IsInGroup(TargetGroups[i])) {
                    PotentialTargets.Remove(potentialTarget);
                    return;
                }
            }
        }
    }

    protected void InitialEnemyPopulation() {
        Godot.Collections.Array<Destructible> startingTargets = new Godot.Collections.Array<Destructible>(GetNode<Area>("AttackRangeArea").GetOverlappingBodies());
        for(int i = 0; i < startingTargets.Count; i++) {
            BodyEnteringRange(startingTargets[i]);
        }
    }

    public override void SpawnShip() {

    }

    protected void UpdateShipDirectionStrafe() {
        // This needs to be a movement pattern that moves toward the city, passes over it
        // continues on, then turns around and repeats.
        // For now I'm  going to try: if approaching the city and not currently facing it, 
        // turn to face the city. If approaching and facing, move toward the city until reaching
        // it. If not approaching, move away until reaching a distance of 40
        if(ApproachingTarget) {
            Vector3 directionToCity = new Vector3(0, GlobalTransform.origin.y, 0) - GlobalTransform.origin;
            if(directionToCity.Length() <= 0.1) {
                ApproachingTarget = false;
                return;
            }
            Vector3 facingDirection = new Vector3((-1) * Mathf.Sin(Rotation.y), 0, Mathf.Cos(Rotation.y));
        }
        else {
            if(GlobalTransform.origin.DistanceTo(new Vector3(0,GlobalTransform.origin.y, 0)) >= TurnaroundDistance) {
                ApproachingTarget = true;
            }
        }
    }

    protected void UpdateShipDirectionOrbit() {
        // get the vector from the current position to the city, if we aren't pointed at the city, start turning
		// the default rotation for the ship will be along the negative z-axis, which is a rotation of:
		//       x: 0, y: -90, z: 0
		//      Only need to change the y rotation for direction, with + going toward +x-axis from here
        // For now I'm assuming the city is always at (0,0,0), I think I'd like to keep that consistant
        // across levels.
		Vector3 directionToCity = new Vector3(0,GetGlobalTransform().origin.y,0) - GetGlobalTransform().origin;
        Vector3 facingDirection = new Vector3((-1) * Mathf.Sin(Rotation.y), 0, Mathf.Cos(Rotation.y));
        // To get a clockwise perpendicular vector, we switch the x and z vector values and negate the new
        // x coordinate
        Vector3 movementDirection = new Vector3((-1) * directionToCity.z, 0, directionToCity.x);
        DirectionVector = movementDirection.Normalized();
        LookAt(new Vector3(0,GetGlobalTransform().origin.y,0), new Vector3(0,1,0));
    }

    protected void UpdateTarget() {
        if(CurrentTarget == null) {
            if(PotentialTargets.Count > 0) {
                CurrentTarget = PotentialTargets[PotentialTargets.Count - 1];
                PotentialTargets.RemoveAt(PotentialTargets.Count - 1);
            }
        }
    }
}
