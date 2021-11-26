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
    private Texture ItemImage;
    private Control SwitchSlot = null;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();
        SetProcess(false);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        Vector2 floatPosition = GetGlobalMousePosition();
        floatPosition.x -= RectSize.x / 2;
        floatPosition.y -= RectSize.y / 2;
        SetGlobalPosition(floatPosition);
    }

    public void AssignItem(String itemName) {
        ItemData data = GetNode<ItemData>("/root/ItemDataAL");
        UserInventory inventory = GetNode<UserInventory>("/root/UserInventoryAL");
        if(data.IsAnItem(itemName)) {
            ItemName = itemName;
            ItemImage = GD.Load<Texture>(data.GetItemIconPath(itemName));
            GetNode<TextureButton>("TextureRect").TextureNormal = ItemImage;
            // Need a way to get how many of the item is in the inventory here.
        }
        else {
            GD.Print("Error: assigning nonexistant item");
        }
    }

    public void ButtonClicked() {
        // Make the button follow the mouse, does this work for the margin container?
        SetProcess(true);
    }

    public void ButtonReleased() {
        // Check if the button is hovering over a draggable area, if so we reparent to that
        // otherwise we go back to the previous place
        SetProcess(false);
        UserInventory inventory = GetNode<UserInventory>("/root/UserInventoryAL");
        Control dropSlot = SwitchSlot;
        GetParent().RemoveChild(this);
        dropSlot.AddChild(this);
        // I need to figure out how to get the position reset so the parent will control
        // position now
        // Will need to let the user inventory know and figure out how to go with equip or
        // unequip
        if(dropSlot is HBoxContainer inventorySlot) {
            if(GetParent().Name == "SlotOne") {
                inventory.UnequipItem(0);
            }
            else {
                inventory.UnequipItem(1);
            }
            GetParent().RemoveChild(this);
            inventorySlot.AddChild(this);
        }
        else {
            if(dropSlot.GetChildCount() < 1) {
                GetParent().RemoveChild(this);
                dropSlot.AddChild(this);
                // Need to contact the user inventroy to equip this
                if(dropSlot.Name == "SlotOne") {
                    inventory.EquipItem(ItemName, 0);
                }
                else {
                    inventory.EquipItem(ItemName, 1);
                }
            }
        }
    }

    public void HoverSlot(Control potentialSlot) {
        SwitchSlot = potentialSlot;
    }

    public void LeaveSlot(Control leavingSlot) {

    }

    public void SetAmount(int amount) {
        ItemAmount = amount;
    }
}
