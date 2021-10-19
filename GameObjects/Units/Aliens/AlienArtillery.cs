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
    protected bool HoverLocked = false;

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
            Vector3 missileSpawnPoint = GlobalTransform.origin + MissileSpawnOffset;
            AlienMissile newMissile = MissileScene.Instance<AlienMissile>();
            newMissile.Visible = false;
            GetTree().CurrentScene.AddChild(newMissile);
            newMissile.Translation = missileSpawnPoint;
            //AddChild(newMissile);
            //newMissile.Translation = MissileSpawnOffset;
            CurrentMissile = newMissile;
            newMissile.ChargeMissile();
            EnergyShield shipShield = GetNode<EnergyShield>("EnergyShield");
            if(shipShield != null) {
                newMissile.AddCollisionExceptionWith(shipShield);
                EnergyShield spawnedShield = newMissile.GetNode<EnergyShield>("EnergyShield");
                if(spawnedShield != null) {
                    spawnedShield.AddCollisionExceptionWith(shipShield);
                }
            }
            newMissile.Connect("MissileChargeFinished", (AlienArtillery)this, nameof(LaunchMissile));
        }
    }

    protected override void HoverDestinationReached() {
        base.HoverDestinationReached();
        HoverLocked = true;
        StartBombardment();
        LookAt(new Vector3(0, GlobalTransform.origin.y, 0), new Vector3(0,1,0));
    }

    public void LaunchMissile() {
        //RemoveChild(CurrentMissile);
        //GetTree().CurrentScene.AddChild(CurrentMissile);
        if(CurrentTarget != null & Godot.Object.IsInstanceValid(CurrentTarget)) {
            CurrentMissile.SetTarget(CurrentTarget);
        }
        else {
            CurrentMissile.SpawnShip();
        }
        GetNode<Timer>("MissileLaunchTimer").Start();
        CurrentMissile = null;
    }

    protected override void SetHoverLocation()
    {
        if(!HoverLocked) {
            base.SetHoverLocation();
            // Move away from the city a short distance, and lower to height 15
            Vector3 vectorFromCity = (GlobalTransform.origin - new Vector3(0,GlobalTransform.origin.y, 0)).Normalized();
            HoverLocation = new Vector3(GlobalTransform.origin.x, 0, GlobalTransform.origin.z) + vectorFromCity * 10 + new Vector3(0,15,0);
        }
        else {
            HoverLocation = GlobalTransform.origin;
        }
    }

    protected void StartBombardment() {
        Bombarding = true;
        GetNode<Timer>("MissileLaunchTimer").Start();
    }
}
