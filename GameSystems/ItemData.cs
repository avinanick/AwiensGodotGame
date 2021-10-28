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
        private float ItemSpawnInterval;

        public ItemSet(string scenePath, string itemPath, float spawnInterval) {
            ItemScenePath = scenePath;
            ItemIconPath = itemPath;
            ItemSpawnInterval = spawnInterval;
        }

        public string GetIconPath() {
            return ItemIconPath;
        }

        public string GetScenePath() {
            return ItemScenePath;
        }

        public float GetSpawnInterval() {
            return ItemSpawnInterval;
        }
    }

    private System.Collections.Generic.Dictionary<string, ItemSet> DataSet;

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

    public string GetItemFilePath(string itemName) {
        return DataSet[itemName].GetScenePath();
    }

    public string GetItemIconPath(string itemName) {
        return DataSet[itemName].GetIconPath();
    }

    public float GetItemSpawnInterval(string itemName) {
        return DataSet[itemName].GetSpawnInterval();
    }

    public bool IsAnItem(string potentialItemName) {
        return DataSet.ContainsKey(potentialItemName);
    }
}
