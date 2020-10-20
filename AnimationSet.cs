using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Xaml.Internals;

namespace Xamarin.X247Grad.Animation
{
    /// <summary>
    /// An animation set. Aggregates multiple animation stops and plays them in sequence.
    /// </summary>
    [ContentProperty("Stops")]
    public class AnimationSet : IAnimationController
    {
        /// <summary>
        /// Assigned names per visual element for animation cancelling.
        /// </summary>
        private readonly ConditionalWeakTable<VisualElement, string> _names =
            new ConditionalWeakTable<VisualElement, string>();

        /// <summary>
        /// Backing for the stop list.
        /// </summary>
        private IList<AnimationStop> _stops;

        /// <summary>
        /// The list of animation stops with setters and timings.
        /// </summary>
        public IList<AnimationStop> Stops => _stops ??= new List<AnimationStop>();

        /// <summary>
        /// The easing of the animation, if no stop easing is given.
        /// </summary>
        public Easing Easing { get; set; }

        /// <summary>
        /// Plays this animation on the given element.
        /// </summary>
        /// <param name="element">The element to play on.</param>
        /// <returns>Returns a task that results in true if the animation was fully played.</returns>
        public async Task<bool> Play(VisualElement element)
        {
            try
            {
                // Preemptively abort this animation.
                Abort(element);

                // Add new name for the animation as a handle.
                var name = $"stop-animation-{Guid.NewGuid()}";
                _names.Add(element, name);

                // Service provider value when needed.
                var serviceProvider = new XamlServiceProvider();

                // Get target map for dedicated elements and optionally pre-initialize setters.
                var targets = new Dictionary<Setter, BindableObject>();
                foreach (var stop in Stops)
                foreach (var setter in stop.AllSetters)
                {
                    // If target name is present, get in the given element, otherwise use the root.
                    if (setter.TargetName != null)
                        targets.Add(setter, element.FindByName(setter.TargetName) as BindableObject);
                    else
                        targets.Add(setter, element);

                    // Initialize value conversion.
                    ((IValueProvider) setter).ProvideValue(serviceProvider);
                }


                // List of compiled assignments, computed for each stop.
                var compiled = new List<Action<double>>();

                // Animate all stops.
                foreach (var stop in Stops)
                {
                    // If no setters and non-empty length, delay and skip stop.
                    if (!stop.Setters.Any() && 0 < stop.Length)
                    {
                        await Task.Delay((int) stop.Length);
                        continue;
                    }

                    // Reset compiled list.
                    compiled.Clear();

                    // Compile all setters.
                    foreach (var setter in stop.AllSetters)
                    {
                        // Get target object.
                        var target = targets[setter];

                        // Get source and target value.
                        var from = target.GetValue(setter.Property);
                        var to = setter.Value;

                        // Decide based on type.
                        if (from is int fromInt && to is int toInt)
                            compiled.Add(x => target.SetValue(setter.Property, Interpolate.Linear(fromInt, toInt, x)));
                        else if (from is float fromFloat && to is float toFloat)
                            compiled.Add(x =>
                                target.SetValue(setter.Property, Interpolate.Linear(fromFloat, toFloat, x)));
                        else if (from is double fromDouble && to is double toDouble)
                            compiled.Add(x =>
                                target.SetValue(setter.Property, Interpolate.Linear(fromDouble, toDouble, x)));
                        else if (from is Thickness fromThickness && to is Thickness toThickness)
                            compiled.Add(x =>
                                target.SetValue(setter.Property, Interpolate.Linear(fromThickness, toThickness, x)));
                        else if (from is Size fromSize && to is Size toSize)
                            compiled.Add(x =>
                                target.SetValue(setter.Property, Interpolate.Linear(fromSize, toSize, x)));
                        else if (from is Color fromColor && to is Color toColor)
                            compiled.Add(x =>
                                target.SetValue(setter.Property, Interpolate.Linear(fromColor, toColor, x)));
                        else
                            compiled.Add(x => target.SetValue(setter.Property, x > 0.0 ? to : from));
                    }

                    // Check if empty stop.
                    if (stop.Length == 0U)
                    {
                        // Invoke all compiled interpolation actions.
                        foreach (var action in compiled)
                            action(1.0);

                        // Skip animation.
                        continue;
                    }

                    // Create stop completion for task integration.
                    var stopWasCancelled = new TaskCompletionSource<bool>();

                    // Animate this stop.
                    element.Animate(name, x =>
                    {
                        // Invoke all compiled interpolation actions.
                        foreach (var action in compiled)
                            action(x);
                    }, stop.Rate, stop.Length, stop.Easing ?? Easing, (unused, cancelled) =>
                    {
                        // On completion, set result.
                        stopWasCancelled.SetResult(cancelled);
                    });

                    // If stop was cancelled, return here.
                    if (await stopWasCancelled.Task)
                        return false;
                }

                // Fully animated, return true.
                return true;
            }
            finally
            {
                // Always clear the handle.
                _names.Remove(element);
            }
        }

        /// <summary>
        /// Cancels this animation set on the given element.
        /// </summary>
        /// <param name="element">The element to cancel on.</param>
        public void Abort(VisualElement element)
        {
            // Get name of animation, if it is playing, remove it.
            if (!_names.TryGetValue(element, out var name))
                return;

            // Remove animation and abort it.
            _names.Remove(element);
            element.AbortAnimation(name);
        }
    }
}