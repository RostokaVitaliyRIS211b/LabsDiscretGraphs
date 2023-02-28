

namespace RealizationOfApp
{
    public static class ColorInterpolator
    {
        delegate byte ComponentSelector(Color color);
        static readonly ComponentSelector redSelector = color => color.R;
        static readonly ComponentSelector greenSelector = color => color.G;
        static readonly ComponentSelector blueSelector = color => color.B;

        public static Color InterpolateBetween(
            Color endPoint1,
            Color endPoint2,
            double lambda)
        {
            if (lambda < 0 || lambda > 1)
            {
                throw new ArgumentOutOfRangeException($"{lambda} was out of range");
            }
            Color color = new(
                InterpolateComponent(endPoint1, endPoint2, lambda, redSelector),
                InterpolateComponent(endPoint1, endPoint2, lambda, greenSelector),
                InterpolateComponent(endPoint1, endPoint2, lambda, blueSelector)
            );

            return color;
        }

        static byte InterpolateComponent(
            Color endPoint1,
            Color endPoint2,
            double lambda,
            ComponentSelector selector)
        {
            return (byte)(selector(endPoint1) + (selector(endPoint2) - selector(endPoint1)) * lambda);
        }
    }
}
