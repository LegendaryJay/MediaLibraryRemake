﻿using ConsoleApp1.ConsoleMenus.Multi_purpose;

namespace ConsoleApp1.ConsoleMenus.Top;

public class MainMenu : MenuBase
{
    public MainMenu() : base("Main Menu", 0)
    {
        ThisMenu.Add("Movies", () =>
            {
                var mediaMenu = new MediaMenu.MediaMenu();
                mediaMenu.Run();
            }
        );
        // .Add("Users", () =>
        //     {
        //         
        //     }
        // )
    }
}