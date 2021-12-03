using Godot;
using System;

public class ItemStoreInterface : MarginContainer
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    PackedScene IconScene = GD.Load<PackedScene>("res://GameObjects/Interfaces/Overworld/StoreIcon.tscn");

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();
        PopulateInventory();
        RandomizeAvailable();
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public void MakePurchase(string item) {

    }

    public void MakeSale(string item) {

    }

    private void PopulateInventory() {
        UserInventory inventory = GetNode<UserInventory>("/root/UserInventoryAL");
        System.Collections.Generic.Dictionary<string, int> unequippedItems = inventory.GetInventory();
        Tuple<string, int>[] equippedItems = inventory.GetEquipped();
        Container inventoryContainer = GetNode<Container>("PanelContainer/VBoxContainer/HBoxContainer/PanelContainer/VBoxContainer/ScrollContainer/VBoxContainer");
        foreach(System.Collections.Generic.KeyValuePair<string, int> entry in unequippedItems) {
            StoreIcon newIcon = IconScene.Instance<StoreIcon>();
            newIcon.AssignItem(entry.Key, entry.Value);
            inventoryContainer.AddChild(newIcon);
            newIcon.Connect("IconUsed", this, nameof(MakeSale));
        }
        for(int i = 0; i < equippedItems.Length; i++) {
            StoreIcon newIcon = IconScene.Instance<StoreIcon>();
            newIcon.AssignItem(equippedItems[i].Item1, equippedItems[i].Item2);
            inventoryContainer.AddChild(newIcon);
            newIcon.Connect("IconUsed", this, nameof(MakeSale));
        }
    }

    private void RandomizeAvailable() {

    }
}
