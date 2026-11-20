using Godot;
using System;

public partial class MainMenu : Control
{
	public override void _Ready()
	{
	    GetNode<Button>("VBoxContainer/PlayButton").Pressed += OnPlayButtonPressed;
	    GetNode<Button>("VBoxContainer/QuitButton").Pressed += OnQuitButtonPressed;
	}
    public void OnPlayButtonPressed()
	{
		
		GetTree().ChangeSceneToFile("res://main.tscn");
	
	}

    public void OnQuitButtonPressed()
    {
        GetTree().Quit();
    }
}
