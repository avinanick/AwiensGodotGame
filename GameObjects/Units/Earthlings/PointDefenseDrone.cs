using Godot;
using System;

public class PointDefenseDrone : CombatShip
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Export]
    private float ProjectileKillCooldown = 1.0f;

    private bool ReadyToBlock = true;

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

    public void CooldownTimeout() {
        ReadyToBlock = true;
    }

    public void OnAreaEntered(Node body) {
        if(body is AlienBullet bullet) {
            if(ReadyToBlock) {
                bullet.QueueFree();
                ReadyToBlock = false;
                GetNode<Timer>("CooldownTimer").Start(ProjectileKillCooldown);
                // Some animation to destroy the bullet
            }
        }
    }
}
