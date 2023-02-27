
namespace SfmlAppLib
{
    public sealed class GUI:EventDrawable
    {
        public View viewOfGUI;
        public IList<EventDrawableGUI> elementsOfGUI;
        public GUI(AbstractGUIFactory factory)
        {
            elementsOfGUI = factory.CreateGUI();
            viewOfGUI = factory.GetView();
            IsAlive = factory.GetState();
        }
        GUI(IList<EventDrawableGUI> drawableGUIs)
        {
            viewOfGUI = new();
            elementsOfGUI = drawableGUIs;
        }
        public override void MouseMoved(object? source, MouseMoveEventArgs e) 
        {
            if(IsAlive)
            {
                for(int i=0;i<elementsOfGUI.Count;++i)
                {
                    elementsOfGUI[i].MouseMoved(source, elementsOfGUI, e);
                }
            }
            DeleteObjects();
        }
        public override void MouseButtonPressed(object? source, MouseButtonEventArgs e) 
        {
            if (IsAlive)
            {
                for (int i = 0; i<elementsOfGUI.Count; ++i)
                {
                    elementsOfGUI[i].MouseButtonPressed(source, elementsOfGUI, e);
                }
            }
            DeleteObjects();
        }
        public override void MouseButtonReleased(object? source, MouseButtonEventArgs e) 
        {
            if (IsAlive)
            {
                for (int i = 0; i<elementsOfGUI.Count; ++i)
                {
                    elementsOfGUI[i].MouseButtonReleased(source, elementsOfGUI, e);
                }
            }
            DeleteObjects();
        }
        public override void MouseWheelScrolled(object? source, MouseWheelScrollEventArgs e) 
        {
            if (IsAlive)
            {
                for (int i = 0; i<elementsOfGUI.Count; ++i)
                {
                    elementsOfGUI[i].MouseWheelScrolled(source, elementsOfGUI, e);
                }
            }
            DeleteObjects();
        }
        public override void KeyPressed(object? source, KeyEventArgs e) 
        {
            if (IsAlive)
            {
                for (int i = 0; i<elementsOfGUI.Count; ++i)
                {
                    elementsOfGUI[i].KeyPressed(source, elementsOfGUI, e);
                }
            }
            DeleteObjects();
        }
        public override void KeyReleased(object? source, KeyEventArgs e) 
        {
            if (IsAlive)
            {
                for (int i = 0; i<elementsOfGUI.Count; ++i)
                {
                    elementsOfGUI[i].KeyReleased(source, elementsOfGUI, e);
                }
            }
            DeleteObjects();
        }
        public void DeleteObjects()
        {
            List<EventDrawableGUI> isNeedRemove = new(from elem in elementsOfGUI
                                                      where (elem.IsNeedToRemove)
                                                      select elem);
            foreach (EventDrawableGUI eventDrawable in isNeedRemove)
                elementsOfGUI.Remove(eventDrawable);
        }
        public override void Draw(RenderTarget target, RenderStates states) 
        {
            foreach (EventDrawableGUI eventDrawableGUI in elementsOfGUI)
                eventDrawableGUI.Draw(target, states);
        }
    }
}
