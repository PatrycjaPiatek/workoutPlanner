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
        public ExercisePage()
        {
            InitializeComponent();
        }
        async private void Add()
        {
            await App.Database.SaveExerciseAsync(new Exercise
            {
                Name = "bench press",
                Category = "chest",
                Img = "benchPress.png"

            });            
            
            exerciseCollectionView.ItemsSource = await App.Database.GetExercisesAsync();
        }

        //method that shows current data in table
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            exerciseCollectionView.ItemsSource = await App.Database.GetExercisesAsync();
        }

        //adding new exercise
        async void AddExerciseClicked(object sender, EventArgs e)
        {
            //when name isn't empty
            if (!string.IsNullOrWhiteSpace(nameEntry.Text))
            {
                await App.Database.SaveExerciseAsync(new Exercise
                {
                    Name = nameEntry.Text,
                    Category = categoryEntry.Text
                });
                await DisplayAlert("Success", "Exercise added", "OK");

                nameEntry.Text = categoryEntry.Text = string.Empty;
                exerciseCollectionView.ItemsSource = await App.Database.GetExercisesAsync();
            }
        }
        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            //wybrane cwiczenie
            selectedExercise = e.CurrentSelection[0] as Exercise;
            nameEntry.Text = selectedExercise.Name;
            categoryEntry.Text = selectedExercise.Category;
            //Navigation.PushAsync(new PlanPage());

            if (PlanPage.addToThePlan)
            {
                PlanPage.selectedItem.ExercisesInPlan = new List<Exercise> { selectedExercise };

                //Update Plan  
                await App.Database.UpdatePlanAsync(PlanPage.selectedItem);
                //////////////  
                PlanPage.addToThePlan = false;

                //person1.ExercisesInPlan = new List<Event> { event1 };
                //db.UpdateWithChildren(person1);
                await DisplayAlert("Success", "Exercise added", "OK");
                await Navigation.PushAsync(new PlanPage());
            }
        }

        async private void DeleteClicked(object sender, EventArgs e)
        {
            if (selectedExercise != null)
            {
                //Delete exercise  
                await App.Database.DeleteExerciseAsync(selectedExercise);
                await DisplayAlert("Success", "Exercise deleted", "OK");

                //Get All Exercises  
                exerciseCollectionView.ItemsSource = await App.Database.GetExercisesAsync();
            }
        }

        async private void UpdateClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(nameEntry.Text))
            {
                selectedExercise.Name = nameEntry.Text;
                selectedExercise.Category = categoryEntry.Text;
                await App.Database.UpdateExerciseAsync(selectedExercise);

                await DisplayAlert("Success", "Exercise updated", "OK");

                nameEntry.Text = categoryEntry.Text = string.Empty;
                exerciseCollectionView.ItemsSource = await App.Database.GetExercisesAsync();
            }
        }

        //private void addExerciseToThePlan_Clicked(object sender, EventArgs e)
        //{            
        //    Navigation.PushAsync(new ExercisePage());
        //}
    }
}
