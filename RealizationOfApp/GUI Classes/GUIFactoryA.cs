
namespace RealizationOfApp
{
    public class GUIFactoryA:AbstractGUIFactory
    {
        public override ICollection<EventDrawableGUI> CreateGUI()
        {
            List<EventDrawableGUI> drawableGUIs = new();
            Textbox textbox = new();
            textbox.SetSizeRect(250, 65);
            textbox.SetFillColorRect(new Color(240,152,4));
            textbox.SetColorText(Color.Black);
            textbox.SetSizeCharacterText(16);
            textbox.SetString("Add");
            textbox.SetPos(125, 32.5f);
            drawableGUIs.Add(new ButtonAdd(textbox));
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
