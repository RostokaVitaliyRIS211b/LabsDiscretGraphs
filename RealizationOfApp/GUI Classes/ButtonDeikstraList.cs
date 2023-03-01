

using RealizationOfApp.ElementsOfGraph;

namespace RealizationOfApp.GUI_Classes
{
    public class ButtonDeikstraList:EvTextbox
    {
        public List<string> way;
        public int lengthWay=0,lastCount=0;
        public Color BuffColor;
        public ButtonDeikstraList(Textbox textbox,IEnumerable<string> way,int weight,int lastC):base(textbox)
        {
            this.way=new(way);
            lastCount = lastC;
            BuffColor = textbox.GetFillRectColor();
            lengthWay = weight;
            IEnumerator<string> enumerator = way.GetEnumerator();
            enumerator.MoveNext();
            string startName = enumerator.Current, finishName = enumerator.Current;
            while (enumerator.MoveNext())
                finishName = enumerator.Current;
            this.textbox.SetString($"{startName} -> {finishName}  =  {weight}");
        }
        public override void MouseMoved(object? source, ICollection<EventDrawableGUI> elementsOfGUI, MouseMoveEventArgs e)
        {
           
            if (IsAlive && source is Application app)
            {
                if (app.eventDrawables.Count!=lastCount)
                    IsNeedToRemove=true;
            }
            if(IsAlive && textbox.Contains(e.X,e.Y))
            {
                textbox.SetFillColorRect(new(89, 168, 167));
            }
            else
            {
                textbox.SetFillColorRect(BuffColor);
            }
        }
        public override void MouseButtonPressed(object? source, ICollection<EventDrawableGUI> elementsOfGUI, MouseButtonEventArgs e)
        {
            if(IsAlive && source is Application app && textbox.Contains(e.X,e.Y))
            {
                IEnumerable<VertexGraph> vertices = from elem in app.eventDrawables
                                                    where (elem is VertexGraph)
                                                    let vertex = elem as VertexGraph
                                                    where(way.Contains(vertex.GetString()))
                                                    select vertex;
                foreach (VertexGraph vertex1 in vertices)
                    vertex1.SetTempCol(Color.Magenta);
            }
            else if(IsAlive && source is Application app2)
            {
                if (app2.eventDrawables.Count!=lastCount)
                    IsNeedToRemove=true;
            }
        }
    }
}
