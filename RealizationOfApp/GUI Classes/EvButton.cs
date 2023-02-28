namespace RealizationOfApp.GUI_Classes;

public class EvButton : EvTextbox
{
    public float SpaceBetweenVertexesX = 150;
    public float SpaceBetweenVertexesY = 100;
    public float SpaceFromTop = 130;
    public Color BuffColor;
    public event Action<object?, ICollection<EventDrawableGUI>, MouseMoveEventArgs> OnMouseMoved;
    public event Action<object?, ICollection<EventDrawableGUI>, MouseButtonEventArgs> OnMouseButtonPressed;
    public event Action<object?, ICollection<EventDrawableGUI>, MouseButtonEventArgs> OnMouseButtonReleased;
    public event Action<object?, ICollection<EventDrawableGUI>, MouseWheelScrollEventArgs> OnMouseWheelScrolled;
    public event Action<object?, ICollection<EventDrawableGUI>, KeyEventArgs> OnKeyPressed;
    public event Action<object?, ICollection<EventDrawableGUI>, KeyEventArgs> OnKeyReleased;

    public EvButton(Textbox textbox) : base(textbox)
    {
        BuffColor = textbox.GetFillRectColor();
    }

    public override void MouseMoved(object? source, ICollection<EventDrawableGUI> elementsOfGUI, MouseMoveEventArgs e)
    {
        OnMouseMoved?.Invoke(source, elementsOfGUI, e);
    }

    public override void MouseButtonPressed(object? source, ICollection<EventDrawableGUI> elementsOfGUI,
        MouseButtonEventArgs e)
    {
        OnMouseButtonPressed?.Invoke(source, elementsOfGUI, e);
    }

    public override void MouseButtonReleased(object? source, ICollection<EventDrawableGUI> elementsOfGUI,
        MouseButtonEventArgs e)
    {
        OnMouseButtonReleased?.Invoke(source, elementsOfGUI, e);
    }

    public override void MouseWheelScrolled(object? source, ICollection<EventDrawableGUI> elementsOfGUI,
        MouseWheelScrollEventArgs e)
    {
        OnMouseWheelScrolled?.Invoke(source, elementsOfGUI, e);
    }

    public override void KeyPressed(object? source, ICollection<EventDrawableGUI> elementsOfGUI, KeyEventArgs e)
    {
        OnKeyPressed?.Invoke(source, elementsOfGUI, e);
    }

    public override void KeyReleased(object? source, ICollection<EventDrawableGUI> elementsOfGUI, KeyEventArgs e)
    {
        OnKeyReleased?.Invoke(source, elementsOfGUI, e);
    }
}