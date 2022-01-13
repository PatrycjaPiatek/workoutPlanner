using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace workoutPlanner
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExercisePage : ContentPage
    {
        //variable that represents the selected exercise
        public static Exercise selectedExercise = null;
        //list of names, fajnie gdyby byla robiona z zapytania sql
        public static List<string> ListOfNames = new List<string> { "bench press", "dumbbell bent-over row on bench", "hip trust" };
        //public string selectedCategory = "Accessories";
        public string selectedSource = "photo.png";
        public static bool addBool = false;
        public static bool updateBool = false;
        bool deleteBool = false;

        public ExercisePage()
        {
            InitializeComponent();
        }

        //method that shows current data in table
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            exerciseCollectionView.ItemsSource = await App.Database.GetExercisesAsync();
            selectedExercise = null;
            addBool = false;
            updateBool = false;
        }
        //select excercise
        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //wybrane cwiczenie
            selectedExercise = e.CurrentSelection[0] as Exercise;
            //string newName = "";
            //newName = selectedExercise.Name;

            if (AddUpdatePlan.addExerciseToThePlan)
            {
                if (PlanPage.updateBool)
                {
                    AddUpdatePlan.newEx += ';' + selectedExercise.Name;
                }
                if (PlanPage.addBool)
                {
                    AddUpdatePlan.newEx = ';' + selectedExercise.Name;
                }
                

                //////////////  
                AddUpdatePlan.addExerciseToThePlan = false;

                await DisplayAlert("Success", "Exercise added", "OK");
                await Navigation.PushAsync(new AddUpdatePlan());
                //await Navigation.PopAsync();
            }
        }
        //delete excercise
        async private void DeleteClicked(object sender, EventArgs e)
        {
            if (selectedExercise != null)
            {
                deleteBool = await DisplayAlert("Are you sure?", "Exercise will be deleted", "OK", "NO");
                if (deleteBool)
                {
                    //Delete exercise  
                    await App.Database.DeleteExerciseAsync(selectedExercise);
                    await DisplayAlert("Success", "Exercise deleted", "OK");

                    //Get All Exercises  
                    exerciseCollectionView.ItemsSource = await App.Database.GetExercisesAsync();
                }
            }
            else
            {
                await DisplayAlert(":)", "Select excercise first", "OK");
            }
        }

        //adding new exercise
        async private void AddExerciseClicked(object sender, EventArgs e)
        {
            addBool = true;
            await Navigation.PushAsync(new AddUpdateExcercise());
        }
        async private void UpdateClicked(object sender, EventArgs e)
        {
            //updateBool = true;
            if (selectedExercise != null)
            {
                updateBool = true;
                await Navigation.PushAsync(new AddUpdateExcercise());
            }
            else
            {
                await DisplayAlert(":)", "Select excercise first", "OK");
            }
        }

        private async void details_Clicked(object sender, EventArgs e)
        {
            //updateBool = true;
            if (selectedExercise != null)
            {
                await Navigation.PushAsync(new ExcerciseDetails());
            }
            else
            {
                await DisplayAlert(":)", "Select excercise first", "OK");
            }
        }

        private async void Back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        //private void addExerciseToThePlan_Clicked(object sender, EventArgs e)
        //{            
        //    Navigation.PushAsync(new ExercisePage());
        //}
    }
}
