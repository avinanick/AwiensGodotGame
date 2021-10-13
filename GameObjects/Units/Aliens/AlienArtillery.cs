using Godot;
using System;

public class AlienArtillery : CombatShip
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Export]
    Vector3 MissileSpawnOffset = new Vector3(0,3,0);

    protected PackedScene MissileScene = GD.Load<PackedScene>("res://GameObjects/Units/Aliens/AlienMissile.tscn");
    protected bool Bombarding = false;
    protected AlienMissile CurrentMissile = null;

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

    protected void EndBombardment() {
        Bombarding = false;
    }

    protected void FireOrdinance() {
        if(Bombarding) {
            // Calculate the missile spawn, then charge and launch it.
            //Vector3 missileSpawnPoint = GlobalTransform.origin + MissileSpawnOffset;
            AlienMissile newMissile = MissileScene.Instance<AlienMissile>();
            AddChild(newMissile);
            newMissile.Translation = MissileSpawnOffset;
            CurrentMissile = newMissile;
            newMissile.ChargeMissile();
        }
    }

    protected override void HoverDestinationReached() {
        base.HoverDestinationReached();
        StartBombardment();
    }

    public void LaunchMissile() {
        
    }

    protected override void SetHoverLocation()
    {
        base.SetHoverLocation();
        // Move away from the city a short distance, and lower to height 15
        Vector3 vectorFromCity = (GlobalTransform.origin - new Vector3(0,GlobalTransform.origin.y, 0)).Normalized();
        HoverLocation = vectorFromCity * 10 + new Vector3(0,15,0);
    }

    protected void StartBombardment() {
        Bombarding = true;
        GetNode<Timer>("MissileLaunchTimer").Start();
    }
}
