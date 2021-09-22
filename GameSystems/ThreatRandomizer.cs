using Godot;
using System;

public class ThreatRandomizer : Node
{
    // I want an array for enemy types, separated into difficulties, and one
    // for enemy buffs, also separated into difficulties
    // I'll likely make and use data singletons for this, that seems best
    private string[] EasyAliens = new string[3] {"Alien Missile", "Alien Drone", "Alien Planet Drill"};
    private string[] MediumAliens = new string[3] {"Alien Fighter", "Alien Scout", "Alien Inhibitor"};
    private string[] HardAliens = new string[3] {"Alien Bomber", "Alien Carrier", "Alien Artillery"};
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

    public string PickThreat(bool alienThreat, int difficultyLevel) {
        // If the alien bool is true, get a new alien threat to return to the
        // caller, otherwise get a modifier. The pool to choose from depends on the 
        // difficulty level passed in
        if(alienThreat) {
            if(difficultyLevel < MediumAlienThreshold) {

            }
            else if(difficultyLevel < HardAlienThreshold) {

            }
            else {

            }
        }
        else {
            if(difficultyLevel < EasyModifierThreshold) {
                GD.Print("Error: no modifier can be assigned");
            }
            else if(difficultyLevel < MediumModifierThreshold) {

            }
            else if(difficultyLevel < HardModifierThreshold) {

            }
            else {

            }
        }
        return "";
    }
}
