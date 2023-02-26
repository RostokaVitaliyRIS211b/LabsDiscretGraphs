
namespace SfmlAppLib
{
    public abstract class EventDrawableGUI:Drawable
    {
        public bool IsAlive { get; set; } = true;
        public virtual void MouseMoved(object? source, IEnumerable<EventDrawableGUI> elementsOfGUI, MouseMoveEventArgs e) { }
        public virtual void MouseButtonPressed(object? source, IEnumerable<EventDrawableGUI> elementsOfGUI, MouseButtonEventArgs e) { }
        public virtual void MouseButtonReleased(object? source, IEnumerable<EventDrawableGUI> elementsOfGUI, MouseButtonEventArgs e) { }
        public virtual void MouseWheelScrolled(object? source, IEnumerable<EventDrawableGUI> elementsOfGUI, MouseWheelScrollEventArgs e) { }
        public virtual void KeyPressed(object? source, IEnumerable<EventDrawableGUI> elementsOfGUI, KeyEventArgs e) { }
        public virtual void KeyReleased(object? source, IEnumerable<EventDrawableGUI> elementsOfGUI, KeyEventArgs e) { }
        public abstract void Draw(RenderTarget target, RenderStates states);
    }
}
