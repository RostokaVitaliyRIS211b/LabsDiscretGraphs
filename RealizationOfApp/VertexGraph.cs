using SfmlAppLib;
using Textboxes;

namespace RealizationOfApp
{
    public class VertexGraph:EventDrawable
    {
        protected CircleTextbox circle = new();
        public override void Draw(RenderTarget target, RenderStates states)
        {
            circle.Draw(target, states);
        }
    }
}