using Godot;
using System;

public class ThreatRandomizer : Node
{
    // I want an array for enemy types, separated into difficulties, and one
    // for enemy buffs, also separated into difficulties
    // I'll likely make and use data singletons for this, that seems best
    private string[] EasyAliens = new string[1] {"Alien Missile"};
    private string[] MediumAliens = new string[0] {};
    private string[] HardAliens = new string[0] {};
    private string[] EasyModifiers = new string[0] {};
    private string[] MediumModifiers = new string[0] {};
    private string[] HardModifiers = new string[0] {};
    private int MediumAlienThreshold = 3;
    private int HardAlienThreshold = 6;
    private int EasyModifierThreshold = 2;
    private int MediumModifierThreshold = 5;
    private int HardModifierThreshold = 8;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GD.Randomize();
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public void PickThreat(int difficultyLevel) {

    }

    private void ReadThreats() {

    }
}
