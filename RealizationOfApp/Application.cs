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
        public Application()
        {
            window = new RenderWindow(new VideoMode(1280, 720), "Lines");
            eventDrawables.Add(new GUI(new GUIFactoryA()));
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
                
                window.Display();
            }
        }
        public void MouseMoved(object? source, MouseMoveEventArgs e)
        {
            foreach (EventDrawable eventDrawable in eventDrawables)
                eventDrawable.MouseMoved(this, e);
        }
        public void MouseButtonPressed(object? source, MouseButtonEventArgs e)
        {
            foreach (EventDrawable eventDrawable in eventDrawables)
                eventDrawable.MouseButtonPressed(this, e);
        }
        public void MouseButtonReleased(object? source, MouseButtonEventArgs e)
        {
            foreach (EventDrawable eventDrawable in eventDrawables)
                eventDrawable.MouseButtonReleased(this, e);
        }
        public void KeyPressed(object? source, KeyEventArgs e)
        {
            foreach (EventDrawable eventDrawable in eventDrawables)
                eventDrawable.KeyPressed(this, e);
        }
        public void MouseWheelScrolled(object? source, MouseWheelScrollEventArgs e)
        {
            foreach (EventDrawable eventDrawable in eventDrawables)
                eventDrawable.MouseWheelScrolled(this, e);
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
