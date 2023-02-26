
using SFML.Graphics;

namespace RealizationOfApp
{
    public class EdgeEv:EventDrawable
    {
        public Edge edge;
        public bool IsNew = false;
        public VertexGraph startVer;
        public VertexGraph? endVer;
        public Arrow arrow;
        public Color BuffColor;
        public EdgeEv(Edge edge,VertexGraph start,ref Arrow arrow)
        {
            this.edge = new(edge);
            BuffColor = edge.GetColor();
            this.arrow = arrow;
            arrow.edge=this;
            startVer = start;
        }
        public override void MouseMoved(object? source, MouseMoveEventArgs e)
        {
            if (IsAlive && IsNew && source is Application app)
            {
                edge.SetVertex2(e.X, e.Y);
            }
            else if(IsAlive && !IsNew && edge.Contains(e.X, e.Y))
            {
                edge.SetColor(Color.Magenta);
            }
            else
            {
                edge.SetColor(BuffColor);
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
                            endVer = vertex;
                            edge.SetVertex2(vertex.GetPos());
                            vertex.incindentEdges.Add(this);
                            foreach (EventDrawable eventDrawable1 in app.eventDrawables)
                                eventDrawable1.IsAlive = true;
                            app.graph[startVer.GetString(), vertex.GetString()] = Int32.Parse(edge.GetWeight());
                            break;
                        }
                    }
                }
            }
            else if(IsAlive && !IsNew && e.Button == Mouse.Button.Right && edge.Contains(e.X,e.Y) && source is Application app2)
            {
                Console.WriteLine("GA");
            }
            else if (IsAlive && !IsNew && e.Button == Mouse.Button.Middle && edge.Contains(e.X, e.Y))
            {
                IsNeedToRemove = true;
                arrow.IsNeedToRemove = true;
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
        public void SetWeight(int weight)
        {
            edge.SetWeight1(weight.ToString());
        }
        public override void Draw(RenderTarget target, RenderStates states)
        {
            edge.Draw(target, states);
        }
    }
}
