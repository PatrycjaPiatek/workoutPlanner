using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace workoutPlanner
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void btnExercises_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Exercises());
        }

        private void btnPlans_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Plans());
        }
    }
}
