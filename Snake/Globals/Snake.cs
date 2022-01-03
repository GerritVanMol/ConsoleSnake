using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Timers;

namespace Globals
{
    public class Snake
    {
        public Direction SnakeDirection { get; set; }
        private Position tailLastPosition;
        public List<Position> BodySnake { get; private set; }

        public Snake(Position position)
        {
            BodySnake = new List<Position> {new Position(position.X, position.Y)};
        }

        public void Move()
        {
            for (int i = BodySnake.Count - 1; i > 0; i--)
            {
                BodySnake[i] = BodySnake[i - 1];
            }
            switch (SnakeDirection)
            {
                case Direction.Up:           
                        BodySnake[0] = new Position(BodySnake[0].X, BodySnake[0].Y - 1);
                        break;
                case Direction.Down:
                        BodySnake[0] = new Position(BodySnake[0].X, BodySnake[0].Y + 1);
                            break;
                case Direction.Left:
                        BodySnake[0] = new Position(BodySnake[0].X - 1, BodySnake[0].Y);
                    break;
                case Direction.Right:
                        BodySnake[0] = new Position(BodySnake[0].X + 1, BodySnake[0].Y);
                    break;
                default:
                    BodySnake[0] = new Position(BodySnake[0].X, BodySnake[0].Y);
                    break;
            }
            tailLastPosition = BodySnake.Last();
        }
        
        public void GrowSnake()
        {
            BodySnake.Add(tailLastPosition);
        }
       
    }
}
