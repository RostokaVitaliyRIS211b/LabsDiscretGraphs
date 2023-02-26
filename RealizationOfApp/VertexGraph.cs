    
namespace RealizationOfApp
{
    public class VertexGraph:EventDrawable
    {
        public static int Counter { get; protected set; } = 0;
        protected CircleTextbox circle = new();
        public bool Catched = false;
        public Color BuffColor;
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
                EdgeEv edge = new(new Edge(new(circle.GetPosition()),new(new(e.X,e.Y)),"10"));
                app2.eventDrawables.Add(edge);
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