
namespace SfmlAppLib
{
    public abstract class EventDrawableGUI:Drawable
    {
        public bool IsAlive { get; set; } = true;
        public bool IsNeedToRemove { get; set; } = false;
        public virtual void MouseMoved(object? source, ICollection<EventDrawableGUI> elementsOfGUI, MouseMoveEventArgs e) { }
        public virtual void MouseButtonPressed(object? source, ICollection<EventDrawableGUI> elementsOfGUI, MouseButtonEventArgs e) { }
        public virtual void MouseButtonReleased(object? source, ICollection<EventDrawableGUI> elementsOfGUI, MouseButtonEventArgs e) { }
        public virtual void MouseWheelScrolled(object? source, ICollection<EventDrawableGUI> elementsOfGUI, MouseWheelScrollEventArgs e) { }
        public virtual void KeyPressed(object? source, ICollection<EventDrawableGUI> elementsOfGUI, KeyEventArgs e) { }
        public virtual void KeyReleased(object? source, ICollection<EventDrawableGUI> elementsOfGUI, KeyEventArgs e) { }
        public abstract void Draw(RenderTarget target, RenderStates states);
    }
}
