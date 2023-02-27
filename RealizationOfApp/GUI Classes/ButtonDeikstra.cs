

namespace RealizationOfApp.GUI_Classes
{
    public class ButtonDeikstra:EvTextbox
    {
        public Color BuffColor;
        public string EnteredText;
        public ButtonDeikstra(Textbox textbox) : base(textbox)
        {
            BuffColor = textbox.GetFillRectColor();
            EnteredText="";
        }
        public override void MouseMoved(object? source, IEnumerable<EventDrawableGUI> elementsOfGUI, MouseMoveEventArgs e)
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
        public override void MouseButtonPressed(object? source, IEnumerable<EventDrawableGUI> elementsOfGUI, MouseButtonEventArgs e)
        {
            if (IsAlive && textbox.Contains(e.X, e.Y) && source is Application app)
            {
                try
                {
                    IEnumerable<int> ways;
                    IEnumerable<IEnumerable<string>> waysStr = app.graph.Deikstra(EnteredText,out ways);
                }
                catch(Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
               
            }
        }
    }
}
