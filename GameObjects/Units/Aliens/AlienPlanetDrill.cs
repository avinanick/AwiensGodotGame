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
        if(HoverLocationReached) {
            GetNode<Timer>("DamagePulseTimer").Start();
            Godot.Collections.Array<Destructible> damageTargets = new Godot.Collections.Array<Destructible>(GetNode<Area>("Area").GetOverlappingBodies());
            for(int i = 0; i < damageTargets.Count; i++) {
                damageTargets[i].TakeDamage(LaserDamage);
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
