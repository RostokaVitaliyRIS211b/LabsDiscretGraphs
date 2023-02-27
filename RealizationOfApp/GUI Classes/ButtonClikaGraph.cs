using RealizationOfApp.ElementsOfGraph;

namespace RealizationOfApp.GUI_Classes
{
   
    public class ButtonClikaGraph:EvTextbox
    {
        public Color BuffColor;
        public ButtonClikaGraph(Textbox textbox):base(textbox)
        {
            BuffColor = textbox.GetFillRectColor();
        }
        public override void MouseButtonPressed(object? source, ICollection<EventDrawableGUI> elementsOfGUI, MouseButtonEventArgs e)
        {
            if(IsAlive && textbox.Contains(e.X,e.Y) && source is Application app)
            {
                textbox.SetFillColorRect(Color.Magenta);
                if (app.graph.isNotOriented())
                {
                    try
                    {
                        IEnumerable<IEnumerable<string>> strings = app.graph.ClicksOfGraph();
                        List<string> MaxClick = new();
                        IEnumerator<IEnumerable<string>> enumerator = strings.GetEnumerator();

                        if (enumerator.MoveNext())
                            MaxClick = new(enumerator.Current);
                        else
                            throw new Exception("No Clicks in Graph");

                        foreach(IEnumerable<string> click in strings)
                        {
                            if(MaxClick.Count<click.Count())
                            {
                                MaxClick = new(click);
                            }
                        }

                        IEnumerable<VertexGraph> vertices = from elem in app.eventDrawables
                                                            where (elem is VertexGraph)
                                                            let vertex = elem as VertexGraph
                                                            where (MaxClick.Contains(vertex.GetString()))
                                                            select vertex;
                        foreach (VertexGraph vertex1 in vertices)
                            vertex1.SetTempCol(Color.Magenta);
                    }
                    catch(Exception e1)
                    {
                        app.messageToUser.SetString(e1.Message);
                    }
                }
                else
                {
                    app.messageToUser.SetString("Graph must be not oriented");
                }
            }
            
        }
        public override void MouseMoved(object? source, ICollection<EventDrawableGUI> elementsOfGUI, MouseMoveEventArgs e)
        {
            textbox.SetFillColorRect(BuffColor);
        }
    }
}
