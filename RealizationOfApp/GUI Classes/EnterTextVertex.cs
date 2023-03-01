using RealizationOfApp.ElementsOfGraph;

namespace RealizationOfApp
{
    public class EnterTextVertex:EventDrawable
    {
        public Textbox textbox = new();
        protected Clock clock = new();
        protected VertexGraph vertex;
        public EnterTextVertex(Vector2f position, VertexGraph vertexEv)
        {
            textbox.SetColorText(Color.Black);
            textbox.SetFillColorRect(Color.Magenta);
            textbox.SetSizeCharacterText(15);
            textbox.SetSizeRect(20, 20);
            textbox.SetString("");
            textbox.SetPos(position);
            vertex = vertexEv;
        }
        public override void KeyPressed(object? source, KeyEventArgs e)
        {
            if (IsAlive && e.Code!=Keyboard.Key.Enter)
            {
                textbox.SetString(ConvertToString(textbox.GetString(),e.Code));
            }
            else if (IsAlive && e.Code==Keyboard.Key.Enter && source is Application app)
            {
                IsAlive = false;
                string oldName = vertex.GetString(),newName = textbox.GetString()=="" || textbox.GetString() is null ? VertexGraph.Counter.ToString():textbox.GetString();
                if (!app.graph.ContainsName(newName))
                {
                    vertex.SetName(newName);
                    app.graph.ChangeName(oldName, newName);
                    app.messageToUser.SetString("");
                }
                else
                {
                    app.messageToUser.SetString("This name already exists in graph");
                    Console.WriteLine("This name already exists in graph");
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
        public string ConvertToString(string text,Keyboard.Key key)
        {
            if(key!=Keyboard.Key.LShift && key!=Keyboard.Key.Backspace && key>=Keyboard.Key.A && key<=Keyboard.Key.Z)
            {
                text+=(char)(key+65);
            }
            else if (key==Keyboard.Key.Backspace && text.Length>0)
            {
                text = text.Remove(text.Length-1,1);
            }
            return text;
        }

    }
}

