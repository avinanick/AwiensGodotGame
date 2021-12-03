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
        private int ItemCost;

        public ItemSet(string scenePath, string itemPath, int itemCost) {
            ItemScenePath = scenePath;
            ItemIconPath = itemPath;
            ItemCost = itemCost;
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
        DataSet.Add("Bomb", new ItemSet("res://GameObjects/Items/Bomb.tscn", "res://icon.png", 30));
        DataSet.Add("Repair Drone", new ItemSet("res://GameObjects/Items/RepairDroneItem.tscn", "res://icon.png", 70));
        DataSet.Add("Defense Drone", new ItemSet("res://GameObjects/Items/PointDefenseDroneItem.tscn", "res://icon.png", 80));
        DataSet.Add("AutoTurret", new ItemSet("res://GameObjects/Items/AutoturretItem.tscn", "res://icon.png", 50));
        DataSet.Add("Holographic Decoy", new ItemSet("res://GameObjects/Items/HolographicDecoyItem.tscn", "res://icon.png", 40));
        DataSet.Add("Deployment Beacon", new ItemSet("res://GameObjects/Items/DeploymentBeaconItem.tscn", "res://icon.png", 90));
        DataSet.Add("Combat Drone", new ItemSet("res://GameObjects/Items/CombatDroneItem.tscn", "res://icon.png", 60));
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

    public string[] GetItemList() {
        string[] itemTypes = new string[DataSet.Count];
        int i = 0;
        foreach(System.Collections.Generic.KeyValuePair<string, ItemSet> entry in DataSet) {
            itemTypes[i] = entry.Key;
            i++;
        }
        return itemTypes;
    }

    public bool IsAnItem(string potentialItemName) {
        return DataSet.ContainsKey(potentialItemName);
    }
}
