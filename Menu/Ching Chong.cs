﻿using System;
using System.Linq;
using System.Threading;
using KeyboardMenu;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.Diagnostics;
using System.Runtime;

//TODO Общая задача - при нажатии "Exit" все закрывалось сразу(вместе с консолью)
namespace KeyboardMenu
{

    public class Field
    {
        //Field
        public const int fieldLength = 50, fieldWidth = 15;
        const char fieldTile = '#';
        public string line = string.Concat(Enumerable.Repeat(fieldTile, fieldLength));

        public void CreateField()
        {
            //Print the borders
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(line);

            Console.SetCursorPosition(0, fieldWidth);
            Console.WriteLine(line);
        }
    }
    
    public class Rackets
    {
        Field field = new Field();
        //Rackets
        public int racketLength = Field.fieldWidth / 4;
        const char racketTile = '|';

        public int leftRacketHeight = 0;
        public int rightRacketHeight = 0;

        public void PrintTheRacket()
        {
            //Print the rackets
            for (int i = 0; i < racketLength; i++)
            {
                Console.SetCursorPosition(0, i + 1 + leftRacketHeight);
                Console.WriteLine(racketTile);
                Console.SetCursorPosition(Field.fieldLength - 1, i + 1 + rightRacketHeight);
                Console.WriteLine(racketTile);
                
            }
        }
    }

    public class Ball
    {
        //Ball
        public int ballX = Field.fieldLength / 2;
        public int ballY = Field.fieldWidth / 2;
        public char ballTile = 'O';

        public bool isBallGoingDown = true;
        public bool isBallGoingRight = true;
    }

    public class Points
    {
        //Points
        public int leftPlayerPoints = 0;
        public int rightPlayerPoints = 0;
    }

    public class Scoreboard
    {
        //Scoreboard
        public int scoreboardX = Field.fieldLength / 2 - 2;
        public int scoreboardY = Field.fieldWidth + 1;
    }

    public class Starter
    {
        //TODO Большой метод - сократить
        public static void Pong()
        {
            Field field = new Field();
            Rackets rackets = new Rackets();
            Points points = new Points();
            Scoreboard scoreboard = new Scoreboard();
            Ball ball = new Ball();
            Stopwatch time = new Stopwatch();
            time.Start();
            
            while (true)
            {
                field.CreateField();

                rackets.PrintTheRacket();

                //Do until a key is pressed
                while (!Console.KeyAvailable)
                {
                   
                    Console.SetCursorPosition(scoreboard.scoreboardX, scoreboard.scoreboardY);
                    Console.WriteLine($"{points.leftPlayerPoints} | {points.rightPlayerPoints}");

                    Console.SetCursorPosition(scoreboard.scoreboardX-10, scoreboard.scoreboardY+1);
                    Console.WriteLine(DateTime.Now.ToShortTimeString());
                    //Console.WriteLine($"{player1}   |   {player2}");


                    Console.SetCursorPosition(ball.ballX, ball.ballY);
                    Console.WriteLine(ball.ballTile);
                    Thread.Sleep(100); //Adds a timer so that the players have time to react

                    Console.SetCursorPosition(ball.ballX, ball.ballY);
                    Console.WriteLine(" "); //Clears the previous position of the ball

                    //Update position of the ball
                    if (ball.isBallGoingDown)
                    {
                        ball.ballY++;
                    }
                    else
                    {
                        ball.ballY--;
                    }

                    if (ball.isBallGoingRight)
                    {
                        ball.ballX++;
                    }
                    else
                    {
                        ball.ballX--;
                    }

                    if (ball.ballY == 1 || ball.ballY == Field.fieldWidth - 1)
                    {
                        ball.isBallGoingDown = !ball.isBallGoingDown; //Change direction
                    }

                    //TODO Создать метод победы игрока (любого) [для сокращения кода]
                    if (ball.ballX == 1)
                    {
                        if (ball.ballY >= rackets.leftRacketHeight + 1 && ball.ballY <= rackets.leftRacketHeight + rackets.racketLength) //Left racket hits the ball and it bounces
                        {
                            ball.isBallGoingRight = !ball.isBallGoingRight;
                        }
                        else //Ball goes out of the field; Right player scores
                        {
                            points.rightPlayerPoints++;
                            ball.ballY = Field.fieldWidth / 2;
                            ball.ballX = Field.fieldLength / 2;
                            Console.SetCursorPosition(scoreboard.scoreboardX, scoreboard.scoreboardY);
                            Console.WriteLine($"{points.leftPlayerPoints} | {points.rightPlayerPoints}");
                            if (points.rightPlayerPoints == 10)
                            {
                                time.Stop();
                                //TODO DateTime.Now.ToShortTimeString() in database

                                goto outer;
                            }
                        }
                    }

                    if (ball.ballX == Field.fieldLength - 2)
                    {
                        if (ball.ballY >= rackets.rightRacketHeight + 1 && ball.ballY <= rackets.rightRacketHeight + rackets.racketLength) //Right racket hits the ball and it bounces
                        {
                            ball.isBallGoingRight = !ball.isBallGoingRight;
                        }
                        else //Ball goes out of the field; Left player scores
                        {
                            points.leftPlayerPoints++;
                            ball.ballY = Field.fieldWidth / 2;
                            ball.ballX = Field.fieldLength / 2;
                            Console.SetCursorPosition(scoreboard.scoreboardX, scoreboard.scoreboardY);
                            Console.WriteLine($"{points.leftPlayerPoints} | {points.rightPlayerPoints}");
                            if (points.leftPlayerPoints == 10)
                            {
                                time.Stop();
                                //TODO DateTime.Now.ToShortTimeString() in database

                                goto outer;
                            }
                        }
                    }
                }

                //Check which key has been pressed
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Escape:
                        {
                            Console.SetCursorPosition(Field.fieldLength/2, Field.fieldWidth/2);
                            Console.WriteLine("_Paused");
                            //Невозможно вызвать RunMainMenu
                            Console.ReadKey();
                            Clear();
                            break;
                        }
                    case ConsoleKey.UpArrow:
                        if (rackets.rightRacketHeight > 0)
                        {
                            rackets.rightRacketHeight--;
                        }
                        break;

                    case ConsoleKey.DownArrow:
                        if (rackets.rightRacketHeight < Field.fieldWidth - rackets.racketLength - 1)
                        {
                            rackets.rightRacketHeight++;

                        }
                        break;

                    case ConsoleKey.W:
                        if (rackets.leftRacketHeight > 0)
                        {
                            rackets.leftRacketHeight--;
                        }
                        break;

                    case ConsoleKey.S:
                        if (rackets.leftRacketHeight < Field.fieldWidth - rackets.racketLength - 1)
                        {
                            rackets.leftRacketHeight++;
                        }
                        break;
                }



                //Clear the rackets’ previous positions
                for (int i = 1; i < Field.fieldWidth; i++)
                {
                    Console.SetCursorPosition(0, i);
                    Console.WriteLine(" ");
                    Console.SetCursorPosition(Field.fieldLength - 1, i);
                    Console.WriteLine(" ");
                }
            }
        outer:;
            Console.Clear();
            Console.SetCursorPosition(0, 0);

            if (points.rightPlayerPoints == 10)
            {
                Console.WriteLine("Right player won!");
                Console.WriteLine(time.Elapsed);
            }
            else
            {
                Console.WriteLine("Left player won!");
                Console.WriteLine(time.Elapsed);
            }
        }
    }
}