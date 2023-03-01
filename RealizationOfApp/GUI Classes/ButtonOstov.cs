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
                if (app.graph.isNotOriented())
                {
                    try
                    {
                        Graph graph = app.graph.GetMinimunFrame();
                        graph.IsOriented=true;
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
                        int weight = 0;
                        foreach(Graph.Edge edge1 in edges)
                        {
                            builder.Append($" {edge1.NameOne} <-> {edge1.NameTwo}  ,");
                            weight+=edge1.Weight;
                        }
                        builder.Remove(builder.Length-1, 1);
                        app.messageToUser.SetString(builder.ToString() +$"weight = {weight}");
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
            if (IsAlive && textbox.Contains(e.X, e.Y))
            {
                textbox.SetFillColorRect(new(89, 168, 167));
            }
            else
            {
                textbox.SetFillColorRect(BuffColor);
            }
        }
    }
}
