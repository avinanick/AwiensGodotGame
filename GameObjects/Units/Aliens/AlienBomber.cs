using Godot;
using System;

public class AlienBomber : CombatShip
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    [Export]
    protected float BombCooldown = 2f;
    [Export]
    protected PackedScene BombScene = GD.Load<PackedScene>("res://GameObjects/Units/Aliens/AlienMissile.tscn");
    [Export]
    protected Vector3 BombSpawnOffset = new Vector3();

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

    public void BombCooldownFinished() {
        AlienMissile newMissile = BombScene.Instance<AlienMissile>();
        AddChild(newMissile);
        newMissile.Translation = BombSpawnOffset;
        CurrentMissile = newMissile;
        newMissile.Connect("MissileChargeFinished", this, nameof(BombLaunched));
        newMissile.ChargeMissile();
        EnergyShield shipShield = GetNode<EnergyShield>("EnergyShield");
        if(shipShield != null) {
            newMissile.AddCollisionExceptionWith(shipShield);
            EnergyShield spawnedShield = newMissile.GetNode<EnergyShield>("EnergyShield");
            if(spawnedShield != null) {
                spawnedShield.AddCollisionExceptionWith(shipShield);
            }
        }
    }

    public void BombLaunched() {
        Transform missileTransform = CurrentMissile.GlobalTransform;
        RemoveChild(CurrentMissile);
        GetTree().CurrentScene.AddChild(CurrentMissile);
        CurrentMissile.GlobalTransform = missileTransform;
        CurrentMissile.SpawnShip();
        GetNode<Timer>("BombCooldownTimer").Start(BombCooldown);
    }
}
