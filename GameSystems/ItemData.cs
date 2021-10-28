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

        public string GetItemPath() {
            return ItemIconPath;
        }

        public string GetScenePath() {
            return ItemScenePath;
        }

        public float GetSpawnInterval() {
            return ItemSpawnInterval;
        }
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
