using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xamarin.X247Grad.Animation.Behaviors
{
    /// <summary>
    /// Plays the contained animation 
    /// </summary>
    [ContentProperty("Animation")]
    public class AttachAnimation : Behavior<VisualElement>
    {
        /// <summary>
        /// Attached visual elements.
        /// </summary>
        private readonly List<VisualElement> _elements = new List<VisualElement>();

        /// <summary>
        /// Property for <see cref="Animation"/>.
        /// </summary>
        public static readonly BindableProperty AnimationProperty =
            BindableProperty.Create(nameof(Animation), typeof(IAnimationController), typeof(AttachAnimation));

        /// <summary>
        /// The animation to play.
        /// </summary>
        public IAnimationController Animation
        {
            get => (IAnimationController) GetValue(AnimationProperty);
            set => SetValue(AnimationProperty, value);
        }

        protected override void OnAttachedTo(VisualElement bindable) =>
            _elements.Add(bindable);

        protected override void OnDetachingFrom(VisualElement bindable) =>
            _elements.Remove(bindable);

        /// <summary>
        /// Plays the <see cref="Animation"/> on the attached visual elements.
        /// </summary>
        /// <returns>Returns a task that finishes when the animation has been played on all elements.</returns>
        public async Task<bool> Play()
        {
            // No animation, stop immediately.
            if (Animation == null)
                return false;

            // Await finishing on all attached elements.
            return (await Task.WhenAll(_elements.Select(element => Animation.Play(element)))).All(result => result);
        }

        /// <summary>
        /// Aborts the <see cref="Animation"/> on all attached visual elements.
        /// </summary>
        public void Abort()
        {
            // No animation, stop immediately.
            if (Animation == null)
                return;

            // Abort all.
            foreach (var element in _elements)
                Animation.Abort(element);
        }
    }
}