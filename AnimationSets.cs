using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xamarin.X247Grad.Animation
{
    /// <summary>
    /// An animation set. Aggregates multiple animation stops and plays them in parallel.
    /// </summary>
    [ContentProperty("Sets")]
    public class AnimationSets : IAnimationController
    {
        /// <summary>
        /// Backing for the set list.
        /// </summary>
        private IList<AnimationSet> _sets;

        /// <summary>
        /// The list of animation sets.
        /// </summary>
        public IList<AnimationSet> Sets => _sets ??= new List<AnimationSet>();

        /// <summary>
        /// Plays all sets in parallel.
        /// </summary>
        /// <param name="element">The element to play on.</param>
        /// <returns>Returns a task that results in true if all animations were fully played.</returns>
        public async Task<bool> Play(VisualElement element)
        {
            return (await Task.WhenAll(Sets.Select(set => set.Play(element)))).All(result => result);
        }

        /// <summary>
        /// Cancels all animations
        /// </summary>
        /// <param name="element">The element to cancel on.</param>
        public void Abort(VisualElement element)
        {
            foreach (var set in Sets)
                set.Abort(element);
        }
    }
}