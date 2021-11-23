using Godot;
using System;

public class DeploymentBeaconItem : Item
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public override void Deploy() {
        Godot.Collections.Array playerAvatar = GetTree().GetNodesInGroup("Player");
        if(playerAvatar.Count > 0) {
            if(playerAvatar[0] is Avatar currentPlayer) {
                bool successfullyDeployed = currentPlayer.RespawnCurrentWeapon();
                if(!successfullyDeployed) {
                    UserInventory inventory = GetNode<UserInventory>("/root/UserInventoryAL");
                    inventory.ReturnItem("Deployment Beacon");
                }
                QueueFree();
                return;
            }
        }
        GD.Print("Error: no player avatar found");
        QueueFree();
    }
}
