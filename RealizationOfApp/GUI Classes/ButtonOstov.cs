using RealizationOfApp.ElementsOfGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealizationOfApp.GUI_Classes
{
    
    public class ButtonOstov:EvTextbox
    {
        public Color BuffColor;
        public ButtonOstov(Textbox textbox) : base(textbox)
        {
            BuffColor = textbox.GetFillRectColor();
        }
        public override void MouseButtonPressed(object? source, ICollection<EventDrawableGUI> elementsOfGUI, MouseButtonEventArgs e)
        {
            if (IsAlive && textbox.Contains(e.X, e.Y) && source is Application app)
            {
                textbox.SetFillColorRect(Color.Magenta);
                if (app.graph.isNotOriented())
                {
                    try
                    {
                        
                    }
                    catch (Exception e1)
                    {
                        app.messageToUser.SetString(e1.Message);
                    }
                }
                else
                {
                    app.messageToUser.SetString("Graph must be not oriented");
                }
            }

        }
        public override void MouseMoved(object? source, ICollection<EventDrawableGUI> elementsOfGUI, MouseMoveEventArgs e)
        {
            textbox.SetFillColorRect(BuffColor);
        }
    }
}
