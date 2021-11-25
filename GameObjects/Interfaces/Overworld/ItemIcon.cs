using Godot;
using System;

public class ItemIcon : MarginContainer
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    private String ItemName;
    private int ItemAmount;
    private String ItemDescription;
    private Image ItemImage;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public void AssignItem(String itemName) {
        ItemData data = GetNode<ItemData>("/root/ItemDataAL");
        UserInventory inventory = GetNode<UserInventory>("/root/UserInventroyAL");
        if(data.IsAnItem(itemName)) {
            ItemName = itemName;
            ItemImage = GD.Load<Image>(data.GetItemIconPath(itemName));
            // Need a way to get how many of the item is in the inventory here.
        }
        else {
            GD.Print("Error: assigning nonexistant item");
        }
    }

    public void SetAmount(int amount) {

    }
}
