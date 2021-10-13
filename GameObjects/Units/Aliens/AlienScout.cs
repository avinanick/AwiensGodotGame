using Godot;
using System;

public class AlienScout : CombatShip
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Export]
    protected float HoverDistance = 9f;
    protected PackedScene MissileScene = GD.Load<PackedScene>("res://GameObjects/Units/Aliens/AlienMissile.tscn");
    protected bool Targeting = false;

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
        Targeting = false;
        // Targeting end animation here
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

    public void SpawnMissile() {
        if(Targeting) {
            Vector3 spawnLocation = GlobalTransform.origin + new Vector3(0,30,0);
            AlienMissile newMissile = MissileScene.Instance<AlienMissile>();
            GetTree().CurrentScene.AddChild(newMissile);
            newMissile.Translation = spawnLocation;
            newMissile.SetTarget(CurrentTarget);
            newMissile.WarpIn();
            GetNode<Timer>("MissileWarpTimer").Start();
        }
    }

    protected void StartTargeting() {
        Targeting = true;
        GetNode<Timer>("MissileWarpTimer").Start();
        // Add animation here
    }
}
