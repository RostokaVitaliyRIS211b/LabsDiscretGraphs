
namespace SfmlAppLib
{
    public abstract class EventDrawable:IEventHandler,Drawable
    {
        public bool IsAlive { get; set; }
        public virtual void MouseMoved(object? source,MouseMoveEventArgs e){}
        public virtual void MouseButtonPressed(object? source, MouseButtonEventArgs e) {}
        public virtual void MouseButtonReleased(object? source, MouseButtonEventArgs e) {}
        public virtual void MouseWheelScrolled(object? source, MouseWheelScrollEventArgs e) {}
        public virtual void KeyPressed(object? source, KeyEventArgs e) {}
        public virtual void KeyReleased(object? source, KeyEventArgs e) {}
        public abstract void Draw(RenderTarget target, RenderStates states);
    }
}
