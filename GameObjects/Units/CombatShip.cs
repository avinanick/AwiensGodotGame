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
    protected enum StrafeStateType {
        Approaching,
        Departing,
        Turning
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
    protected StrafeStateType StrafeState = StrafeStateType.Approaching;
    protected float TurnaroundDistance = 40;
    protected float RotateSpeed = 1;
    protected Vector3 HoverLocation = new Vector3();
    protected bool HoverLocationReached = false;

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
        switch(AttackPattern) {
            case AttackPatternType.Orbit:
                UpdateShipDirectionOrbit();
                break;
            case AttackPatternType.Strafe:
                UpdateShipDirectionStrafe(delta);
                break;
            case AttackPatternType.Hover:
                UpdateShipDirectionHover();
                break;
        }
        UpdateTarget();
        if(CurrentTarget != null & Weapon != null & Godot.Object.IsInstanceValid(CurrentTarget)) {
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

    protected virtual void HoverDestinationReached() {
        HoverLocationReached = true;
        DirectionVector = new Vector3(0,0,0);
    }

    protected void InitialEnemyPopulation() {
        //Godot.Collections.Array<Destructible> startingTargets = new Godot.Collections.Array<Destructible>(GetNode<Area>("AttackRangeArea").GetOverlappingBodies());
        //for(int i = 0; i < startingTargets.Count; i++) {
        //    BodyEnteringRange(startingTargets[i]);
        //}
        // Do I want to just make ranges global instead?
        Godot.Collections.Array<Destructible> possibleTargets = new Godot.Collections.Array<Destructible>(GetTree().GetNodesInGroup("Destructible"));
        for(int i = 0; i < possibleTargets.Count; i++) {
            BodyEnteringRange(possibleTargets[i]);
        }
    }

    protected virtual void SetHoverLocation() {

    }

    public override void SpawnShip() {

    }

    protected void UpdateShipDirectionHover() {
        // Move toward the hover location, if arrived, set the direction vector to 0
        // otherwise move toward the hover location. This move type will need to move
        // toward a target, and update when the target is destroyed.
        if(!HoverLocationReached) {
            if(GlobalTransform.origin.DistanceTo(HoverLocation) < 0.1) {
                HoverDestinationReached();
            }
            else {
                DirectionVector = (HoverLocation - GlobalTransform.origin).Normalized();
            }
        }
    }

    protected void UpdateShipDirectionStrafe(float delta) {
        // This needs to be a movement pattern that moves toward the city, passes over it
        // continues on, then turns around and repeats.
        // For now I'm  going to try: if approaching the city and not currently facing it, 
        // turn to face the city. If approaching and facing, move toward the city until reaching
        // it. If not approaching, move away until reaching a distance of 40
        Vector3 directionToCity = new Vector3(0, GlobalTransform.origin.y, 0) - GlobalTransform.origin;
        switch(StrafeState) {
            case StrafeStateType.Approaching:
                if(directionToCity.Length() <= 0.1) {
                    StrafeState = StrafeStateType.Departing;
                    return;
                }
                break;
            case StrafeStateType.Departing:
                if(GlobalTransform.origin.DistanceTo(new Vector3(0,GlobalTransform.origin.y, 0)) >= TurnaroundDistance) {
                    StrafeState = StrafeStateType.Turning;
                    DirectionVector = new Vector3(0,0,0);
                }
                break;
            case StrafeStateType.Turning:
                Vector3 facingDirection = new Vector3((-1) * Mathf.Sin(Rotation.y), 0, Mathf.Cos(Rotation.y));
                float angleBetween = Mathf.Acos(facingDirection.Dot(directionToCity) / (facingDirection.Length() * directionToCity.Length()));
                if(angleBetween <= 0.01f) {
                    DirectionVector = directionToCity.Normalized();
                    StrafeState = StrafeStateType.Approaching;
                }
                else {
                    RotateY(RotateSpeed * delta);
                }
                break;
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
        if(CurrentTarget == null || !Godot.Object.IsInstanceValid(CurrentTarget)) {
            HoverLocationReached = false;
            if(PotentialTargets.Count > 0) {
                CurrentTarget = PotentialTargets[PotentialTargets.Count - 1];
                PotentialTargets.RemoveAt(PotentialTargets.Count - 1);
                if(AttackPattern == AttackPatternType.Hover) {
                    SetHoverLocation();
                }
            }
        }
    }
}
