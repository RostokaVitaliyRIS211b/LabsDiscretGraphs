using RealizationOfApp.ElementsOfGraph;

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
            textbox.SetFillColorRect(new(236, 253, 230));
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
                textbox.SetString(ConvertToInt(textbox.GetString(), e.Code));
            }
            else if(IsAlive && e.Code==Keyboard.Key.Enter && source is Application app)
            {
                IsAlive = false;
                bool isCorrect = Int32.TryParse(textbox.GetString(), out weight);
                if (!isCorrect)
                {
                    weight = 1;
                    app.messageToUser.SetString("Weight is not type correctly");
                }
                else
                {
                    edge.SetWeight(weight);
                    app.messageToUser.SetString("");
                }
                app.graph[edge.startVer.GetString(), edge.endVer.GetString()]=weight;
                if(!app.IsOriented)
                {
                    EdgeEv? edgeEv = edge.startVer.incindentEdges.Find(x => x.endVer==edge.startVer && x.startVer==edge.endVer);
                    if(edgeEv is not null)
                    {
                        edgeEv.SetWeight(weight);

                    }
                }
                foreach (EventDrawable ev in app.eventDrawables)
                    ev.IsAlive=true;
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
        public string ConvertToInt(string text,Keyboard.Key key)
        {
            switch (key)
            {
                case Keyboard.Key.Num0:
                    text+="0";
                    break;
                case Keyboard.Key.Num1:
                    text+="1";
                    break;
                case Keyboard.Key.Num2:
                    text+="2";
                    break;
                case Keyboard.Key.Num3:
                    text+="3";
                    break;
                case Keyboard.Key.Num4:
                    text+="4";
                    break;
                case Keyboard.Key.Num5:
                    text+="5";
                    break;
                case Keyboard.Key.Num6:
                    text+="6";
                    break;
                case Keyboard.Key.Num7:
                    text+="7";
                    break;
                case Keyboard.Key.Num8:
                    text+="8";
                    break;
                case Keyboard.Key.Num9:
                    text+="9";
                    break;
                case Keyboard.Key.Backspace:
                    if(text.Length>0)
                        text = text.Remove(text.Length-1, 1);
                    break;
            }

            return text;
        }
    }
}
