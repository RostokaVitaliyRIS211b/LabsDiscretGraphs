

using RealizationOfApp.ElementsOfGraph;
using SFML.Graphics;

namespace RealizationOfApp.GUI_Classes
{
    public class ButtonOriented:EvTextbox
    {
        public Color BuffColor;
        public bool IsPressed = false;
        public ButtonOriented(Textbox textbox) : base(textbox)
        {
            BuffColor = textbox.GetFillRectColor();
        }
        public override void MouseMoved(object? source, ICollection<EventDrawableGUI> elementsOfGUI, MouseMoveEventArgs e)
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
        public override void MouseButtonPressed(object? source, ICollection<EventDrawableGUI> elementsOfGUI, MouseButtonEventArgs e)
        {
            if(IsAlive && !IsPressed && textbox.Contains(e.X,e.Y) && source is Application app)
            {
                IsPressed = true;
                app.IsOriented = !app.IsOriented;
                app.graph.IsOriented = app.IsOriented;
                textbox.SetString($"{(app.IsOriented?"Oriented":"Not Oriented")}");
                List<VertexGraph> vertices = new(from elem in app.eventDrawables
                                             where(elem is VertexGraph)
                                             let vertex = elem as VertexGraph
                                             select vertex);
                List<EdgeEv> edgeEvs = new(from elem in app.eventDrawables
                                                 where (elem is EdgeEv)
                                                 let vertex = elem as EdgeEv
                                                 select vertex);
                if(!app.IsOriented)
                {
                    foreach (VertexGraph vertex1 in vertices)
                    {
                        foreach (VertexGraph vertex2 in vertices)
                        {
                            string name1 = vertex1.GetString(), name2 = vertex2.GetString();
                            if (vertex1!=vertex2 && app.graph[name1, name2]==0 && app.graph[name2, name1]>0)
                            {
                                app.graph[name1, name2]=app.graph[name2, name1];
                                Arrow arrow = new();
                                EdgeEv edge = new(new Edge(new(vertex1.GetPos(), Color.Black), new(vertex2.GetPos(), Color.Black),
                                    app.graph[name2, name1].ToString()), vertex1, vertex2, ref arrow);
                                vertex1.incindentEdges.Add(edge);
                                vertex2.incindentEdges.Add(edge);
                                app.eventDrawables.Insert(app.eventDrawables.Count - 1, arrow);
                                app.eventDrawables.Insert(0, edge);
                            }
                            else if(vertex1!=vertex2 && app.graph[name1, name2]!=app.graph[name2, name1] 
                                && app.graph[name2, name1]>0 && app.graph[name1, name2]>0)
                            {
                                app.graph[name1, name2]=app.graph[name2, name1];
                                EdgeEv? edge1 = edgeEvs.Find(x=>x.startVer==vertex1 && x.endVer==vertex2), 
                                    edge2 = edgeEvs.Find(x => x.startVer==vertex2 && x.endVer==vertex1);
                                if(edge1 is not null && edge2 is not null)
                                {
                                    edge1.SetWeight(app.graph[name2, name1]);
                                    edge2.SetWeight(app.graph[name2, name1]);
                                }
                            }
                        }
                    }
                    foreach (EdgeEv edgeEv in edgeEvs)
                        edgeEv.edge.isOriented=false;
                }
                else
                {
                    for(int i=0;i<vertices.Count;++i)
                    {
                        VertexGraph vertex1 = vertices[i];
                        for(int j = i+1;j<vertices.Count;++j)
                        {
                            string name1 = vertex1.GetString(), name2 = vertices[j].GetString();
                            app.graph[name2, name1]=0;
                            EdgeEv? ev = edgeEvs.Find(x => x.startVer==vertices[j] && x.endVer==vertex1);
                            if(ev is not null)
                            {
                                app.eventDrawables.Remove(ev);
                                app.eventDrawables.Remove(ev.arrow);
                                vertex1.incindentEdges.Remove(ev);
                                vertices[j].incindentEdges.Remove(ev);
                            }
                        }
                    }
                    foreach (EdgeEv edgeEv in edgeEvs)
                        edgeEv.edge.isOriented=true;
                }
            }
        }
        public override void MouseButtonReleased(object? source, ICollection<EventDrawableGUI> elementsOfGUI, MouseButtonEventArgs e)
        {
            IsPressed = false;
        }
    }
}
