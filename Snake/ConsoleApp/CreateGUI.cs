using System;
using System.Collections.Generic;
using System.Text;
using Logic;
using Globals;
using System.Net;
using System.Linq;
using System.Threading;
using System.Reflection;
using System.IO;
using System.Reflection.Metadata.Ecma335;

namespace ConsoleApp
{
    public class CreateGUI
    {
        ConsoleKey consoleKey;
        ConsoleKeyInfo consoleKeyInfo;
        Game game;
        private List<Position> previousSnake;
        public CreateGUI()
        {
            Console.Title = "Snake - The Game";
            Console.SetWindowSize(70, 35);
            consoleKey = new ConsoleKey();
            consoleKeyInfo = new ConsoleKeyInfo();
            NewGame();
            //game = new Game();
            //DisplayMenu();
        }

        private void NewGame()
        {
            try
            {
                game = new Game
                {
                    PlayerScores = new HighScore()
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Thread.Sleep(1000);
            }
            finally
            {
                DisplayMenu();
            }
        }

        public void GameLoop()
        { 
            DrawSnack();
            while (!game.GameOver)
            {
                VerifyInput();
                GameScore();
                if (previousSnake.Count > 0)
                {
                    DrawNothing(previousSnake.Last());
                }
                previousSnake = new List<Position>(game.TheSnake.BodySnake.ToArray());
                foreach (var snake in game.TheSnake.BodySnake.ToArray()) //System.InvalidOperationException: 'Collection was modified; enumeration operation may not execute.'
                {
                    DrawSnake(snake);
                    if (game.GameOver == true)
                    {
                        Console.SetCursorPosition(game.Width / 2, game.Height / 2);
                        Console.WriteLine("Game Over!!!");
                        Console.SetCursorPosition(game.Width - 16, game.Height + 3);
                    }
                }
                if (game.SnackEaten == true)
                {
                    DrawSnack();
                    game.SnackEaten = false;
                }            
            }
            
        }

        private void DisplayMenu()
        {
            Console.CursorVisible = false;        
                Console.Write(game.TheMenu.IntroTxt);
                Console.WriteLine("");
                while (true)
                {
                    for (int i = 0; i < game.TheMenu.Options.Count; i++)
                    {
                        if (i == game.TheMenu.Index)
                        {
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.WriteLine(game.TheMenu.Options[i]);
                        }
                        else
                        {
                            Console.WriteLine(game.TheMenu.Options[i]);
                        }
                        Console.ResetColor();
                    }
                    SelectOption();
                }
        }

        private void SelectOption()
        {
                    ReadKey();
            if (consoleKey == ConsoleKey.UpArrow)
            {
                if (game.TheMenu.Index <= 0)game.TheMenu.Index = game.TheMenu.Options.Count - 1;
                else { game.TheMenu.Index--; }
            } 
            else if (consoleKey == ConsoleKey.DownArrow) 
            {
                if (game.TheMenu.Index == game.TheMenu.Options.Count - 1) game.TheMenu.Index = 0;
                else { game.TheMenu.Index++; }
            }
            if (consoleKey == ConsoleKey.Enter)
            {
                if (game.TheMenu.Index == 0)
                {
                    Console.Clear();
                    MakeArena();
                    previousSnake = new List<Position>();
                    game.Reset();
                    PlayerName();
                    game.Start();
                    GameLoop();
                }
                else if (game.TheMenu.Index == 1)
                {
                    Console.WriteLine("Scores saved to desktop!");
                    Thread.Sleep(1500);
                    WriteScores();
                    
                }
                else if (game.TheMenu.Index == 2)
                {
                    WriteScores();
                }
            }
            Console.Clear();
                    Console.Write(game.TheMenu.IntroTxt);
                    Console.WriteLine("");
        }

        private void WriteScores()
        {
            try
            {
                game.PlayerScores.ValuesToDoc();
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                Thread.Sleep(1000);
            }
            finally
            {
                if (game.TheMenu.Index == 2)
                {
                    Environment.Exit(0);
                }
                Console.Clear();
                DisplayMenu();
            }
        }

        private void VerifyInput()
        {
            if (Console.KeyAvailable)
            {
                ReadKey();
                switch (consoleKey)
                {
                    case ConsoleKey.LeftArrow:
                        if (game.TheSnake.SnakeDirection == Direction.Right)
                        {
                            game.TheSnake.SnakeDirection = Direction.Right;
                        }
                        else
                        {
                            game.TheSnake.SnakeDirection = Direction.Left;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (game.TheSnake.SnakeDirection == Direction.Down)
                        {
                            game.TheSnake.SnakeDirection = Direction.Down;
                        }
                        else
                        {
                            game.TheSnake.SnakeDirection = Direction.Up;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (game.TheSnake.SnakeDirection == Direction.Left)
                        {
                            game.TheSnake.SnakeDirection = Direction.Left;
                        }
                        else
                        {
                            game.TheSnake.SnakeDirection = Direction.Right;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (game.TheSnake.SnakeDirection == Direction.Up)
                        {
                            game.TheSnake.SnakeDirection = Direction.Up;
                        }
                        else
                        {
                            game.TheSnake.SnakeDirection = Direction.Down;
                        }
                        break;
                }
            }
        }
        private void ReadKey()
        {
            consoleKeyInfo = Console.ReadKey(true);
            consoleKey = consoleKeyInfo.Key;
        }
        private void DrawSnack()
        {
            Console.SetCursorPosition(game.TheSnack.SnackX, game.TheSnack.SnackY);  // => System.NullReferenceException: 'Object reference not set to an instance of an object.'
            Console.Write("#");
            GameScore();
        }
        //Possible to use dictionary for user information and scores
        private void GameScore()
        {
            Console.SetCursorPosition(game.Width - 16, game.Height + 3);
            Console.Write($"Score: {game.Score}\r");
        }

        private void PlayerName()
        {
            Console.SetCursorPosition(0, game.Height + 2);
            Console.Write("Please enter your name: ");
            Console.SetCursorPosition(0, game.Height + 3);
            Console.Write(game.Player = Console.ReadLine() + "\r");
            Console.Write("                                 \r");
            while (game.ValidName == false)
            {
                Console.SetCursorPosition(0, game.Height + 2);
                Console.Write("                                 \r");
                Console.WriteLine("Name not valid!\r");
                Console.SetCursorPosition(0, game.Height + 3);
                Console.Write("                                 \r");
                Console.Write(game.Player = Console.ReadLine() + "\r");
                Console.Write("                                 \r");
            }
            Console.SetCursorPosition(0, game.Height + 2);
            Console.Write("                                 \r");
            Console.SetCursorPosition(0, game.Height + 4);
            Console.Write("Enjoy! Highscores will be saved to desktop!                     \r");
        }

        public void MakeArena()
        {
            ShowArena((game.Width + 1), (game.Height + 1), '/');//Corner bottom right
            ShowArena(0, (game.Height + 1), '\\');//Corner bottom left
            for (int i = 1; i <= game.Width; i++)//Horizontal line (width)
            {
                ShowArena(i, 0, '─');
                ShowArena(i, (game.Height + 1), '─');
            }
            ShowArena((game.Width + 1), 0, '\\');//Top right
            ShowArena(0, 0, '/');//Top left
            for (int i = 1; i <= (game.Height); i++) //Vertical line (height)
            {
                ShowArena(0, i, '│');
                ShowArena((game.Width + 1), i, '│');
            }
            Console.WriteLine("\n");
        }

        private void ShowArena(int x, int y, char sym)
        {
            Console.SetCursorPosition(x, y);
            Console.Write($"{sym}");
        }

        private void DrawSnake(Position snake)
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(snake.X, snake.Y);
            Console.Write("O");
        }

        private void DrawNothing(Position previousSnake)
        {
            Console.SetCursorPosition(previousSnake.X, previousSnake.Y);
            Console.Write(" ");        
        }   

       
    }
}
