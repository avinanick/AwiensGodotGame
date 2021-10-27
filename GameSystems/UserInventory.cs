using Godot;
using System;

public class UserInventory : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    protected System.Collections.Generic.Dictionary<String, int> EquippedItems = new System.Collections.Generic.Dictionary<string, int>();
    
    protected System.Collections.Generic.Dictionary<String, int> InventoryItems = new System.Collections.Generic.Dictionary<string, int>();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public void AddItem(String itemName, int amount) {

    }

    public void EquipItem(String itemName, int amount = -1) {

    }

    public System.Collections.Generic.Dictionary<String, int> GetEquipped() {
        return EquippedItems;
    }

    public System.Collections.Generic.Dictionary<String, int> GetInventory() {
        return InventoryItems;
    }

    public Godot.Collections.Dictionary<string, object> GetSaveDate() {
        return new Godot.Collections.Dictionary<string, object>() {

        };
    }

    public void UnequipItem(String itemName, int amount = -1) {

    }

    public int UseItem(String itemName) {
        return 0; // STUB
    }
}
