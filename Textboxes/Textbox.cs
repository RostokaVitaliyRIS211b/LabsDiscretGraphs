namespace Textboxes
{
    public class Textbox: Drawable
    {
        protected RectangleShape rect;
        protected Text text;
        protected Font font;
        public Textbox()
        { 
            rect = new RectangleShape();
            text = new Text();
            text.OutlineThickness = 0;
            font = new Font(Directory.GetCurrentDirectory()+"/ofont.ru_Impact.ttf"); text.Font = font; 
        }
        public Textbox(RectangleShape rect, Text text, Font font)
        {
            font = new Font(Directory.GetCurrentDirectory()+"/ofont.ru_Impact.ttf"); text.Font = font;
            this.rect = rect;
            this.text = text;
            this.font = font;
        }
        public Textbox(in Textbox textbox)
        {
            rect = new RectangleShape();
            text = new Text();
            font = new Font(Directory.GetCurrentDirectory()+"/ofont.ru_Impact.ttf"); text.Font = font;
            SetSizeCharacterText((int)textbox.text.CharacterSize);
            SetString(textbox.text.DisplayedString);
            SetColortText(textbox.text.FillColor);
            SetOutlineColorRect(textbox.rect.OutlineColor);
            SetOutlineThicknessRect(textbox.rect.OutlineThickness);
            SetFillColorRect(textbox.rect.FillColor);
            SetSizeRect(textbox.rect.Size);
            SetPos(textbox.rect.Position);
        }
        public void Copy(in Textbox textbox)
        {
            SetSizeCharacterText((int)textbox.text.CharacterSize);
            SetString(textbox.text.DisplayedString);
            SetColortText(textbox.text.FillColor);
            SetOutlineColorRect(textbox.rect.OutlineColor);
            SetOutlineThicknessRect(textbox.rect.OutlineThickness);
            SetFillColorRect(textbox.rect.FillColor);
            SetSizeRect(textbox.rect.Size);
            SetPos(textbox.rect.Position);
        }
        public void SetColortText(Color color)
        {
            text.FillColor=color;
        }
        public void SetFillColorRect(Color color)
        {
            rect.FillColor = color;
        }
        public void SetOutlineColorRect(Color color)
        {
            rect.OutlineColor = color;
        }
        public void SetOutlineThicknessRect(float thick)
        {
            rect.OutlineThickness = thick;
        }
        public void SetSizeRect(float width, float height)
        {
            rect.Size = new Vector2f(width, height);
            rect.Origin = new Vector2f(width / 2, height / 2);
        }
        public void SetSizeRect(Vector2f size)
        {
            rect.Size = size;
            rect.Origin = new Vector2f(size.X / 2, size.Y / 2);
        }
        public void SetPos(float x, float y)
        {
            //suConsole.WriteLine("rect {0} {1}", rect.Origin, rect.Size);
            text.Origin = new Vector2f(text.GetGlobalBounds().Width / 2f, text.GetGlobalBounds().Height / 2f+text.CharacterSize/6f);// магические числа на
            rect.Position = new Vector2f(x, y);
            text.Position = new Vector2f(x, y);
            //suConsole.WriteLine("text {0} {1} {2}", text.Origin, text.GetGlobalBounds().Width, text.GetGlobalBounds().Top);
        }
        public void SetPos(Vector2f vec)
        {
            //suConsole.WriteLine("rect {0} {1}", rect.Origin, rect.Size);
            text.Origin = new Vector2f(text.GetGlobalBounds().Width / 2f, text.GetGlobalBounds().Height / 2f + text.CharacterSize / 6f);// магические числа на
            rect.Position = vec;
            text.Position = vec;
            //suConsole.WriteLine("text {0} {1} {2}", text.Origin, text.GetGlobalBounds().Width, text.GetGlobalBounds().Top);
        }
        public void SetSizeCharacterText(int size)
        {
            text.CharacterSize = (uint)size;
            text.Origin = new Vector2f(text.GetGlobalBounds().Width / 2f, text.GetGlobalBounds().Height / 2f + text.CharacterSize / 6f);// магические числа на
        }
        public void SetString(string str)
        {
            text.DisplayedString = str;
            text.Origin = new Vector2f(text.GetGlobalBounds().Width / 2f, text.GetGlobalBounds().Height / 2f + text.CharacterSize / 6f);// магические числа на
        }
        public string GetString()
        {
            return text.DisplayedString;
        }
        public Vector2f GetSizeRect()
        {
            return rect.Size;
        }
        public Vector2f GetPosition()
        {
            return rect.Position;
        }
        public bool Contains(float x,float y)
        {
            return rect.GetGlobalBounds().Contains(x, y);
        }
        public bool Contains (Vector2f pos)
        {
            return rect.GetGlobalBounds().Contains(pos.X, pos.Y);
        }
        public bool Contains(Vector2i pos)
        {
            return rect.GetGlobalBounds().Contains(pos.X, pos.Y);
        }
        public Text GetText()
        {
            return text;
        }
        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(rect, states);
            target.Draw(text, states);
        }
    }
    public class CircleTextbox:Drawable
    {
        protected CircleShape circle;
        protected Text text;
        protected Font font;
        public CircleTextbox()
        {
            circle = new();
            font = new(Directory.GetCurrentDirectory()+"/ofont.ru_Impact.ttf");
            text = new(String.Empty,font);
        }
        public CircleTextbox(CircleShape circle, Text text, Font font)
        {
            this.circle=circle;
            this.text=text;
            this.font=font;
        }
        public CircleTextbox(in CircleTextbox circleTextbox)
        {
            circle = new();
            font = circleTextbox.font;
            text = new(circleTextbox.GetString(), font);
            SetCharacterSize(circleTextbox.GetText().CharacterSize);
            SetString(circleTextbox.GetString());
            SetOutlineThicknessText(circleTextbox.GetText().OutlineThickness);
            SetRadius(circleTextbox.GetRadius());
            SetOutlineThicknessCircle(circleTextbox.GetOutlineCircle());
            SetOutlineColorCircle(circleTextbox.GetOutlineColorCircle());
            SetOutlineColorText(circleTextbox.GetText().OutlineColor);
            SetFillColorCircle(circleTextbox.GetFillColorCircle());
            SetFillColorText(circleTextbox.GetText().FillColor);
            SetPosition(0, 0);
        }
        public void SetFillColorCircle(Color color)
        {
            circle.FillColor = color;
        }
        public void SetFillColorText(Color color)
        {
            text.FillColor = color;
        }
        public void SetOutlineColorCircle(Color color)
        {
            circle.OutlineColor = color;
        }
        public void SetOutlineColorText(Color color)
        {
            text.OutlineColor = color;
        }
        public void SetOutlineThicknessCircle(float thickness)
        {
            circle.OutlineThickness = thickness;
        }
        public void SetOutlineThicknessText(float thickness)
        {
            text.OutlineThickness = thickness;
        }
        public void SetRadius(float radius)
        {
            circle.Radius = radius;
            circle.Origin = new Vector2f(radius,radius);
        }
        public void SetCharacterSize(uint size)
        {
            text.CharacterSize = size;
            text.Origin = new Vector2f(text.GetGlobalBounds().Width / 2f, text.GetGlobalBounds().Height / 2f + text.CharacterSize / 6f);// магические числа на
        }
        public void SetString(string text)
        {
            this.text.DisplayedString = text;
            this.text.Origin = new Vector2f(this.text.GetGlobalBounds().Width / 2f, this.text.GetGlobalBounds().Height / 2f + this.text.CharacterSize / 6f);// магические числа на
        }
        public string GetString()
        {
            return text.DisplayedString;
        }
        public float GetRadius()
        {
            return circle.Radius;
        }
        public void GetPosition(out float x,out float y)
        {
            x = circle.Position.X;
            y = circle.Position.Y;
        }
        public Vector2f GetPosition()
        {
            return circle.Position;
        }
        public void GetPosition(out Vector2f vector2F)
        {
            vector2F = circle.Position;
        }
        public float GetOutlineCircle()
        {
            return circle.OutlineThickness;
        }
        public Color GetFillColorCircle()
        {
            return circle.FillColor;
        }
        public Color GetOutlineColorCircle()
        {
            return circle.OutlineColor;
        }
        public void SetPosition(Vector2f pos)
        {
            //suConsole.WriteLine("rect {0} {1}", rect.Origin, rect.Size);
            text.Origin = new Vector2f(text.GetGlobalBounds().Width / 2f, text.GetGlobalBounds().Height / 2f+text.CharacterSize/6f);// магические числа на
            circle.Position = pos;
            text.Position = pos;
            //suConsole.WriteLine("text {0} {1} {2}", text.Origin, text.GetGlobalBounds().Width, text.GetGlobalBounds().Top);
        }
        public void SetPosition(float x,float y)
        {
            //suConsole.WriteLine("rect {0} {1}", rect.Origin, rect.Size);
            text.Origin = new Vector2f(text.GetGlobalBounds().Width / 2f, text.GetGlobalBounds().Height / 2f+text.CharacterSize/6f);// магические числа на
            circle.Position = new Vector2f(x, y);
            text.Position = new Vector2f(x, y);
            //suConsole.WriteLine("text {0} {1} {2}", text.Origin, text.GetGlobalBounds().Width, text.GetGlobalBounds().Top);
        }
        public bool Contains(float x,float y)
        {
            return circle.GetGlobalBounds().Contains(x, y);
        }
        public bool Contains(Vector2f vector2F)
        {
            return circle.GetGlobalBounds().Contains(vector2F.X, vector2F.Y);
        }
        public bool Contains(Vector2i vector2I)
        {
            return circle.GetGlobalBounds().Contains(vector2I.X, vector2I.Y);
        }
        public Text GetText()
        {
            return text;
        }
        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(circle);
            target.Draw(text);
        }
    }

}
