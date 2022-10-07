using Menu;
using System.Diagnostics;
using System.ComponentModel.DataAnnotations.Schema;

using static System.Console;

//TODO Общая задача - при нажатии "Exit" все закрывалось сразу(вместе с консолью)
namespace KeyboardMenu
{
    public class Session
    {
        public int SessionId { get; set; }

        public Ball ball { get; set; }
        public Player LeftPlayer { get; set; }
        public Player RightPlayer { get; set; }
        public Field field { get; set; }
        public Scoreboard ScoreBoard { get; set; }
        public DateTime StartTime { get; set; }
        public Stopwatch GameDuration { get; set; }

        public bool FinishFlag { get; set; }
        public Session()
        {

        }
        public Session(string LeftPlayerName, string RightPlayerName)
        {
            ball = new Ball();
            LeftPlayer = new Player(LeftPlayerName);
            RightPlayer = new Player(RightPlayerName);
            field = new Field();
            ScoreBoard = new Scoreboard();
            StartTime = DateTime.Now;
            GameDuration = new Stopwatch();
            FinishFlag = false;
        }

        public void UpdateRound()
        {
            Console.SetCursorPosition(ScoreBoard.X, ScoreBoard.Y);
            Console.WriteLine($"{LeftPlayer.Points} | {RightPlayer.Points}");
           
            Console.SetCursorPosition(ScoreBoard.X, ScoreBoard.Y + 1);
            Console.WriteLine($"{LeftPlayer.Name}   |   {RightPlayer.Name}");
                     
           //TODO перенести это в данные: DateTime.Now.ToShortTimeString();
            
            Console.SetCursorPosition(ball.X, ball.Y);
            Console.WriteLine(Ball.BallTile);
            Thread.Sleep(100); //Adds a timer so that the players have time to react


            Console.SetCursorPosition(ball.X, ball.Y);
            Console.WriteLine(" "); //Clears the previous position of the ball
            
            TimeSpan ts = GameDuration.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
            Console.SetCursorPosition(Field.fieldLength / 2 - 2, Field.fieldWidth + 3);
            Console.WriteLine(elapsedTime);
        }
        public void UpdateBall()
        {
            if (ball.isBallGoingDown)
            {
                ball.Y++;
            }
            else
            {
                ball.Y--;
            }

            if (ball.isBallGoingRight)
            {
                ball.X++;
            }
            else
            {
                ball.X--;
            }

            if (ball.Y == 1 || ball.Y == Field.fieldWidth - 1)
            {
                ball.isBallGoingDown = !ball.isBallGoingDown; //Change direction
            }
        }
        public void UpdateRackets()
        {
            for (int i = 1; i < Field.fieldWidth; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.WriteLine(" ");
                Console.SetCursorPosition(Field.fieldLength - 1, i);
                Console.WriteLine(" ");
            }
        }
        public bool IsRightKnockBall()
        {
            if (ball.Y >= LeftPlayer.Racket.Height + 1 && ball.Y <= LeftPlayer.Racket.Height + Rackets.Length) //Left racket hits the ball and it bounces
            {
                //TODO Убрать отсюда смену направления
                return false;
            }
            return true;
        }
        public bool IsLeftKnockBall()
        {

            if (ball.Y >= RightPlayer.Racket.Height + 1 && ball.Y <= RightPlayer.Racket.Height + Rackets.Length) //Right racket hits the ball and it bounces
            {
                //TODO Убрать отсюда смену направления
                return false;
            }
            return true;
        }
        public void PrintTheRacket()
        {
            for (int i = 0; i < Rackets.Length; i++)
            {
                Console.SetCursorPosition(0, i + 1 + LeftPlayer.Racket.Height);
                Console.WriteLine(Rackets.Tile);
                Console.SetCursorPosition(Field.fieldLength - 1, i + 1 + RightPlayer.Racket.Height);
                Console.WriteLine(Rackets.Tile);
            }
        }
        public void Start()
        {
            GameDuration.Start();
            while (!FinishFlag)
            {
                field.CreateField();
                PrintTheRacket();

                while (!Console.KeyAvailable)
                {
                    UpdateRound();
                    UpdateBall();

                    if (ball.X == 1)
                        if (!IsRightKnockBall()) { ball.isBallGoingRight = !ball.isBallGoingRight; }
                        else
                        {
                            RightPlayer.Points++;
                            ball.Y = Field.fieldWidth / 2;
                            ball.X = Field.fieldLength / 2;
                            Console.SetCursorPosition(ScoreBoard.X, ScoreBoard.Y);
                            Console.WriteLine($"{LeftPlayer.Points} | {RightPlayer.Points}");
                            if (RightPlayer.Points == 1)
                            {
                                GameDuration.Stop();
                                //TODO DateTime.Now.ToShortTimeString() in database
                                RightPlayer.Win();
                                Console.WriteLine(GameDuration.Elapsed);
                                FinishFlag = true;
                                break;
                            }
                        }
                    if (ball.X == Field.fieldLength - 2)
                        if (!IsLeftKnockBall()) { ball.isBallGoingRight = !ball.isBallGoingRight; }
                        else
                        {
                            LeftPlayer.Points++;
                            ball.Y = Field.fieldWidth / 2;
                            ball.X = Field.fieldLength / 2;
                            Console.SetCursorPosition(ScoreBoard.X, ScoreBoard.Y);
                            Console.WriteLine($"{LeftPlayer.Points} | {RightPlayer.Points}");
                            if (LeftPlayer.Points == 1)
                            {
                                GameDuration.Stop();
                                //TODO DateTime.Now.ToShortTimeString() in database
                                LeftPlayer.Win();
                                Console.WriteLine(GameDuration.Elapsed);
                                FinishFlag = true;
                                break;
                            }
                        }



                }
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Escape:
                        {

                            Console.SetCursorPosition(Field.fieldLength / 2, Field.fieldWidth / 2);                            
                            Console.WriteLine("_Paused");
                            Console.ReadKey();
                            Clear();
                            //TODO Добавить Update раунда и мяча
                            break;
                        }
                    case ConsoleKey.UpArrow:
                        if (RightPlayer.Racket.Height > 0)
                        {
                            RightPlayer.Racket.Height--;
                        }
                        break;

                    case ConsoleKey.DownArrow:
                        if (RightPlayer.Racket.Height < Field.fieldWidth - Rackets.Length - 1)
                        {
                            RightPlayer.Racket.Height++;

                        }
                        break;

                    case ConsoleKey.W:
                        if (LeftPlayer.Racket.Height > 0)
                        {
                            LeftPlayer.Racket.Height--;
                        }
                        break;

                    case ConsoleKey.S:
                        if (LeftPlayer.Racket.Height < Field.fieldWidth - Rackets.Length - 1)
                        {
                            LeftPlayer.Racket.Height++;
                        }
                        break;
                }
                UpdateRackets();


            }

            
        }
    }

    public class Player
    {
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }
        [NotMapped]
        public Rackets Racket { get; set; }

        public void Win()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"{Name} победил!");
            //TODO Вставить [Console.WriteLine(GameDuration.Elapsed);] после победы одного из игроков
        }

        public Player()
        {

        }

        public Player(string name)
        {
            Name = name;
            Points = 0;
            Racket = new Rackets();
        }
    }

    public enum GameSettings
    {
        fieldLength = 50,
        fieldWidth = 15
    }

    public class Field
    {
        //Field
        public static int fieldLength = (int)GameSettings.fieldLength;
        public static int fieldWidth = (int)GameSettings.fieldWidth;

        public const char fieldTile = '#';
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
        public int RacketsId { get; set; }
        public static int Length = Field.fieldWidth / 4;
        public const char Tile = '|';
        public int Height { get; set; } = 0;


    }

    public class Ball
    {
        public int X { get; set; } = Field.fieldLength / 2;
        public int Y { get; set; } = Field.fieldWidth / 2;

        public const char BallTile = 'O';

        public bool isBallGoingDown { get; set; } = true;
        public bool isBallGoingRight { get; set; } = true;

    }

    public class Scoreboard
    {
        //Scoreboard
        public int X { get; private set; } = Field.fieldLength / 2 - 2;
        public int Y { get; private set; } = Field.fieldWidth + 1;
    }

}