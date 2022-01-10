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
            excerciseImg.Source = ExercisePage.selectedExercise.Img;
            excerciseName.Text = ExercisePage.selectedExercise.Name;
            excerciseCategory.Text = ExercisePage.selectedExercise.Category;
            excerciseDetails.Text = ExercisePage.selectedExercise.Details;
        }
    }
}