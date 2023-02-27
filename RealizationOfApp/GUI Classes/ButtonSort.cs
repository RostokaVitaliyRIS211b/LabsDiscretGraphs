
using RealizationOfApp.ElementsOfGraph;
using SfmlAppLib;

namespace RealizationOfApp.GUI_Classes
{
    public class ButtonSort:EvTextbox
    {
        public float SpaceBetweenVertexesX = 150;
        public float SpaceBetweenVertexesY = 100;
        public float SpaceFromTop = 130;
        public Color BuffColor;
        public ButtonSort(Textbox textbox) : base(textbox)
        {
            BuffColor = textbox.GetFillRectColor();
        }
        public override void MouseMoved(object? source, IEnumerable<EventDrawableGUI> elementsOfGUI, MouseMoveEventArgs e)
        {
            if (IsAlive && textbox.Contains(e.X, e.Y))
            {
                textbox.SetFillColorRect(Color.Magenta);
            }
            else
            {
                textbox.SetFillColorRect(BuffColor);
            }
        }
        public override void MouseButtonPressed(object? source, IEnumerable<EventDrawableGUI> elementsOfGUI, MouseButtonEventArgs e)
        {
            if(IsAlive && textbox.Contains(e.X,e.Y) && source is Application app)
            {

                if(app.graph.IsOneDirected())
                {
                    IEnumerable<IEnumerable<string>> levels = app.graph.GetTieredParallelForm();
                    List<VertexGraph> vertexes = new(from elem in app.eventDrawables
                                                     where (elem is VertexGraph)
                                                     let ver = elem as VertexGraph
                                                     select ver);
                    float startPosY = SpaceFromTop;
                    foreach(IEnumerable<string> level in levels)
                    {
                        float startPosX = app.CurrentWidth/2-SpaceBetweenVertexesX*level.Count()/2;
                        foreach(string name in level)
                        {
                            vertexes.Find(x => x.GetString()==name)?.SetPos(startPosX, startPosY);
                            startPosX+=SpaceBetweenVertexesX;
                        }
                        startPosY+=SpaceBetweenVertexesY;
                    }
                }
            }
        }
    }
}
