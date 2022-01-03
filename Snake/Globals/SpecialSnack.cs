using System;
using System.Collections.Generic;
using System.Text;

namespace Globals
{
    public class SpecialSnack : Snack
    {

        public int SpecialX { get; set; }
        public int SpecialY { get; set; }
        public SpecialSnack(int snackX, int snackY) : base(snackX, snackY)
        {
            SpecialX = snackX;
            SpecialY = snackY;
        }
    }
}
