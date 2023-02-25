
namespace RealizationOfApp
{
    public class EvTextbox:EventDrawable
    {
        public Textbox textbox = new();
        public EvTextbox(Textbox textbox)
        {
            this.textbox = new(textbox);
        }
        public override void MouseMoved(object source, MouseMoveEventArgs e)
        {
            
        }
        public override void MouseButtonPressed(object source, MouseButtonEventArgs e)
        {
            
        }
        public override void MouseButtonReleased(object source, MouseButtonEventArgs e)
        {
            
        }
        public override void Draw(RenderTarget target, RenderStates states)
        {
            textbox.Draw(target, states);
        }
    }
}
