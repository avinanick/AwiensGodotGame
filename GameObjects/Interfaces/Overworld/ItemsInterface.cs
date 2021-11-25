using Godot;
using System;

public class ItemsInterface : MarginContainer
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    PackedScene IconScene = GD.Load<PackedScene>("res://GameObjects/Interfaces/ItemIcon.tscn");

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Visible = false;
        CallDeferred(nameof(PopulateItems));
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public void CloseInterface() {
        Visible = false;
    }

    public void OpenInterface() {
        Visible = true;
    }

    private void PopulateItems() {
        UserInventory inventory = GetNode<UserInventory>("/root/UserInventoryAL");
        System.Collections.Generic.Dictionary<string, int> unequippedItems = inventory.GetInventory();
        Tuple<String, int>[] equippedItems = inventory.GetEquipped();
        Node equippedInventoryContainer = GetNode<Node>("PanelContainer/VBoxContainer/InventoryPanelContainer/ScrollContainer/HBoxContainer");
        Node slotOneContainer = GetNode<Node>("PanelContainer/VBoxContainer/Deployment/HBoxContainer/ItemsEquipped/VBoxContainer/SlotOneContainer/HBoxContainer/SlotOne");
        Node slotTwoContainer = GetNode<Node>("PanelContainer/VBoxContainer/Deployment/HBoxContainer/ItemsEquipped/VBoxContainer/SlotTwoContainer/HBoxContainer/SlotTwo");
        foreach(System.Collections.Generic.KeyValuePair<string, int> entry in unequippedItems) {
            ItemIcon newIcon = IconScene.Instance<ItemIcon>();
            equippedInventoryContainer.AddChild(newIcon);
            newIcon.AssignItem(entry.Key);
            newIcon.SetAmount(entry.Value);
        }
        if(equippedItems.Length > 0) {
            ItemIcon newIcon = IconScene.Instance<ItemIcon>();
            slotOneContainer.AddChild(newIcon);
            newIcon.AssignItem(equippedItems[0].Item1);
            newIcon.SetAmount(equippedItems[0].Item2);
        }
        if(equippedItems.Length > 1) {
            ItemIcon newIcon = IconScene.Instance<ItemIcon>();
            slotTwoContainer.AddChild(newIcon);
            newIcon.AssignItem(equippedItems[1].Item1);
            newIcon.SetAmount(equippedItems[1].Item2);
        }
    }
}
