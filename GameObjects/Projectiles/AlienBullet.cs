using Godot;
using System;

public class AlienBullet : Projectile
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

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

    protected void CheckEnhancements(KinematicCollision collision) {
        CampaignTracker tracker = GetNode<CampaignTracker>("/root/CampaignTrackerAL");
        int emwCount = tracker.CheckForEnhancement("EM Weapons");
        if(emwCount > 0) {
            if(collision != null) {
                if(collision.Collider is EnergyShield shield) {
                    Damage = (int)(Damage * Mathf.Pow(1.1f, emwCount));
                }
            }
        }
        int apwCount = tracker.CheckForEnhancement("AP Weapons");
        if(apwCount > 0) {
            if(collision != null) {
                if(collision.Collider is EnergyShield shield) {
                    // Need to do something in a different case
                }
                else {
                    Damage = (int)(Damage * Mathf.Pow(1.1f, apwCount));
                }
            }
        }
    }

    protected override void HandleImpact(KinematicCollision collision)
    {
        CheckEnhancements(collision);
        base.HandleImpact(collision);
    }
}
