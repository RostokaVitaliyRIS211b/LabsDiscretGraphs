

namespace RealizationOfApp.GUI_Classes
{
    public class ButtonDeikstra:EvTextbox
    {
        public Color BuffColor;
        bool isUsed = false;
        public List<ButtonDeikstraList> buttons;
        public ButtonDeikstra(Textbox textbox) : base(textbox)
        {
            BuffColor = textbox.GetFillRectColor();
            buttons = new();

        }
        public override void MouseMoved(object? source, ICollection<EventDrawableGUI> elementsOfGUI, MouseMoveEventArgs e)
        {
            if (IsAlive && textbox.Contains(e.X, e.Y))
            {
                textbox.SetFillColorRect(Color.Magenta);
            }
            else
            {
                textbox.SetFillColorRect(BuffColor);
            }
        }
        public override void KeyPressed(object? source, ICollection<EventDrawableGUI> elementsOfGUI, KeyEventArgs e)
        {
            if(IsAlive && isUsed && e.Code!=Keyboard.Key.Enter &&(  
                e.Code>=Keyboard.Key.A &&  e.Code<=Keyboard.Key.Z || 
                e.Code>=Keyboard.Key.Num0 &&  e.Code<=Keyboard.Key.Num9 || e.Code == Keyboard.Key.Backspace )
                )
            {
                string currStr = textbox.GetString();
                if (e.Code>=Keyboard.Key.A &&  e.Code<=Keyboard.Key.Z)
                    textbox.SetString(currStr+(char)(65+e.Code));
                else if(e.Code>=Keyboard.Key.Num0 &&  e.Code<=Keyboard.Key.Num9)
                    textbox.SetString(currStr+(char)(e.Code+22));
                else if(e.Code==Keyboard.Key.Backspace && currStr.Length>0)
                    textbox.SetString(currStr.Remove(currStr.Length-1,1));
            }
            else if(IsAlive && isUsed && e.Code==Keyboard.Key.Enter && source is Application app)
            {
                isUsed = false;
                ConstructList(textbox.GetString(), app, elementsOfGUI);
                foreach (EventDrawable eventDrawable in app.eventDrawables)
                    eventDrawable.IsAlive = true;
            }
        }
        public override void MouseButtonPressed(object? source, ICollection<EventDrawableGUI> elementsOfGUI, MouseButtonEventArgs e)
        {
            if (IsAlive && textbox.Contains(e.X, e.Y) && source is Application app)
            {
                isUsed = true;
                foreach (EventDrawable eventDrawable in app.eventDrawables)
                {
                    if(!(eventDrawable is GUI))
                        eventDrawable.IsAlive = false;
                }
                   
                textbox.SetString("");
                foreach (ButtonDeikstraList button in buttons)
                    button.IsNeedToRemove=true;
                buttons.Clear();
                IsAlive = true;
            }
        }
        protected void ConstructList(string name,Application app,ICollection<EventDrawableGUI> elements)
        {
            if(app.graph.ContainsName(name))
            {
                try
                {
                    IEnumerable<int> weights;
                    IEnumerable<IEnumerable<string>> ways = app.graph.Deikstra(name,out weights);
                    Textbox textboxSh = new();
                    textboxSh.SetColorText(Color.Black);
                    textboxSh.SetFillColorRect(Color.Cyan);
                    textboxSh.SetSizeRect(new(100, 30));
                    textboxSh.SetSizeCharacterText(12);
                    textboxSh.SetPos(new(50, 80));
                    IEnumerator<int> enumerator = weights.GetEnumerator();
                    enumerator.MoveNext();
                    foreach (IEnumerable<string> way in ways)
                    {
                        ButtonDeikstraList button = new(textboxSh, way, enumerator.Current,app.eventDrawables.Count);
                        elements.Add(button);
                        buttons.Add(button);
                        textboxSh.SetPos(textboxSh.GetPosition().X,textboxSh.GetPosition().Y+textboxSh.GetSizeRect().Y);
                        enumerator.MoveNext();
                    }
                }
                catch(Exception e)
                {
                    app.messageToUser.SetString(e.Message);
                }
            }
        }
    }
}
