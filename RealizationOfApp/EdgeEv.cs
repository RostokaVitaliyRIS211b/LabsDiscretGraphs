
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
        public override void MouseMoved(object? source, MouseMoveEventArgs e)
        {
            if (IsAlive && IsNew && source is Application app)
            {
                edge.SetVertex2(e.X, e.Y);
            }
        }
        public override void MouseButtonPressed(object? source, MouseButtonEventArgs e)
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
                            vertex.incindentEdges.Add(this);
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
        public bool Contains(float x,float y)
        {
            return edge.Contains(x, y);
        }
        public Vector2f GetPosVer1()
        {
            return edge.GetPosVer1();
        }
        public Vector2f GetPosVer2()
        {
            return edge.GetPosVer2();
        }
        public void SetPosVer1(float x,float y)
        {
            edge.SetVertex1(new(x, y));
        }
        public void SetPosVer2(float x, float y)
        {
            edge.SetVertex2(new(x, y));
        }
        public override void Draw(RenderTarget target, RenderStates states)
        {
            edge.Draw(target, states);
        }
    }
}
