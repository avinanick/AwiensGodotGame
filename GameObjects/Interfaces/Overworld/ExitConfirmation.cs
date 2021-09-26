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
        base._Ready();
        Visible = false;
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public void AbandonCancelled() {
        // Close the menu
        GetNode<Control>("DoubleCheck").Visible = false;
        GetNode<Control>("VBoxContainer").Visible = true;
    }

    public void AbandonClicked() {
        GetNode<Control>("VBoxContainer").Visible = false;
        GetNode<Control>("DoubleCheck").Visible = true;
    }

    public void AbandonConfirmed() {
        // Use the abandon world thing I'll put in a singleton?
    }

    public void QuitCancelled() {
        Visible = false;
    }

    public void QuitClicked() {
        // This should just quit to main menu
    }
}
