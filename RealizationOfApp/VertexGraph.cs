    
namespace RealizationOfApp
{
    public class VertexGraph:EventDrawable
    {
        public static int Counter { get; protected set; } = 0;
        protected CircleTextbox circle = new();
        public bool Catched = false;
        public Color BuffColor;
        public List<EdgeEv> incindentEdges = new();
        public VertexGraph(CircleTextbox circle)
        {
            this.circle = new(circle);
            if (circle.GetString()=="")
                circle.SetString(Counter.ToString());
            ++Counter;

            BuffColor = circle.GetFillColorCircle();
        }
        public override void MouseMoved(object? source, MouseMoveEventArgs e)
        {
            if(IsAlive && !Catched && circle.Contains(e.X,e.Y))
            {
                circle.SetFillColorCircle(Color.Magenta);
            }
            else if(IsAlive && !Catched)
            {
                circle.SetFillColorCircle(BuffColor);
            }
            else if(IsAlive && Catched)
            {
                foreach(EdgeEv edge in incindentEdges)
                {
                    if(circle.Contains(edge.GetPosVer1()))
                    {
                        edge.SetPosVer1(e.X, e.Y);
                    }
                    else
                    {
                        edge.SetPosVer2(e.X, e.Y);
                    }
                }
                circle.SetPosition(e.X, e.Y);
            }
        }
        public override void MouseButtonReleased(object? source, MouseButtonEventArgs e)
        {
            if (IsAlive && Catched  && source is Application app)
            {
                Catched = false;
                foreach (EventDrawable drawables in app.eventDrawables)
                    drawables.IsAlive = true;
            }
        }
        public override void MouseButtonPressed(object? source, MouseButtonEventArgs e)
        {
            if(IsAlive && circle.Contains(e.X,e.Y) && e.Button==Mouse.Button.Left && source is Application app)
            {
                Catched = true;
                foreach (EventDrawable drawables in app.eventDrawables)
                    drawables.IsAlive = false;
                IsAlive = true;
                circle.SetPosition(e.X, e.Y);
            }
            else if(IsAlive && circle.Contains(e.X, e.Y) && !Catched && e.Button==Mouse.Button.Right && source is Application app2)
            {
                foreach (EventDrawable drawables in app2.eventDrawables)
                    drawables.IsAlive = false;
                EdgeEv edge = new(new Edge(new(circle.GetPosition(),Color.Black),new(new(e.X,e.Y), Color.Black),"10"));
                edge.IsNew = true;
                incindentEdges.Add(edge);
                Arrow arrow = new(ref edge.edge);
                app2.eventDrawables.Insert(app2.eventDrawables.Count-1, arrow);
                app2.eventDrawables.Insert(0,edge);
            }
        }
        public bool Contains(Vector2f vector)
        {
            return circle.Contains(vector);
        }
        public bool Contains(float x,float y)
        {
            return circle.Contains(x, y);
        }
        public void SetPos(Vector2f vector)
        {
            circle.SetPosition(vector);
        }
        public void SetPos(float x,float y)
        {
            circle.SetPosition(x,y);
        }
        public void SetColor(Color color)
        {
            BuffColor = color;
            circle.SetFillColorCircle(color);
        }
        public Vector2f GetPos()
        {
            return circle.GetPosition();
        }
        public override void Draw(RenderTarget target, RenderStates states)
        {
            circle.Draw(target, states);
        }
    }
}