namespace SfmlAppLib
{

    public interface IEventHandler
    {
        public void MouseMoved(object? source, MouseMoveEventArgs e);
        public void MouseButtonPressed(object? source, MouseButtonEventArgs e);
        public void MouseButtonReleased(object? source, MouseButtonEventArgs e);
        public void MouseWheelScrolled(object? source, MouseWheelScrollEventArgs e);
        public void KeyPressed(object? source, KeyEventArgs e);
        public void KeyReleased(object? source, KeyEventArgs e);
    }

}