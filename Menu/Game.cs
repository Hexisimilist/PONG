using KeyboardMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace KeyboardMenu
{

    class Game
    {
        public void Start()
        {

            Title = "Ping Pong Game";
            RunMainMenu();

        }
        private void RunMainMenu()
        {


            string prompt = @"

██████╗ ██╗███╗   ██╗ ██████╗     ██████╗  ██████╗ ███╗   ██╗ ██████╗      ██████╗  █████╗ ███╗   ███╗███████╗
██╔══██╗██║████╗  ██║██╔════╝     ██╔══██╗██╔═══██╗████╗  ██║██╔════╝     ██╔════╝ ██╔══██╗████╗ ████║██╔════╝
██████╔╝██║██╔██╗ ██║██║  ███╗    ██████╔╝██║   ██║██╔██╗ ██║██║  ███╗    ██║  ███╗███████║██╔████╔██║█████╗  
██╔═══╝ ██║██║╚██╗██║██║   ██║    ██╔═══╝ ██║   ██║██║╚██╗██║██║   ██║    ██║   ██║██╔══██║██║╚██╔╝██║██╔══╝  
██║     ██║██║ ╚████║╚██████╔╝    ██║     ╚██████╔╝██║ ╚████║╚██████╔╝    ╚██████╔╝██║  ██║██║ ╚═╝ ██║███████╗
╚═╝     ╚═╝╚═╝  ╚═══╝ ╚═════╝     ╚═╝      ╚═════╝ ╚═╝  ╚═══╝ ╚═════╝      ╚═════╝ ╚═╝  ╚═╝╚═╝     ╚═╝╚══════╝
                ";


            string[] options = { "Play", "About", "Exit" };
            Menu mainmenu = new Menu(prompt, options);
            int selectedindex = mainmenu.Run();

            
            switch (selectedindex)
            {
                case 0:
                    PlayGame();
                    break;
                    case 1:
                    DisplayAboutInfo();
                    break;
                case 2:
                    ExitGame();
                    break;


            }

        }
     

       
        private void PlayGame()
        {
            Clear();
            Starter.Pong();
        }

        private void DisplayAboutInfo()
        {
            Clear();
            WriteLine (@"
 ██████╗ ██╗   ██╗████████╗███████╗██████╗     ██╗  ██╗███████╗ █████╗ ██╗   ██╗███████╗███╗   ██╗
██╔═══██╗██║   ██║╚══██╔══╝██╔════╝██╔══██╗    ██║  ██║██╔════╝██╔══██╗██║   ██║██╔════╝████╗  ██║
██║   ██║██║   ██║   ██║   █████╗  ██████╔╝    ███████║█████╗  ███████║██║   ██║█████╗  ██╔██╗ ██║
██║   ██║██║   ██║   ██║   ██╔══╝  ██╔══██╗    ██╔══██║██╔══╝  ██╔══██║╚██╗ ██╔╝██╔══╝  ██║╚██╗██║
╚██████╔╝╚██████╔╝   ██║   ███████╗██║  ██║    ██║  ██║███████╗██║  ██║ ╚████╔╝ ███████╗██║ ╚████║
 ╚═════╝  ╚═════╝    ╚═╝   ╚══════╝╚═╝  ╚═╝    ╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝  ╚═══╝  ╚══════╝╚═╝  ╚═══╝                                             

                                  
Directed by:

Milushev Timur

Nekrasov Vladislav

Arseniy Mazurenko

Borisov Ivan

");
            ReadKey(true);

            RunMainMenu();

        }


        private void ExitGame()
        {
            WriteLine("\nPress any key to exit.");
            ReadKey(true);
            Environment.Exit(0);
        }
    }
}
