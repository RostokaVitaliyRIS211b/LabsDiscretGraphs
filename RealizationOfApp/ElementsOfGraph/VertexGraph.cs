namespace RealizationOfApp.ElementsOfGraph
{
    public class VertexGraph : EventDrawable
    {
        public static int Counter { get; protected set; } = 0;
        protected CircleTextbox circle = new();
        public bool Catched = false;
        public Color BuffColor;
        public List<EdgeEv> incindentEdges = new();
        public VertexGraph(CircleTextbox circle)
        {
            this.circle = new(circle);
            if (circle.GetString() == "")
                circle.SetString(Counter.ToString());
            ++Counter;
            BuffColor = circle.GetFillColorCircle();
        }
        public override void MouseMoved(object? source, MouseMoveEventArgs e)
        {
            if (IsAlive && !Catched && circle.Contains(e.X, e.Y))
            {
                circle.SetFillColorCircle(Color.Magenta);
            }
            else if (IsAlive && !Catched)
            {
                circle.SetFillColorCircle(BuffColor);
            }
            else if (IsAlive && Catched)
            {
                SetPos(e.X, e.Y);
            }
        }
        public override void MouseButtonReleased(object? source, MouseButtonEventArgs e)
        {
            if (IsAlive && Catched && source is Application app)
            {
                Catched = false;
                foreach (EventDrawable drawables in app.eventDrawables)
                    drawables.IsAlive = true;
            }
        }
        public override void MouseButtonPressed(object? source, MouseButtonEventArgs e)
        {
            if (IsAlive && circle.Contains(e.X, e.Y) && e.Button == Mouse.Button.Left && source is Application app)
            {
                Catched = true;
                foreach (EventDrawable drawables in app.eventDrawables)
                    drawables.IsAlive = false;
                IsAlive = true;
                circle.SetPosition(e.X, e.Y);
            }
            else if (IsAlive && circle.Contains(e.X, e.Y) && !Catched && e.Button == Mouse.Button.Right && source is Application app2)
            {
                foreach (EventDrawable drawables in app2.eventDrawables)
                    drawables.IsAlive = false;
                Arrow arrow = new();
                EdgeEv edge = new(new Edge(new(circle.GetPosition(), Color.Black), new(new(e.X, e.Y), Color.Black), "1"), this, ref arrow);
                edge.IsNew = true;
                incindentEdges.Add(edge);
                app2.eventDrawables.Insert(app2.eventDrawables.Count - 1, arrow);
                app2.eventDrawables.Insert(0, edge);
            }
            else if (IsAlive && circle.Contains(e.X, e.Y) && !Catched && e.Button == Mouse.Button.Middle && source is Application app3)
            {
                IsNeedToRemove = true;
                foreach (EdgeEv edgeEv in incindentEdges)
                {
                    edgeEv.IsNeedToRemove = true;
                    edgeEv.arrow.IsNeedToRemove = true;
                }
                incindentEdges.Clear();
            }
        }
        public override void KeyPressed(object? source, KeyEventArgs e)
        {
            if (IsAlive && source is Application application)
            {
                if (e.Code == Keyboard.Key.N && circle.Contains(Mouse.GetPosition(application.window)))
                {
                    foreach (EventDrawable drawables in application.eventDrawables)
                        drawables.IsAlive = false;
                    application.eventDrawables.Add(new EnterTextVertex(circle.GetText().Position, this));
                    application.messageToUser.SetString("Type the name and press enter");
                }
            }
        }
        public bool Contains(Vector2f vector)
        {
            return circle.Contains(vector);
        }
        public bool Contains(float x, float y)
        {
            return circle.Contains(x, y);
        }
        public void SetPos(Vector2f vector)
        {
            SetPos(vector.X, vector.Y);
        }
        public void SetPos(float x, float y)
        {
            for (int i = 0; i < incindentEdges.Count; ++i)
            {
                if (incindentEdges[i].IsNeedToRemove)
                {
                    incindentEdges.RemoveAt(i);
                    --i;
                }
                else if (incindentEdges[i].startVer==this)
                {
                    incindentEdges[i].SetPosVer1(x, y);
                }
                else
                {
                    incindentEdges[i].SetPosVer2(x, y);
                }
            }
            circle.SetPosition(x, y);
        }
        public void SetColor(Color color)
        {
            BuffColor = color;
            circle.SetFillColorCircle(color);
        }
        public void SetTempCol(Color color)
        {
            circle.SetFillColorCircle(color);
        }
        public void SetName(string name)
        {
            circle.SetString(name);
        }
        public string GetString()
        {
            return circle.GetString();
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