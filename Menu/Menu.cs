using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace KeyboardMenu
{

    class Menu
    {
        private int SelectedIndex;
        private string[] Options;
        private string WriteText;
       
        public Menu(string prompt, string[] options)
        {
            WriteText = prompt;
            Options = options;
            SelectedIndex = 0;
        }
        

        public void DisplayOptions()
        {
            WriteLine(WriteText);
            for (int i = 0; i < Options.Length; i++)
            {
                string currentOption = Options[i];
                string prefix;


                if(i == SelectedIndex)
                {
                    prefix = " *";
                    ForegroundColor = ConsoleColor.Black;
                  BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    prefix = "  ";
                    ForegroundColor = ConsoleColor.White;
                    BackgroundColor = ConsoleColor.Black;
                }

                WriteLine($"{prefix} << {currentOption} >>");

            }
            ResetColor();
        }
        public int Run()
        {

            

            ConsoleKey keyPressed;
            do
            {

                Clear();
                DisplayOptions();


                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.UpArrow)
                {
                    SelectedIndex--;
                    if (SelectedIndex == -1) 
                    {
                        SelectedIndex = Options.Length - 1;
                    }
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    SelectedIndex++;
                    if(SelectedIndex == Options.Length)
                    {
                        SelectedIndex = 0;
                    }
                }
        
            }while (keyPressed != ConsoleKey.Enter);
           
            return SelectedIndex;

        }
    }
}
