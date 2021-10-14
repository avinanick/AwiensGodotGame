using Godot;
using System;

public class AlienCarrier : CombatShip
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Export]
    Vector3 ShipSpawnOffset = new Vector3(0,3,0);

    protected bool Seiging = false;
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

    protected void CreateShip() {
        
    }

    protected void EndSeige() {
        Seiging = false;
    }

    protected override void HoverDestinationReached() {
        base.HoverDestinationReached();
        HoverLocked = true;
        StartSeige();
    }

    public void LaunchShip() {
        
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

    protected void StartSeige() {
        Seiging = true;
        GetNode<Timer>("ShipSpawnTimer").Start();
    }
}
