using Graphs;
using Textboxes;
using SfmlAppLib;

namespace RealizationOfApp
{
    public class Application
    {
        public RenderWindow window;
        public Graph graph = new();
        public List<EventDrawable> eventDrawables=new();
        public List<IEventHandler> eventHandlers = new();
        public uint CurrentWidth = 1280, CurrentHeight = 720;
        public Application()
        {
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
                window.Clear(Color.White);
                DeleteObjects();
                foreach (EventDrawable eventDrawable in eventDrawables)
                    window.Draw(eventDrawable);
                window.Display();
            }
        }
        public void DeleteObjects()
        {
            for (int i = 0; i<eventDrawables.Count; ++i)
            {
                if (eventDrawables[i].IsNeedToRemove)
                {
                    if (eventDrawables[i] is EdgeEv e)
                    {
                        if (graph.ContainsName(e.startVer.GetString()) && graph.ContainsName(e.endVer.GetString()))
                        {
                            graph[e.startVer.GetString(), e.endVer.GetString()] = 0;
                        }
                    }
                    else if (eventDrawables[i] is VertexGraph v)
                    {
                        graph.DeleteVertex(v.GetString());
                    }
                    eventDrawables.RemoveAt(i);
                    --i;
                }
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
