
namespace SfmlAppLib
{
    public sealed class GUI:EventDrawable
    {
        public View viewOfGUI;
        public ICollection<EventDrawableGUI> elementsOfGUI;
        public GUI(AbstractGUIFactory factory)
        {
            elementsOfGUI = factory.CreateGUI();
            viewOfGUI = factory.GetView();
            IsAlive = factory.GetState();
        }
        public override void MouseMoved(object? source, MouseMoveEventArgs e) 
        {
            if(IsAlive)
            {
                foreach (EventDrawableGUI x in elementsOfGUI)
                    x.MouseMoved(source, elementsOfGUI, e);
            } 
        }
        public override void MouseButtonPressed(object? source, MouseButtonEventArgs e) 
        {
            if (IsAlive)
            {
                foreach (EventDrawableGUI x in elementsOfGUI)
                    x.MouseButtonPressed(source, elementsOfGUI, e);
            }
        }
        public override void MouseButtonReleased(object? source, MouseButtonEventArgs e) 
        {
            if (IsAlive)
            {
                foreach (EventDrawableGUI x in elementsOfGUI)
                    x.MouseButtonReleased(source, elementsOfGUI, e);
            }
        }
        public override void MouseWheelScrolled(object? source, MouseWheelScrollEventArgs e) 
        {
            if (IsAlive)
            {
                foreach (EventDrawableGUI x in elementsOfGUI)
                    x.MouseWheelScrolled(source, elementsOfGUI, e);
            }
        }
        public override void KeyPressed(object? source, KeyEventArgs e) 
        {
            if (IsAlive)
            {
                foreach (EventDrawableGUI x in elementsOfGUI)
                    x.KeyPressed(source, elementsOfGUI, e);
            }
        }
        public override void KeyReleased(object? source, KeyEventArgs e) 
        {
            if (IsAlive)
            {
                foreach (EventDrawableGUI x in elementsOfGUI)
                    x.KeyReleased(source, elementsOfGUI, e);
            }
        }
        public override void Draw(RenderTarget target, RenderStates states) 
        {
            foreach (EventDrawableGUI eventDrawableGUI in elementsOfGUI)
                eventDrawableGUI.Draw(target, states);
        }
    }
}
