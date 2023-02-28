using RealizationOfApp.GUI_Classes;

namespace RealizationOfApp
{
    public class GUIFactoryA : AbstractGUIFactory
    {
        public override IList<EventDrawableGUI> CreateGUI()
        {
            List<EventDrawableGUI> drawableGUIs = new();
            Textbox textbox = new();
            textbox.SetSizeRect(100, 65);
            textbox.SetOutlineColorRect(Color.Black);
            textbox.SetOutlineThicknessRect(2);
            textbox.SetFillColorRect(new Color(133, 253, 251));
            textbox.SetColorText(Color.Black);
            textbox.SetSizeCharacterText(16);


            textbox.SetString("Add");
            textbox.SetPos(100, 32.5f);
            drawableGUIs.Add(new ButtonAdd(textbox));
            textbox.SetPos(textbox.GetPosition().X + textbox.GetSizeRect().X, textbox.GetSizeRect().Y / 2);
            textbox.SetString("Sort");
            drawableGUIs.Add(new ButtonSort(textbox));
            textbox.SetPos(textbox.GetPosition().X + textbox.GetSizeRect().X, textbox.GetSizeRect().Y / 2);
            textbox.SetString("Deikstra");
            drawableGUIs.Add(new ButtonDeikstra(textbox));
            textbox.SetPos(textbox.GetPosition().X + textbox.GetSizeRect().X, textbox.GetSizeRect().Y / 2);
            textbox.SetString("MaxClick");
            drawableGUIs.Add(new ButtonClikaGraph(textbox));
            textbox.SetPos(textbox.GetPosition().X + textbox.GetSizeRect().X, textbox.GetSizeRect().Y / 2);
            textbox.SetString("Ostov");
            drawableGUIs.Add(new ButtonOstov(textbox));
            textbox.SetPos(textbox.GetPosition().X + textbox.GetSizeRect().X, textbox.GetSizeRect().Y / 2);
            textbox.SetString("Oriented");
            drawableGUIs.Add(new ButtonOriented(textbox));

            textbox.SetPos(textbox.GetPosition().X + textbox.GetSizeRect().X, textbox.GetSizeRect().Y / 2);
            textbox.SetString("Test");
            var testButton = new EvButton(textbox);
            testButton.OnMouseMoved +=
                (object? obj, ICollection<EventDrawableGUI> huis, MouseMoveEventArgs e) =>
                {
                    if (testButton.IsAlive && testButton.textbox.Contains(e.X,e.Y))
                    {
                        testButton.textbox.SetFillColorRect(Color.Green);
                    }
                    else
                    {
                        testButton.textbox.SetFillColorRect(testButton.BuffColor);
                    }
                };
            drawableGUIs.Add(testButton);
            testButton.OnMouseButtonPressed += (object? source, ICollection<EventDrawableGUI> elementsOfGUI, MouseButtonEventArgs e)=>
            {
                if (testButton.IsAlive && source is Application application && textbox.Contains(e.X, e.Y))
                {
                    application.messageToUser.SetString("Hello");
                }
            };
            
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