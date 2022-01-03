//By Gerrit Van Mol
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Timers;
using Globals;

namespace Logic
{
    public class Game : IGame
    {
        public Snake TheSnake { get; set; }
        public Snack TheSnack { get; private set; }
        public HighScore PlayerScores { get; set; }
        public Menu TheMenu = new Menu();
        public bool ValidName { get; private set; }
        public bool GameOver { get; private set; }
        public int Score { get; private set; }
        private string playerName;
        public string Player
        {
            get { return playerName; }
            set
            {
                if (value.Any(name => name >= 65 && name <= 90) || value.Any(name => name >= 97 && name <= 122) && 
                    (!Regex.Match(value, @"[²&é'(§èçà)-^$£ù%/:+=~,?€!<>]").Success))
                {
                    this.playerName = value;
                    ValidName = true;
                }
            } 
        }

        public bool SnackEaten { get; set; }
        public int Width { get => 50; }
        public int Height { get => 25; }
     
           
        

        private Timer timer;
        private readonly int interval = 500;

        private readonly Random rnd = new Random();

        public void Start()
        {
            GameOver = false;
            timer = new Timer(interval);
            timer.Elapsed += TimerElapsed;
            timer.Start();
        }

        private void Stop()
        {
            GameOver = true;
            timer.Stop();
        }

        public void Reset()
        {
            GameOver = false;
            Score = 0;
            TheSnake = new Snake(new Position(Width / 2, Height / 2));
            TheSnack = new Snack(rnd.Next(2, Width - 2), rnd.Next(2, Height - 2));
            SnackEaten = false;
            ValidName = false;
            
        }

        //need method for timer loop event so that the snake can move (change on screen)
        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (!GameOver)
            {
                TheSnake.Move();
                HitArenaOrSnack();
                OwnBodyHit();      
            }
        }


        private void HitArenaOrSnack()
        {
            var headPosition = TheSnake.BodySnake[0];
            if (headPosition.X <= 0 || headPosition.Y <= 0 || headPosition.X >= Width || headPosition.Y >= Height)
            {
                CreatePlayer();
                Stop();
            }
            if (headPosition.X == TheSnack.SnackX && headPosition.Y == TheSnack.SnackY)
            {
                TheSnake.GrowSnake();
                Score++;
                if (timer.Interval > 150) timer.Interval -= 50;
                if (timer.Interval <= 150) timer.Interval -= 2;
                NewSnack();
                SnackEaten = true;
            }
        }

        //need method to check if snake head is on same position as snack and or body
        private void OwnBodyHit()
        {
            if (TheSnake.BodySnake.Count > 3)
            {
                for (int i = 1; i < TheSnake.BodySnake.Count; i++)
                {
                    if (TheSnake.BodySnake[0].X.Equals(TheSnake.BodySnake[i].X) && TheSnake.BodySnake[0].Y.Equals(TheSnake.BodySnake[i].Y))
                    {
                        CreatePlayer();
                        Stop();
                    }
                }
            }
        }

        public void NewSnack()
        {
            TheSnack.SnackX = rnd.Next(2, Width - 2);
            TheSnack.SnackY = rnd.Next(2, Height - 2);
            Snack anotherSnack = TheSnack;
            anotherSnack.BadSnackX = rnd.Next(2, Width - 2);
            anotherSnack.BadSnackY = rnd.Next(2, Width - 2);
            for (int i = 0; i < TheSnake.BodySnake.Count; i++)
            {
                if (TheSnack.SnackX == TheSnake.BodySnake[i].X && TheSnack.SnackY == TheSnake.BodySnake[i].Y)
                {
                    TheSnack.SnackX = rnd.Next(2, Width - 2);
                    TheSnack.SnackY = rnd.Next(2, Height - 2);
                }
            }
        }

        public void CreatePlayer()
        {
            PlayerScores.NewPlayerCheck(playerName, Score);
        }
    }
}
