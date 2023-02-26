using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealizationOfApp
{
    public class EnterTextEdge:EventDrawable
    {
        public Textbox textbox = new();
        protected Clock clock = new();
        protected EdgeEv edge;
        protected int weight = 1;
        public EnterTextEdge(Vector2f position,EdgeEv edgeEv)
        {
            textbox.SetColorText(Color.Black);
            textbox.SetFillColorRect(Color.White);
            textbox.SetSizeCharacterText(15);
            textbox.SetSizeRect(20, 20);
            textbox.SetString("");
            textbox.SetPos(position);
            edge = edgeEv;
        }
        public override void KeyPressed(object? source, KeyEventArgs e)
        {
            if(IsAlive && e.Code!=Keyboard.Key.Enter)
            {
                textbox.SetString(textbox.GetString()+ConvertToInt(e.Code));
            }
            else if(IsAlive && e.Code==Keyboard.Key.Enter)
            {
                IsAlive = false;
                if (!Int32.TryParse(textbox.GetString(), out weight))
                    weight = 1;
                edge.SetWeight(weight);
                IsNeedToRemove = true;
            }
        }
        public override void Draw(RenderTarget target, RenderStates states)
        {
            Vector2f t = new(textbox.GetText().GetGlobalBounds().Left+textbox.GetText().GetGlobalBounds().Width+1, textbox.GetText().GetGlobalBounds().Top-2);
            Vector2f d = new(textbox.GetText().GetGlobalBounds().Left+textbox.GetText().GetGlobalBounds().Width+1, textbox.GetText().GetGlobalBounds().Top+15);
            textbox.Draw(target, states);
            if (clock.ElapsedTime.AsSeconds()>1 && clock.ElapsedTime.AsSeconds()<2)
            {
                target.Draw(new Vertex[] { new Vertex(t, Color.Black), new Vertex(d, Color.Black) }, PrimitiveType.Lines, states);
            }
            else if (clock.ElapsedTime.AsSeconds()>2)
                clock.Restart();
            textbox.Draw(target, states);
        }
        public string ConvertToInt(Keyboard.Key key)
        {
            string s = "";
            switch (key)
            {
                case Keyboard.Key.Num0:
                    s="0";
                    break;
                case Keyboard.Key.Num1:
                    s="1";
                    break;
                case Keyboard.Key.Num2:
                    s="2";
                    break;
                case Keyboard.Key.Num3:
                    s="3";
                    break;
                case Keyboard.Key.Num4:
                    s="4";
                    break;
                case Keyboard.Key.Num5:
                    s="5";
                    break;
                case Keyboard.Key.Num6:
                    s="6";
                    break;
                case Keyboard.Key.Num7:
                    s="7";
                    break;
                case Keyboard.Key.Num8:
                    s="8";
                    break;
                case Keyboard.Key.Num9:
                    s="9";
                    break;
            }

            return s;
        }
    }
}
