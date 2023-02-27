
namespace RealizationOfApp
{
    public abstract class EvTextbox:EventDrawableGUI
    {
        public Textbox textbox = new();
        public EvTextbox(Textbox textbox)
        {
            this.textbox = new(textbox);
        }
        public override void Draw(RenderTarget target, RenderStates states)
        {
            textbox.Draw(target, states);
        }
    }
}
