using Godot;
using System;

public class UserInventory : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private class ItemInfo {
        public String ItemName;
        public int ItemAmount;

        public ItemInfo(String newName, int newAmount) {
            ItemName = newName;
            ItemAmount = newAmount;
        }

        public Tuple<String, int> ToTuple() {
            return new Tuple<string, int>(ItemName, ItemAmount);
        }
    }
    private ItemInfo[] EquippedItems = {
        new ItemInfo("Bomb", 2),
        new ItemInfo("", -1)
    };
        
    protected System.Collections.Generic.Dictionary<String, int> InventoryItems = new System.Collections.Generic.Dictionary<string, int>();

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

    public void AddItem(String itemName, int amount) {
        // If the item is already in the inventory, just add to it, otherwise
        // we need to make the entry
        ItemData itemAL = GetNode<ItemData>("/root/ItemDataAL");
        if(itemAL.IsAnItem(itemName)) {
            if(InventoryItems.ContainsKey(itemName)) {
                InventoryItems[itemName] += amount;
            }
            else {
                InventoryItems.Add(itemName, amount);
            }
        }
    }

    public void EquipItem(String itemName, int itemSlot, int amount = -1) {
        // Transfers the amount of the selected item from the inventory to the
        // equipped items, right now does not check to make sure there is room
        // as I don't have a set number of equippable items in mind
        if(InventoryItems.ContainsKey(itemName) & InventoryItems[itemName] > 0) {
            int transferAmount = 0;
            if(amount < 0) {
                transferAmount = InventoryItems[itemName];
            }
            else if(InventoryItems[itemName] >= amount) {
                transferAmount = amount;
            }
            else {
                transferAmount = InventoryItems[itemName];
            }
            InventoryItems[itemName] -= transferAmount;
            if(EquippedItems[itemSlot].ItemName == itemName) {
                EquippedItems[itemSlot].ItemAmount += transferAmount;
            }
            else {
                UnequipItem(itemSlot, transferAmount);
                EquippedItems[itemSlot].ItemName = itemName;
                EquippedItems[itemSlot].ItemAmount = transferAmount;
            }
        }
        else {
            GD.Print("No items to equip");
        }
    }

    public Tuple<String, int>[] GetEquipped() {
        Tuple<string, int>[] equipped = new Tuple<string, int>[EquippedItems.Length];
        for(int i = 0; i < EquippedItems.Length; i++) {
            equipped[i] = EquippedItems[i].ToTuple();
        }
        return equipped;
    }

    public System.Collections.Generic.Dictionary<String, int> GetInventory() {
        return InventoryItems;
    }

    public Godot.Collections.Dictionary<string, object> GetSaveDate() {
        return new Godot.Collections.Dictionary<string, object>() {

        };
    }

    private void SpawnItem(string itemName) {
        PackedScene itemScene = (PackedScene)GD.Load(GetNode<ItemData>("/root/ItemDataAL").GetItemFilePath(itemName));
        Item newItem = itemScene.Instance<Item>();
        GetTree().CurrentScene.AddChild(newItem);
        GD.Print("Item spawned");
        newItem.Deploy();
    }

    public void UnequipItem(int itemSlot, int amount = -1) {
        // Transfers from equipped items to the inventory items.
        ItemData itemAL = GetNode<ItemData>("/root/ItemDataAL");
        if(itemAL.IsAnItem(EquippedItems[itemSlot].ItemName) & EquippedItems[itemSlot].ItemAmount > 0) {
            int transferAmount = 0;
            if(amount < 0) {
                transferAmount = EquippedItems[itemSlot].ItemAmount;
            }
            else if(EquippedItems[itemSlot].ItemAmount >= amount) {
                transferAmount = amount;
            }
            else {
                transferAmount = EquippedItems[itemSlot].ItemAmount;
            }
            if(InventoryItems.ContainsKey(EquippedItems[itemSlot].ItemName)) {
                InventoryItems[EquippedItems[itemSlot].ItemName] += transferAmount;
            }
            else {
                InventoryItems.Add(EquippedItems[itemSlot].ItemName, transferAmount);
            }
            EquippedItems[itemSlot].ItemAmount -= transferAmount;
            if(EquippedItems[itemSlot].ItemAmount < 1) {
                EquippedItems[itemSlot].ItemName = "";
            }
        }
        else {
            GD.Print("No items to remove");
        }
    }

    public int UseItem(int itemSlot) {
        ItemData itemAL = GetNode<ItemData>("/root/ItemDataAL");
        if(itemAL != null & itemAL.IsAnItem(EquippedItems[itemSlot].ItemName) & EquippedItems[itemSlot].ItemAmount > 0) {
            EquippedItems[itemSlot].ItemAmount -= 1;
            CallDeferred(nameof(SpawnItem), EquippedItems[itemSlot].ItemName);
            if(EquippedItems[itemSlot].ItemAmount < 1) {
                EquippedItems[itemSlot].ItemName = "";
            }
            return EquippedItems[itemSlot].ItemAmount;
            // do I actually deploy the item here?
        }
        GD.Print("No items equipped in that slot");
        return -1;
    }
}
