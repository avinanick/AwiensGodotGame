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
    private string[] EasyModifiers = new string[2] {"Jamming", "Smokescreen"};
    private string[] MediumModifiers = new string[3] {"Shielded", "Elites", "Boosted"};
    private string[] HardModifiers = new string[1] {"Swarming"};
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

    public string PickThreat() {
        // If the alien bool is true, get a new alien threat to return to the
        // caller, otherwise get a modifier. The pool to choose from depends on the 
        // difficulty level passed in
        int difficultyLevel = GetNode<CampaignTracker>("/root/CampaignTrackerAL").GetCurrentDifficulty();
        bool alienThreat = (difficultyLevel % 2 == 0 || difficultyLevel < EasyModifierThreshold);
        string[] relevantThreats = new string[0];
        if(alienThreat) {
            if(difficultyLevel < MediumAlienThreshold) {
                // Pick randomly from easy aliens
                relevantThreats = EasyAliens;
            }
            else if(difficultyLevel < HardAlienThreshold) {
                // Pick randomly from easy and medium aliens
                relevantThreats = new string[EasyAliens.Length + MediumAliens.Length];
                Array.Copy(EasyAliens, relevantThreats, EasyAliens.Length);
                Array.Copy(MediumAliens, 0, relevantThreats, EasyAliens.Length, MediumAliens.Length);
            }
            else {
                // Pick randomly from all aliens
                relevantThreats = new string[EasyAliens.Length + MediumAliens.Length + HardAliens.Length];
                Array.Copy(EasyAliens, relevantThreats, EasyAliens.Length);
                Array.Copy(MediumAliens, 0, relevantThreats, EasyAliens.Length, MediumAliens.Length);
                Array.Copy(HardAliens, 0, relevantThreats, EasyAliens.Length + MediumAliens.Length, HardAliens.Length);
            }
        }
        else {
            if(difficultyLevel < EasyModifierThreshold) {
                GD.Print("Error: no modifier can be assigned");
            }
            else if(difficultyLevel < MediumModifierThreshold) {
                // Pick randomly from the easy modifiers
                relevantThreats = EasyModifiers;
            }
            else if(difficultyLevel < HardModifierThreshold) {
                // Pick randomly from the easy and medium modifiers
                relevantThreats = new string[EasyModifiers.Length + MediumModifiers.Length];
                Array.Copy(EasyModifiers, relevantThreats, EasyModifiers.Length);
                Array.Copy(MediumModifiers, 0, relevantThreats, EasyModifiers.Length, MediumModifiers.Length);
            }
            else {
                // Pick randomly from all modifiers
                relevantThreats = new string[EasyModifiers.Length + MediumModifiers.Length + HardModifiers.Length];
                Array.Copy(EasyModifiers, relevantThreats, EasyModifiers.Length);
                Array.Copy(MediumModifiers, 0, relevantThreats, EasyModifiers.Length, MediumModifiers.Length);
                Array.Copy(HardModifiers, 0, relevantThreats, EasyModifiers.Length + MediumModifiers.Length, HardModifiers.Length);
            }
        }
        if(relevantThreats.Length > 0) {
        int index = Mathf.Abs((int)(GD.Randi() % relevantThreats.Length));
        return relevantThreats[index];
        }
        return "";
    }
}
