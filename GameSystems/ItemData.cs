using Godot;
using System;

public class ItemData : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private class ItemSet
    {
        private string ItemScenePath;
        private string ItemIconPath;

        public ItemSet(string scenePath, string itemPath) {
            ItemScenePath = scenePath;
            ItemIconPath = itemPath;
        }

        public string GetIconPath() {
            return ItemIconPath;
        }

        public string GetScenePath() {
            return ItemScenePath;
        }
    }

    private System.Collections.Generic.Dictionary<string, ItemSet> DataSet = new System.Collections.Generic.Dictionary<string, ItemSet>();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();
        DataSet.Add("Bomb", new ItemSet("res://GameObjects/Items/Bomb.tscn", "res://icon.png"));
        DataSet.Add("Repair Drone", new ItemSet("res://GameObjects/Items/RepairDroneItem.tscn", "res://icon.png"));
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public string GetItemFilePath(string itemName) {
        return DataSet[itemName].GetScenePath();
    }

    public string GetItemIconPath(string itemName) {
        return DataSet[itemName].GetIconPath();
    }

    public bool IsAnItem(string potentialItemName) {
        return DataSet.ContainsKey(potentialItemName);
    }
}
