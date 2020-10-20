using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin.X247Grad.Animation.Examples
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AttachedAnimation : ContentPage
    {
        public AttachedAnimation() =>
            InitializeComponent();

        /// <summary>
        /// Plays the animation defined in the XAML file as a behavior.
        /// </summary>
        private async void PlayAnimation(object sender, EventArgs e) =>
            await AttachAnimation.Play();
    }
}