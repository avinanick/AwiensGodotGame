using Godot;
using System;

public class AlienData : Node
{
    private class AlienSet
    {
        private string AlienScenePath;
        private string AlienIconPath;
        private float AlienSpawnInterval;

        public AlienSet(string scenePath, string iconPath, float spawnInterval) {
            AlienScenePath = scenePath;
            AlienIconPath = iconPath;
            AlienSpawnInterval = spawnInterval;
        }

        public string GetIconPath() {
            return AlienIconPath;
        }

        public string GetScenePath() {
            return AlienScenePath;
        }

        public float GetSpawnInterval() {
            return AlienSpawnInterval;
        }
    }

    private System.Collections.Generic.Dictionary<string, AlienSet> DataSet;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        DataSet = new System.Collections.Generic.Dictionary<string, AlienSet>();
        DataSet.Add("Alien Missile", new AlienSet("res://GameObjects/Units/Aliens/AlienMissile.tscn", "res://icon.png", 3.0f));
        DataSet.Add("Alien Drone", new AlienSet("res://GameObjects/Units/Aliens/AlienDrone.tscn", "res://icon.png", 5.0f));
        DataSet.Add("Alien Planet Drill", new AlienSet("res://GameObjects/Units/Aliens/AlienPlanetDrill.tscn", "res://icon.png", 5.0f));
        DataSet.Add("Alien Fighter", new AlienSet("res://GameObjects/Units/Aliens/AlienFighter.tscn", "res://icon.png", 5.0f));
        DataSet.Add("Alien Inhibitor", new AlienSet("res://GameObjects/Units/Aliens/AlienImpeder.tscn", "res://icon.png", 5.0f));
        DataSet.Add("Alien Scout", new AlienSet("res://GameObjects/Units/Aliens/AlienScout.tscn", "res://icon.png", 5.0f));
        DataSet.Add("Alien Artillery", new AlienSet("res://GameObjects/Units/Aliens/AlienArtillery.tscn", "res://icon.png", 5.0f));
        DataSet.Add("Alien Bomber", new AlienSet("res://GameObjects/Units/Aliens/AlienBomber.tscn", "res://icon.png", 5.0f));
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public string GetAlienFilePath(string alienName) {
        return DataSet[alienName].GetScenePath();
    }

    public string GetAlienIconPath(string alienName) {
        return DataSet[alienName].GetIconPath();
    }

    public float GetAlienSpawnInterval(string alienName) {
        return DataSet[alienName].GetSpawnInterval();
    }

    public bool IsAnAlien(string potentialAlienName) {
        return DataSet.ContainsKey(potentialAlienName);
    }
}
