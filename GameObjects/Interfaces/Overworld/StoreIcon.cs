using Godot;
using System;

public class StoreIcon : MarginContainer
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private string ItemName;
    private int Amount;
    private bool IsStoreItem = false;
    private CampaignTracker Tracker;
    private ItemData Data;

    [Signal]
    public delegate void IconUsed(string itemName);

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();
        Tracker = GetNode<CampaignTracker>("/root/CampaignTrackerAL");
        Data = GetNode<ItemData>("/root/ItemDataAL");
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public void AssignItem(string itemName, int amount, bool storeItem) {
        ItemName = itemName;
        IsStoreItem = storeItem;
        GetNode<TextureButton>("TextureButton").TextureNormal = GD.Load<Texture>(GetNode<ItemData>("/root/ItemDataAL").GetItemIconPath(itemName));
    }

    public void IconClicked() {
        if(IsStoreItem) {
            if(Tracker.GetCurrentPoints() >= Data.GetItemCost(ItemName)) {
                // remove the point cost
                Tracker.SpendPoints(Data.GetItemCost(ItemName));
            }
            else {
                // Player can't afford the item, maybe a signal to give an error message or
                // something?
                GD.Print("Cannnot afford this item");
                return;
            }
        }
        else {
            // Add half the cost of the item to the current points
            Tracker.EarnPoints(Data.GetItemCost(ItemName) / 2);
        }
        EmitSignal(nameof(IconUsed), ItemName);
        Amount -= 1;
        if(Amount <= 0) {
            QueueFree();
        }
    }
}
