using Globals;
using System.Collections.Generic;

namespace Logic
{
  public interface IGame
        {
            bool GameOver { get; }
            int Height { get; }
            bool SnackEaten { get; set; }
            Snack TheSnack { get; }
            Snake TheSnake { get; set; }
            int Width { get; }
     

        void NewSnack();
            void Reset();
            void Start();
        }
    }