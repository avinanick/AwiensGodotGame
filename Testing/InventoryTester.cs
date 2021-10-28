using Godot;
using System;

public class InventoryTester : Node
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

    public void OnTesterTimeout() {
        UserInventory inventory = GetNode<UserInventory>("/root/UserInventoryAL");
        inventory.AddItem("Bomb", 3);
        GD.Print(inventory.GetInventory());
        GD.Print(inventory.GetEquipped());
        inventory.EquipItem("Bomb", 0);
        GD.Print(inventory.GetInventory());
        GD.Print(inventory.GetEquipped());
        inventory.UnequipItem(0);
        GD.Print(inventory.GetInventory());
        GD.Print(inventory.GetEquipped());
    }
}
