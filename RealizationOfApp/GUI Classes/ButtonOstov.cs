using RealizationOfApp.ElementsOfGraph;
using System.Text;

namespace RealizationOfApp.GUI_Classes
{
    
    public class ButtonOstov:EvTextbox
    {
        public Color BuffColor;
        public ButtonOstov(Textbox textbox) : base(textbox)
        {
            BuffColor = textbox.GetFillRectColor();
        }
        public override void MouseButtonPressed(object? source, ICollection<EventDrawableGUI> elementsOfGUI, MouseButtonEventArgs e)
        {
            if (IsAlive && textbox.Contains(e.X, e.Y) && source is Application app)
            {
                textbox.SetFillColorRect(Color.Magenta);
                if (app.graph.isNotOriented())
                {
                    try
                    {
                        Graph graph = app.graph.GetMinimunFrame();
                        List<Graph.Edge> edges = new(graph.GetEdges());
                        IEnumerable<EdgeEv> edgeEvs = from edge in app.eventDrawables
                                                      where (edge is EdgeEv)
                                                      let edgeEv = edge as EdgeEv
                                                      where(edges.Find(x=>x.NameOne==edgeEv.startVer.GetString() 
                                                      && x.NameTwo==edgeEv.endVer.GetString())
                                                      is not null)
                                                      select edgeEv;
                        foreach (EdgeEv ev in edgeEvs)
                            ev.SetTempColor(Color.Magenta);
                        graph.IsOriented = false;
                        edges = new(graph.GetEdges());
                        StringBuilder builder = new();
                        Console.WriteLine(edges.Count);
                        int counter = 1;
                        foreach(Graph.Edge edge1 in edges)
                        {
                            if(counter!=edges.Count)
                            {
                                builder.Append($" {edge1.NameOne} ->");
                            }
                            else
                            {
                                builder.Append($" {edge1.NameOne} -> {edge1.NameTwo}");
                            }
                            ++counter;
                        }
                        app.messageToUser.SetString(builder.ToString());
                    }
                    catch (Exception e1)
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
