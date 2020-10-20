using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin.X247Grad.Animation.Examples
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ParallelAnimation : ContentPage
    {
        public ParallelAnimation() =>
            InitializeComponent();

        /// <summary>
        /// Plays the animation defined in the XAML file at resource dictionary level.
        /// </summary>
        private async void PlayAnimations(object sender, EventArgs e) =>
            await Animations.Play(Label);
    }
}