
namespace SfmlAppLib
{
    public abstract class AbstractGUIFactory
    {
        public abstract ICollection<EventDrawableGUI> CreateGUI();
        public abstract View GetView();
        public abstract bool GetState();
    }
}
