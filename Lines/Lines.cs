

namespace Lines
{
    public class VectorFloat
    {
        public Vector2f VecCoords { get; protected set; }
        public VectorFloat()
        {

        }
        public VectorFloat(Vector2f vector1,Vector2f vector2)
        {
            VecCoords = new(vector1.X-vector2.X, vector1.Y-vector2.Y);
        }
        public double Dlina()
        {
            return Math.Pow(VecCoords.X, 2)+Math.Pow(VecCoords.Y, 2);
        }
        public double Angle(VectorFloat vector1)
        {
            return Math.Acos( (this*vector1)/(Dlina()*vector1.Dlina() ) );
        }
        public static double operator *(VectorFloat one,VectorFloat two)
        {
            return one.VecCoords.X*two.VecCoords.X+one.VecCoords.Y*two.VecCoords.Y;
        }
        public void SetVec(Vector2f vector1, Vector2f vector2)
        {
            VecCoords = new(vector1.X-vector2.X, vector1.Y-vector2.Y);
        }

    }

    public class Edge:Drawable
    {
        protected Vertex vertex1;
        protected Vertex middleVertex;
        protected Vertex vertex2;
        protected Font font= new Font(Directory.GetCurrentDirectory()+"/ofont.ru_Impact.ttf");
        protected Text weight1;
        public bool isOriented = true;
        //protected Text weight2;
        protected readonly uint CharacterSize = 15;
        public Edge()
        {
            weight1 = new()
            {
                FillColor = Color.Black,
                Font=font,
                CharacterSize=15,
                OutlineColor = Color.White
            };
            //weight2 = new(weight1);
        }
        public Edge (Edge edge)
        {
            vertex1 = edge.vertex1;
            vertex2 = edge.vertex2;
            middleVertex = edge.middleVertex;
            font = edge.font;
            weight1 = new(edge.weight1);
            //weight2 = new(edge.weight2);
            CharacterSize = edge.CharacterSize;
        }
        public Edge(Vertex vertex1,Vertex vertex2,string text1)
        {
            weight1 = new()
            {
                FillColor = Color.Black,
                Font=font,
                CharacterSize=CharacterSize,
                OutlineColor = Color.White
            };
            //weight2 = new(weight1);

            weight1.DisplayedString= text1 ;
            //weight2.DisplayedString=!isOne ? text1 : null;

            weight1.OutlineThickness = 5 ;
            //weight2.OutlineThickness = !isOne ? 5 : 0;

            weight1.Origin = new Vector2f(weight1.GetGlobalBounds().Width / 2f, weight1.GetGlobalBounds().Height / 2f+weight1.CharacterSize/6f);// магические числа на
            //weight2.Origin = new Vector2f(weight1.GetGlobalBounds().Width / 2f, weight1.GetGlobalBounds().Height / 2f+weight1.CharacterSize/6f);// магические числа на

            this.vertex1 = new(vertex1.Position,vertex1.Color);
            this.vertex2 = new(vertex2.Position, vertex2.Color);
            float posXMiddle = ((vertex1.Position.X+vertex2.Position.X)/2);
            float posYMiddle = ((vertex1.Position.Y+vertex2.Position.Y)/2);
            middleVertex = new(new(posXMiddle, posYMiddle), Color.Black);
            float DifferenceX = vertex1.Position.X-vertex2.Position.X-CharacterSize;
            float DifferenceY = vertex1.Position.Y-vertex2.Position.Y-CharacterSize;
            weight1.Position = new Vector2f(vertex1.Position.X-DifferenceX/4, vertex1.Position.Y-DifferenceY/4);
            //weight2.Position = new Vector2f(posXMiddle+posXMiddle/2, posYMiddle+posYMiddle/2);
        }
        //public Edge(Vertex vertex1, Vertex vertex2, string text1, string text2)
        //{
        //    weight1 = new();
        //    weight2 = new();

        //    weight1.DisplayedString=text1;
        //    weight2.DisplayedString=text2;

        //    weight1.OutlineThickness = 5;
        //    weight2.OutlineThickness = 5;

        //    weight1.Origin = new Vector2f(weight1.GetGlobalBounds().Width / 2f, weight1.GetGlobalBounds().Height / 2f+weight1.CharacterSize/6f);// магические числа на
        //    weight2.Origin = new Vector2f(weight1.GetGlobalBounds().Width / 2f, weight1.GetGlobalBounds().Height / 2f+weight1.CharacterSize/6f);// магические числа на

        //    this.vertex1 = new(vertex1.Position, vertex1.Color);
        //    this.vertex2 = new(vertex2.Position, vertex2.Color);

        //    float posXMiddle = (vertex1.Position.X+vertex2.Position.X-CharacterSize);
        //    float posYMiddle = (vertex1.Position.Y+vertex2.Position.Y-CharacterSize);

        //    weight1.Position = new Vector2f(posXMiddle-posXMiddle/2,posYMiddle-posYMiddle/2);
        //    weight2.Position = new Vector2f(posXMiddle+posXMiddle/2, posYMiddle+posYMiddle/2);
        //}
        public void SetVertex1(float x,float y)
        {
            vertex1.Position = new Vector2f(x, y);
            float posXMiddle = ((vertex1.Position.X+vertex2.Position.X)/2);
            float posYMiddle = ((vertex1.Position.Y+vertex2.Position.Y)/2);
            middleVertex.Position = new(posXMiddle, posYMiddle);
            float DifferenceX = vertex1.Position.X-vertex2.Position.X-CharacterSize;
            float DifferenceY = vertex1.Position.Y-vertex2.Position.Y-CharacterSize;
            weight1.Position = new Vector2f(vertex1.Position.X-DifferenceX/4, vertex1.Position.Y-DifferenceY/4);
            //weight2.Position = new Vector2f(posXMiddle+posXMiddle/2, posYMiddle+posYMiddle/2);
        }
        public void SetVertex2(float x, float y)
        {
            vertex2.Position = new Vector2f(x, y);
            float posXMiddle = ((vertex1.Position.X+vertex2.Position.X)/2);
            float posYMiddle = ((vertex1.Position.Y+vertex2.Position.Y)/2);
            middleVertex.Position = new(posXMiddle, posYMiddle);
            float DifferenceX = vertex1.Position.X-vertex2.Position.X-CharacterSize;
            float DifferenceY = vertex1.Position.Y-vertex2.Position.Y-CharacterSize;
            weight1.Position = new Vector2f(vertex1.Position.X-DifferenceX/4, vertex1.Position.Y-DifferenceY/4);
            //weight2.Position = new Vector2f(posXMiddle+posXMiddle/2, posYMiddle+posYMiddle/2);
        }
        public void SetVertex1(Vector2f vector)
        {
            vertex1.Position = vector;
            float posXMiddle = ((vertex1.Position.X+vertex2.Position.X)/2);
            float posYMiddle = ((vertex1.Position.Y+vertex2.Position.Y)/2);
            middleVertex.Position = new(posXMiddle, posYMiddle);
            float DifferenceX = vertex1.Position.X-vertex2.Position.X-CharacterSize;
            float DifferenceY = vertex1.Position.Y-vertex2.Position.Y-CharacterSize;
            weight1.Position = new Vector2f(vertex1.Position.X-DifferenceX/4, vertex1.Position.Y-DifferenceY/4);
            //weight2.Position = new Vector2f(posXMiddle+posXMiddle/2, posYMiddle+posYMiddle/2);
        }
        public void SetVertex2(Vector2f vector)
        {
            vertex2.Position = vector;
            float posXMiddle = ((vertex1.Position.X+vertex2.Position.X)/2);
            float posYMiddle = ((vertex1.Position.Y+vertex2.Position.Y)/2);
            middleVertex.Position = new(posXMiddle, posYMiddle);
            float DifferenceX = vertex1.Position.X-vertex2.Position.X-CharacterSize;
            float DifferenceY = vertex1.Position.Y-vertex2.Position.Y-CharacterSize;
            weight1.Position = new Vector2f(vertex1.Position.X-DifferenceX/4, vertex1.Position.Y-DifferenceY/4);
            //weight2.Position = new Vector2f(posXMiddle+posXMiddle/2, posYMiddle+posYMiddle/2);
        }
        public void SetFont(Font font)
        {
            this.font = font;
        }
        public void SetWeight1(string number)
        {
            weight1.DisplayedString=number;

            weight1.Origin = new Vector2f(weight1.GetGlobalBounds().Width / 2f, weight1.GetGlobalBounds().Height / 2f+weight1.CharacterSize/6f);// магические числа на

            weight1.OutlineThickness = 5;
            
        }
        //public void SetWeight2(string number)
        //{
        //    weight2.DisplayedString=number;

        //    weight2.Origin = new Vector2f(weight1.GetGlobalBounds().Width / 2f, weight1.GetGlobalBounds().Height / 2f+weight1.CharacterSize/6f);// магические числа на

        //    weight1.OutlineThickness = 5;
        //}
        public float Dlina(float x,float y,float x1,float y1)
        {
            return (float)Math.Sqrt(Math.Pow(x-x1,2)+Math.Pow(y-y1, 2));
        }
        public float Dlina(Vector2f point1,Vector2f point2)
        {
            return (float)Math.Sqrt(Math.Pow(point1.X-point2.X, 2)+Math.Pow(point1.Y-point2.Y, 2));
        }  
        public void SetColor(Color color)
        {
            vertex1.Color = color;
            middleVertex.Color = color;
            vertex2.Color = color;
        }
        public Color GetColor()
        {
            return vertex1.Color;
        }
        public double Angle(float x, float y, float x1, float y1)
        {
            Vector2f b = new( 6, 0 );
            Vector2f b1 = new(100, 0);
            Vector2f a = new( x-x1, y-y1 );
            return Math.Acos( ( new VectorFloat(b,a)*new VectorFloat(b,b1)) /(Dlina(a, b)*Dlina(b,b1) ) );
        }
        public double Angle()
        {
            Vector2f b = new(6, 720);
            Vector2f b1 = new(1000, 720);
            return (new VectorFloat(vertex1.Position,vertex2.Position )*new VectorFloat(b, b1)) /(Dlina(vertex1.Position, vertex2.Position)*Dlina(b, b1));
        }
        public bool Contains(float x, float y)
        {
            double angle = Angle();
            double accuracy = 0.15*Math.Abs(Math.Pow(angle,4));
            if (angle<0.500 && angle>-0.400)
                accuracy = 0.15;
            if (angle>0.9999 || (angle<0.010 && angle>-0.011) || (angle<-0.9999))
                accuracy = 2;
            float posXMiddle = ((vertex1.Position.X+vertex2.Position.X)/2);
            float posYMiddle = ((vertex1.Position.Y+vertex2.Position.Y)/2);
            double lengthOfLine = Dlina(vertex1.Position, vertex2.Position)+accuracy;
            double lengthAboutToPoint = Dlina(x, y, vertex1.Position.X, vertex1.Position.Y);
            double lengthAboutToPoint2 = Dlina(x, y, posXMiddle, posYMiddle);
            double checkPointNearToLine = Math.Abs((x-vertex1.Position.X)/(vertex2.Position.X-vertex1.Position.X) - (y-vertex1.Position.Y)/(vertex2.Position.Y-vertex1.Position.Y));
            return (checkPointNearToLine < accuracy) && ( lengthAboutToPoint <= lengthOfLine/2) && (lengthAboutToPoint2 <= lengthOfLine/2);
        }
        public bool Contains(Vector2f point)
        {
            return Contains(point.X, point.Y);
        }
        public Vector2f GetPosVer1()
        {
            return vertex1.Position;
        }
        public Vector2f GetPosVer2()
        {
            return vertex2.Position;
        }
        public string GetWeight()
        {
            return weight1.DisplayedString;
        }
        public Vector2f GetWeightPosition()
        {
            return weight1.Position;
        }
        public void Draw(RenderTarget target,RenderStates states)
        {
            target.Draw(ToArr(), PrimitiveType.Lines, states);
            target.Draw(weight1);
            //target.Draw(weight2);
        }
        public Vertex[] ToArr()
        {
            return new Vertex[2] { vertex1,isOriented?vertex2:middleVertex};
        }
    }
}
