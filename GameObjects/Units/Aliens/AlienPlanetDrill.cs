using Godot;
using System;

public class AlienPlanetDrill : CombatShip
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Export]
    protected int LaserDamage = 1;

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

    protected int CheckWeapons(Destructible target) {
        int damage = LaserDamage;
        CampaignTracker tracker = GetNode<CampaignTracker>("/root/CampaignTrackerAL");
        int emwCount = tracker.CheckForEnhancement("EM Weapons");
        if(emwCount > 0) {
            if(target != null) {
                if(target is EnergyShield shield) {
                    damage = (int)(LaserDamage * Mathf.Pow(1.1f, emwCount));
                }
            }
        }
        int apwCount = tracker.CheckForEnhancement("AP Weapons");
        if(apwCount > 0) {
            if(target != null) {
                if(target is EnergyShield shield) {
                    // Need to do something in a different case
                }
                else {
                    damage = (int)(LaserDamage * Mathf.Pow(1.1f, apwCount));
                }
            }
        }
        return damage;
    }

    public void EndAttackChannel() {
        // This should play the end attack animation
        GetNode<AnimationPlayer>("AnimationPlayer").PlayBackwards("BeamAttack");
    }

    protected override void HoverDestinationReached()
    {
        base.HoverDestinationReached();
        StartAttackChannel();
    }

    public void PulseDamage() {
        EnergyShield shipShield = GetNode<EnergyShield>("EnergyShield");
        if(HoverLocationReached) {
            GetNode<Timer>("DamagePulseTimer").Start();
            Godot.Object damageTarget = GetNode<RayCast>("RayCast").GetCollider();
            if(damageTarget != null) {
                if(damageTarget is Destructible destructible) {
                    destructible.TakeDamage(CheckWeapons(destructible));
                }
            }
        }
    }

    protected override void SetHoverLocation() {
        if(CurrentTarget != null & Godot.Object.IsInstanceValid(CurrentTarget)) {
            Vector3 location = CurrentTarget.GlobalTransform.origin;
            location.y += 20;
            HoverLocation = location;
            EndAttackChannel();
        }
    }

    protected void StartAttackChannel() {
        // play the start attack animation, for now will just turn on the attack mesh
        GetNode<AnimationPlayer>("AnimationPlayer").Play("BeamAttack");
        PulseDamage();
    }
}
