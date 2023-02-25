
namespace RealizationOfApp
{
    public class GUIFactoryA:AbstractGUIFactory
    {
        public override ICollection<EventDrawableGUI> CreateGUI()
        {
            List<EventDrawableGUI> drawableGUIs = new();

            return drawableGUIs;
        }
        public override View GetView()
        {
            return new View();
        }
        public override bool GetState()
        {
            return true;
        }
    }
}
