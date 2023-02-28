

using RealizationOfApp.ElementsOfGraph;
using RealizationOfApp.GUI_Classes;

namespace RealizationOfApp//application
{
    public class Application
    {
        public RenderWindow window;
        public Clock clock = new();
        public Graph graph = new();
        public int LastCount = 0;
        public bool IsRestart = false;
        public bool IsOriented = true;
        public Textbox messageToUser = new();
        public List<List<EventDrawable>> eventDrawablesStates = new();
        public List<EventDrawable> eventDrawables=new();
        public List<IEventHandler> eventHandlers = new();
        public uint CurrentWidth = 1280, CurrentHeight = 720;
        public Application()
        {
            messageToUser.SetColorText(Color.Black);
            messageToUser.SetSizeCharacterText(16);
            messageToUser.SetPos(CurrentWidth/2, CurrentHeight-30);
            messageToUser.SetString("");
            window = new RenderWindow(new VideoMode(CurrentWidth, CurrentHeight), "Lines");
            eventDrawables.Add(new GUI(new GUIFactoryA()));
            window.SetFramerateLimit(60);
            window.MouseMoved+=MouseMoved;
            window.KeyPressed+=KeyPressed;
            window.MouseButtonReleased+=MouseButtonReleased;
            window.MouseButtonPressed+=MouseButtonPressed;
            window.MouseWheelScrolled+=MouseWheelScrolled;
            window.Closed+=Closed;
        }

        public void Start()
        {
            while (window.IsOpen)
            {
                window.DispatchEvents();
                window.Clear(new(236,253,230));
                DeleteObjects();
                foreach (EventDrawable eventDrawable in eventDrawables)
                    window.Draw(eventDrawable);
                DisplayMessage();
                window.Display();
                LastCount = eventDrawables.Count;
            }
        }
        public void DeleteObjects()
        {
            bool isBeenDeletes = false;
            for (int i = 0; i<eventDrawables.Count; ++i)
            {
                if (eventDrawables[i].IsNeedToRemove)
                {
                    if (eventDrawables[i] is EdgeEv e)
                    {
                        if (graph.ContainsName(e.startVer.GetString()) && graph.ContainsName(e.endVer.GetString()))
                        {
                            graph[e.startVer.GetString(), e.endVer.GetString()] = 0;
                            isBeenDeletes = true;
                        }
                    }
                    else if (eventDrawables[i] is VertexGraph v)
                    {
                        graph.DeleteVertex(v.GetString());
                        isBeenDeletes = true;
                    }
                    eventDrawables.RemoveAt(i);
                    --i;
                }
            }
            if (isBeenDeletes)
            {
                ColoringComponentsOfConnection();
            }
                
        }
        public void ColoringComponentsOfConnection()
        {
            Color first = Color.Red, second = Color.Green;
            int i = 0;
            List<VertexGraph> vertexes = new(from elem in eventDrawables
                                             where (elem is VertexGraph)
                                             let ver = elem as VertexGraph
                                             select ver);
            if(vertexes.Count!=0)
            {
                IEnumerable<IEnumerable<string>> components = graph.GetComponentsOfConnection();

                foreach (IEnumerable<string> names in components)
                {
                    Color currentColor = Color.Transparent;
                    if (i<2)
                    {
                        currentColor = i==0 ? first : second;
                    }
                    else
                    {
                        currentColor = ColorInterpolator.InterpolateBetween(first, second, Randic.random.NextDouble());
                    }
                    foreach (string name in names)
                    {
                        vertexes.Find(x => x.GetString()==name)?.SetColor(currentColor);
                    }
                    ++i;
                }
           
            }
        }
        public void DisplayMessage()
        {
            if(!IsRestart && messageToUser.GetString()!="")
            {
                clock.Restart();
                IsRestart = true;
            }
            if(messageToUser.GetString()!="" && clock.ElapsedTime.AsMilliseconds()>8000)
            {
                IsRestart = false;
                messageToUser.SetString("");
            }
            if(messageToUser.GetString()!="")
            {
                window.Draw(messageToUser);
            }

        }
        public void MouseMoved(object? source, MouseMoveEventArgs e)
        {
           for(int i=0;i<eventDrawables.Count;++i)
           {
                eventDrawables[i].MouseMoved(this, e);
           }  
        }
        public void MouseButtonPressed(object? source, MouseButtonEventArgs e)
        {
            for (int i = 0; i<eventDrawables.Count; ++i)
            {
                eventDrawables[i].MouseButtonPressed(this, e);
            }
        }
        public void MouseButtonReleased(object? source, MouseButtonEventArgs e)
        {
            for (int i = 0; i<eventDrawables.Count; ++i)
            {
                eventDrawables[i].MouseButtonReleased(this, e);
            }
        }
        public void KeyPressed(object? source, KeyEventArgs e)
        {
            for (int i = 0; i<eventDrawables.Count; ++i)
            {
                eventDrawables[i].KeyPressed(this, e);
            }
        }
        public void MouseWheelScrolled(object? source, MouseWheelScrollEventArgs e)
        {
            for (int i = 0; i<eventDrawables.Count; ++i)
            {
                eventDrawables[i].MouseWheelScrolled(this, e);
            }
            Console.WriteLine(graph);
        }
        public void Closed(object? source, EventArgs e)
        {
            if (source is Window c)
            {
                c.Close();
            }
        }
    }
}
