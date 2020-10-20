using Xamarin.Forms;

namespace Xamarin.X247Grad.Animation
{
    /**
     * Animation interpolation utilities specifically for <see cref="AnimationSets"/>, <see cref="AnimationSet"/> and
     * <see cref="AnimationStop"/>.
     */
    public static class Interpolate
    {
        /// <summary>
        /// Linear interpolation between <paramref name="a"/> and <paramref name="b"/> at <paramref name="x"/>.
        /// </summary>
        /// <param name="a">The start value.</param>
        /// <param name="b">The end value.</param>
        /// <param name="x">The position between start and end.</param>
        /// <returns>Returns the interpolated value.</returns>
        public static int Linear(int a, int b, double x) =>
            (int) (a * (1.0 - x) + b * x);

        /// <summary>
        /// Linear interpolation between <paramref name="a"/> and <paramref name="b"/> at <paramref name="x"/>.
        /// </summary>
        /// <param name="a">The start value.</param>
        /// <param name="b">The end value.</param>
        /// <param name="x">The position between start and end.</param>
        /// <returns>Returns the interpolated value.</returns>
        public static float Linear(float a, float b, double x) =>
            (float) (a * (1.0 - x) + b * x);

        /// <summary>
        /// Linear interpolation between <paramref name="a"/> and <paramref name="b"/> at <paramref name="x"/>.
        /// </summary>
        /// <param name="a">The start value.</param>
        /// <param name="b">The end value.</param>
        /// <param name="x">The position between start and end.</param>
        /// <returns>Returns the interpolated value.</returns>
        public static double Linear(double a, double b, double x) =>
            a * (1.0 - x) + b * x;

        /// <summary>
        /// Linear interpolation between <paramref name="a"/> and <paramref name="b"/> at <paramref name="x"/>.
        /// </summary>
        /// <param name="a">The start value.</param>
        /// <param name="b">The end value.</param>
        /// <param name="x">The position between start and end.</param>
        /// <returns>Returns the interpolated value.</returns>
        public static Thickness Linear(Thickness a, Thickness b, double x) =>
            new Thickness(Linear(a.Left, b.Left, x), Linear(a.Top, b.Top, x), Linear(a.Right, b.Right, x),
                Linear(a.Bottom, b.Bottom, x));

        /// <summary>
        /// Linear interpolation between <paramref name="a"/> and <paramref name="b"/> at <paramref name="x"/>.
        /// </summary>
        /// <param name="a">The start value.</param>
        /// <param name="b">The end value.</param>
        /// <param name="x">The position between start and end.</param>
        /// <returns>Returns the interpolated value.</returns>
        public static Size Linear(Size a, Size b, double x) =>
            new Size(Linear(a.Width, b.Width, x), Linear(a.Height, b.Height, x));

        /// <summary>
        /// Linear interpolation between <paramref name="a"/> and <paramref name="b"/> at <paramref name="x"/>.
        /// </summary>
        /// <param name="a">The start value.</param>
        /// <param name="b">The end value.</param>
        /// <param name="x">The position between start and end.</param>
        /// <returns>Returns the interpolated value.</returns>
        public static Color Linear(Color a, Color b, double x) =>
            new Color(Linear(a.R, b.R, x), Linear(a.G, b.G, x), Linear(a.B, b.B, x), Linear(a.A, b.A, x));
    }
}