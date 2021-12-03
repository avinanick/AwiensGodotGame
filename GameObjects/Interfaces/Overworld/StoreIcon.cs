using Godot;
using System;

public class StoreIcon : MarginContainer
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private string ItemName;
    private int Amount;

    [Signal]
    public delegate void IconUsed(string itemName);

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

    public void AssignItem(string itemName, int amount) {
        ItemName = itemName;
        GetNode<TextureButton>("TextureButton").TextureNormal = GD.Load<Texture>(GetNode<ItemData>("/root/ItemDataAL").GetItemIconPath(itemName));
    }

    public void IconClicked() {
        EmitSignal(nameof(IconUsed), ItemName);
        Amount -= 1;
        if(Amount <= 0) {
            QueueFree();
        }
    }
}
