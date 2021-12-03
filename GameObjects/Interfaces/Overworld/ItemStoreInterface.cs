using Godot;
using System;

public class ItemStoreInterface : MarginContainer
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    PackedScene IconScene = GD.Load<PackedScene>("res://GameObjects/Interfaces/Overworld/StoreIcon.tscn");

    [Export]
    private int NumSaleItems = 3;
    private int MaxItemAmount = 3;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();
        CallDeferred(nameof(PopulateInventory));
        CallDeferred(nameof(RandomizeAvailable));
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public void MakePurchase(string item) {
        UserInventory inventory = GetNode<UserInventory>("/root/UserInventoryAL");
        inventory.AddItem(item, 1);
    }

    public void MakeSale(string item) {
        UserInventory inventory = GetNode<UserInventory>("/root/UserInventoryAL");
        inventory.RemoveItem(item, 1);
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
        ItemData data = GetNode<ItemData>("/root/ItemDataAL");
        Container storeContainer = GetNode<Container>("PanelContainer/VBoxContainer/HBoxContainer/PanelContainer2/VBoxContainer/ScrollContainer/VBoxContainer");
        string[] itemTypes = data.GetItemList();
        // Shuffle the array of item types
        Random rng = new Random();
        int numItems = itemTypes.Length;
        while(numItems > 1) {
            int switchPosition = rng.Next(numItems--);
            string temp = itemTypes[numItems];
            itemTypes[numItems] = itemTypes[switchPosition];
            itemTypes[switchPosition] = temp;
        }
        // Create the item icons
        for(int i = 0; i < NumSaleItems; i++) {
            StoreIcon icon = IconScene.Instance<StoreIcon>();
            icon.AssignItem(itemTypes[i], rng.Next(MaxItemAmount) + 1);
            storeContainer.AddChild(icon);
            icon.Connect("IconUsed", this, nameof(MakeSale));
        }
    }
}
