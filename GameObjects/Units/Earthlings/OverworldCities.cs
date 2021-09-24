using Godot;
using System;

public class OverworldCities : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private Vector2[] CityPositions;
    // I'm currently thinking the threats will be an array of stacks, that
    // allows me to push additions to the stack and use an enumerator to easily
    // read the stack contents for the interface, since it shouldn't really get
    // exceptionally long.
    private System.Collections.Generic.Stack<string>[] CityThreats;
    private int SelectedCity = -1;

    [Signal]
    public delegate void EnterCity(string cityName, Vector2 location, string[] threats);
    [Signal]
    public delegate void ExitCity();
    [Signal]
    public delegate void SelectCity(Vector2 location);

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Godot.Collections.Array<Node2D> cities = new Godot.Collections.Array<Node2D>(GetChildren());
        CityThreats = new System.Collections.Generic.Stack<string>[cities.Count];
        CityPositions = new Vector2[cities.Count];
        for(int i = 0; i < cities.Count; i++) {
            CityPositions[i] = cities[i].GlobalPosition;
            CityThreats[i] = new System.Collections.Generic.Stack<string>();
        }
        CallDeferred(nameof(LoadThreats));
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public void CityClicked(Node viewportDetector, InputEvent eventDetected, int shapeIndex, int cityNumber) {
        if(eventDetected.IsAction("select") && eventDetected.IsPressed()) {
            GD.Print(cityNumber, " city clicked");
            // I think I want some sort of confirmation interface to pop up, and if the player
            // clicks accept then we'll go to the level
            SelectedCity = cityNumber;
            EmitSignal(nameof(SelectCity), CityPositions[cityNumber]);
        }
    }

    public void CityMouseEntered(int cityNumber) {
        GD.Print(cityNumber, " city mouse entered");
        EmitSignal(nameof(EnterCity), "", CityPositions[cityNumber], CityThreats[cityNumber].ToArray());
    }

    public void CityMouseExited(int cityNumber) {
        EmitSignal(nameof(ExitCity));
        GD.Print(cityNumber, " city mouse exited");
    }

    public void LoadThreats() {
        File threatSave = new File();
        if(!threatSave.FileExists("user://threats.save")) {
            GD.Print("No threat save file found");
            UpdateThreats();
            return;
        }
        threatSave.Open("user://threats.save", File.ModeFlags.Read);

        Godot.Collections.Dictionary<int, string[]> threatsDict = new Godot.Collections.Dictionary<int, string[]>((Godot.Collections.Dictionary)JSON.Parse(threatSave.GetLine()).Result);
        for(int i = 0; i < threatsDict.Count; i++) {
            for(int j = 0; j < threatsDict[i].Length; j++) {
                CityThreats[i].Push(threatsDict[i][j]);
            }
        }
        threatSave.Close();
    }

    public void SaveThreats() {
        Godot.Collections.Dictionary<int, string[]> threatsDict = new Godot.Collections.Dictionary<int, string[]>();
        
        File threatSave = new File();
        threatSave.Open("user://threats.save", File.ModeFlags.Write);
        for(int i = 0; i < CityThreats.Length; i++) {
            threatsDict.Add(i, new string[CityThreats[i].Count]);
            int j = 0;
            foreach(string threatString in CityThreats[i]) {
                threatsDict[i][j] = threatString;
            }
        }
        threatSave.StoreLine(JSON.Print(threatsDict));
        threatSave.Close();
    }

    public void SelectionConfirmed() {
        
    }

    public void UpdateThreats() {
        Godot.Collections.Array<ThreatRandomizer>threatRandomizers = new Godot.Collections.Array<ThreatRandomizer>(GetTree().GetNodesInGroup("ThreatSource"));
        if(threatRandomizers.Count > 0) {
            for(int i = 0; i < CityThreats.Length; i++) {
                CityThreats[i].Push(threatRandomizers[0].PickThreat());
            }
        }
        else {
            GD.Print("Error: No threat randomizer found");
        }
    }
}
