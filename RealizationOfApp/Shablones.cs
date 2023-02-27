using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealizationOfApp.ElementsOfGraph;

namespace RealizationOfApp
{
    public static class Shablones
    {
        public static CircleTextbox circleShablone = new();
        static Shablones()
        {
            circleShablone.SetCharacterSize(15);
            circleShablone.SetFillColorCircle(Color.Cyan);
            circleShablone.SetFillColorText(Color.Black);
            circleShablone.SetRadius(30);
            circleShablone.SetString(VertexGraph.Counter.ToString());
        }
    }
}
