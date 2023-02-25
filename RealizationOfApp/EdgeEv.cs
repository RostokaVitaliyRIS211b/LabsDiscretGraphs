
namespace RealizationOfApp
{
    public class EdgeEv:EventDrawable
    {
        protected Edge edge;
        public bool IsNew = false;
        public EdgeEv(Edge edge)
        {
            this.edge = new(edge);
        }
        public override void MouseMoved(object source, MouseMoveEventArgs e)
        {
            if (IsAlive && IsNew && source is Application app)
            {
                edge.SetVertex2(e.X, e.Y);
            }
        }
        public override void MouseButtonPressed(object source, MouseButtonEventArgs e)
        {
            if(IsAlive && IsNew && e.Button == Mouse.Button.Left && source is Application app)
            {
                foreach(EventDrawable eventDrawable in app.eventDrawables)
                {
                    if(eventDrawable is VertexGraph vertex)
                    {
                        if(vertex.Contains(e.X,e.Y))
                        {
                            IsNew = false;
                            edge.SetVertex2(vertex.GetPos());

                            foreach (EventDrawable eventDrawable1 in app.eventDrawables)
                                eventDrawable1.IsAlive = true;
                           
                            break;
                        }
                    }
                }
            }
        }
        public bool Contains(Vector2f vector)
        {
            return edge.Contains(vector);
        }
        public override void Draw(RenderTarget target, RenderStates states)
        {
            edge.Draw(target, states);
        }
    }
}
