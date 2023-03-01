using RealizationOfApp.ElementsOfGraph;

namespace RealizationOfApp
{
    public class ButtonAdd:EvTextbox
    {
        public Color BuffColor;
        public ButtonAdd(Textbox textbox):base(textbox)
        {
            BuffColor = textbox.GetFillRectColor();
        }
        public override void MouseMoved(object? source, ICollection<EventDrawableGUI> elementsOfGUI, MouseMoveEventArgs e)
        {
            if(IsAlive && textbox.Contains(e.X,e.Y))
            {
                textbox.SetFillColorRect(new(89, 168, 167));
            }
            else
            {
                textbox.SetFillColorRect(BuffColor);
            }
        }
        public override void MouseButtonPressed(object? source, ICollection<EventDrawableGUI> elementsOfGUI, MouseButtonEventArgs e)
        {
            if(IsAlive && source is Application application && textbox.Contains(e.X,e.Y))
            {
                CircleTextbox circleTextbox = new(Shablones.circleShablone);
                circleTextbox.SetString(VertexGraph.Counter.ToString());
                circleTextbox.SetPosition(application.CurrentWidth/2, application.CurrentHeight/2);
                VertexGraph vertex = new(circleTextbox);
                application.graph.AddVertex(circleTextbox.GetString());
                application.eventDrawables.Add(vertex);
                textbox.SetFillColorRect(BuffColor);
                application.ColoringComponentsOfConnection();
            }
        }
        public override void MouseButtonReleased(object? source, ICollection<EventDrawableGUI> elementsOfGUI, MouseButtonEventArgs e)
        {

        }
    }
}
