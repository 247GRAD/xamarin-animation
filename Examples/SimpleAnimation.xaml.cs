using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin.X247Grad.Animation.Examples
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SimpleAnimation : ContentPage
    {
        public SimpleAnimation() =>
            InitializeComponent();

        /// <summary>
        /// Plays the animation defined in the XAML file at resource dictionary level.
        /// </summary>
        private async void PlayAnimation(object sender, EventArgs e) =>
            await Animation.Play(Label);
    }
}