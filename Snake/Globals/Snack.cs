using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Globals
{
    public class Snack
    {
        public int SnackX { get; set; }
        public int SnackY { get; set; }

        public int BadSnackX { get; set; }
        public int BadSnackY { get; set; }
        public int NegativeScore { get; }
        public Snack(int snackX, int snackY)
        {
            SnackX = snackX;
            SnackY = snackY;
        }

        public Snack(int badSnackX, int badSnackY, int negativeScore)
        {
            BadSnackX = badSnackX;
            BadSnackY = badSnackY;
            NegativeScore = negativeScore;
        }
        //properties of a snack are its position
    }
}
