using Godot;
using System;

public class ExitConfirmation : PanelContainer
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public void AbandonCancelled() {
        // Close the menu
    }

    public void AbandonClicked() {

    }

    public void AbandonConfirmed() {
        // Use the abandon world thing I'll put in a singleton?
    }

    public void QuitCancelled() {
        
    }

    public void QuitClicked() {
        // This should just quit to main menu
    }
}
