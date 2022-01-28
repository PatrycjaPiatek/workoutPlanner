using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace workoutPlanner
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExcerciseDetails : ContentPage
    {
        //public string selectedSource = "photo.png";
        public ExcerciseDetails()
        {
            InitializeComponent();
            //if (PlanDetails.exName)
            //{

            //    PlanDetails.exName = false;
            //}
            excerciseImg.Source = ExercisePage.selectedExercise.Img;
            excerciseName.Text = ExercisePage.selectedExercise.Name;
            excerciseCategory.Text = ExercisePage.selectedExercise.Category;
            excerciseDetails.Text = ExercisePage.selectedExercise.Details;
        }
        protected override bool OnBackButtonPressed()
        {
            BackToMainMenu();
            return true;
        }

        private async void BackToMainMenu()
        {
            //oproznia stos
            var existingPages = Navigation.NavigationStack.ToList();
            for (int i = 0; i < existingPages.Count - 1; i++)
            {
                Navigation.RemovePage(existingPages[i]);
            }
            await Navigation.PushAsync(new ExercisePage());
        }
    }
}