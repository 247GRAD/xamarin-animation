using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xamarin.X247Grad.Animation
{
    /// <summary>
    /// Base for playable animations. See implementations at <see cref="AnimationSet"/> and <see cref="AnimationSets"/>.
    /// </summary>
    public interface IAnimationController
    {
        /// <summary>
        /// Plays the animation on the element.
        /// </summary>
        /// <param name="element">The visual element to play the animation on.</param>
        /// <returns>Returns a task finishing with the result of playing the animation.</returns>
        Task<bool> Play(VisualElement element);


        /// <summary>
        /// Aborts the animation on the element.
        /// </summary>
        /// <param name="element">The visual element to abort the animation on.</param>
        void Abort(VisualElement element);
    }
}