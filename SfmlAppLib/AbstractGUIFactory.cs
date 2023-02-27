
namespace SfmlAppLib
{
    public abstract class AbstractGUIFactory
    {
        public abstract IList<EventDrawableGUI> CreateGUI();
        public abstract View GetView();
        public abstract bool GetState();
    }
}
