using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kp
{

    public class FoodType
    {
        public int Score { get; }
        public Color Color { get; }
        public int Size { get; }

        public FoodType(int score, Color color, int size)
        {
            Score = score;
            Color = color;
            Size = size;
        }
    }
}