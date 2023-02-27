
using RealizationOfApp.GUI_Classes;

namespace RealizationOfApp
{
    public class GUIFactoryA:AbstractGUIFactory
    {
        public override ICollection<EventDrawableGUI> CreateGUI()
        {
            List<EventDrawableGUI> drawableGUIs = new();
            Textbox textbox = new();
            textbox.SetSizeRect(200, 65);
            textbox.SetFillColorRect(new Color(240,152,4));
            textbox.SetColorText(Color.Black);
            textbox.SetSizeCharacterText(16);
            textbox.SetString("Add");
            textbox.SetPos(100, 32.5f);
            drawableGUIs.Add(new ButtonAdd(textbox));
            textbox.SetPos(textbox.GetPosition().X+textbox.GetSizeRect().X, textbox.GetSizeRect().Y/2);
            textbox.SetString("Sort");
            drawableGUIs.Add(new ButtonSort(textbox));
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
