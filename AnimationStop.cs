using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Xamarin.X247Grad.Animation
{
    /// <summary>
    /// A stop in an animation set.
    /// </summary>
    [ContentProperty("Setters")]
    public class AnimationStop
    {
        /// <summary>
        /// Compares the setter's property.
        /// </summary>
        private class EqualProperty : IEqualityComparer<Setter>
        {
            /// <summary>
            /// The instance of the comparer.
            /// </summary>
            public static readonly EqualProperty Instance = new EqualProperty();

            public bool Equals(Setter x, Setter y) =>
                x == null ? y == null : ReferenceEquals(x, y) || x.Property == y?.Property;

            public int GetHashCode(Setter obj) =>
                obj.Property.GetHashCode();
        }

        /// <summary>
        /// Backing for the setter list.
        /// </summary>
        private IList<Setter> _setters;

        /// <summary>
        /// The setters to apply for this stop.
        /// </summary>
        public IList<Setter> Setters => _setters ??= new List<Setter>();

        /// <summary>
        /// If given, this style's setters are used instead.
        /// </summary>
        public Style BasedOn { get; set; }

        /// <summary>
        /// All applied setters, first the locally defined setters, then the style setters if present.
        /// </summary>
        public IEnumerable<Setter> AllSetters => BasedOn == null
            ? Setters
            : Setters.Concat(BasedOn.Setters).Distinct(EqualProperty.Instance);

        /// <summary>
        /// The animation rate.
        /// </summary>
        public uint Rate { get; set; } = 16U;

        /// <summary>
        /// The animation length. If zero, values will be assigned instead of animated.
        /// </summary>
        public uint Length { get; set; } = 250U;

        /// <summary>
        /// The easing of the animation stop.
        /// </summary>
        public Easing Easing { get; set; }
    }
}