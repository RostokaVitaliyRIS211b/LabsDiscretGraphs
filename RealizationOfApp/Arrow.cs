using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealizationOfApp
{
    public class Arrow:EventDrawable
    {
        Edge edge;
        public Arrow(ref Edge edge)
        {
            this.edge = edge;
        }
        public override void Draw(RenderTarget target, RenderStates states)
        {
            float posXMiddle = ((edge.GetPosVer1().X+edge.GetPosVer2().X)/2);
            float posYMiddle = ((edge.GetPosVer1().Y+edge.GetPosVer2().Y)/2);
            bool DifferenceY = edge.GetPosVer1().Y-edge.GetPosVer2().Y<=0;
            float CosAngle = (float)edge.Angle();
            float SinAngle = (float)Math.Sqrt(1-CosAngle*CosAngle);
            Vertex vertexArrMid = new(new(posXMiddle, posYMiddle), Color.Red);
            Vertex verUp = new(new(posXMiddle -20*CosAngle, posYMiddle+ (DifferenceY ? -20*SinAngle : +20*SinAngle)), Color.Black);
            Vertex verUp2 = new(new(verUp.Position.X +(!DifferenceY ? -10*SinAngle : +10*SinAngle), verUp.Position.Y-10*CosAngle), Color.Red);
            Vertex verUp3 = new(new(verUp.Position.X -(!DifferenceY ? -10*SinAngle : +10*SinAngle), verUp.Position.Y+10*CosAngle), Color.Red);
            Vertex[] vertices = new Vertex[3] { verUp2, verUp3, vertexArrMid };
            target.Draw(vertices, PrimitiveType.Triangles, states);
        }
    }
}
