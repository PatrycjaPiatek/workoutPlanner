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

        public ExercisePage()
        {
            InitializeComponent();
        }
        //async private void Add()
        //{
        //    await App.Database.SaveExerciseAsync(new Exercise
        //    {
        //        Name = "bench press",
        //        Category = "chest",
        //        Img = "benchPress.png"

        //    });            
            
        //    exerciseCollectionView.ItemsSource = await App.Database.GetExercisesAsync();
        //}

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
                //different names
                foreach(string n in ListOfNames)
                {
                    if (nameEntry.Text == n)
                    {
                        nameEntry.Text += "1";
                    }
                }
                //if (selectedSource.Equals("photo.png"))
                //{
                //    await DisplayAlert("", "Please pick an image first", "ok");
                //}
                //else { }
                await App.Database.SaveExerciseAsync(new Exercise
                {
                    Name = nameEntry.Text,
                    //kategoria wybierana z listy
                    Category = categoryEntry.Text,
                    Img = selectedSource

                });
                await DisplayAlert("Success", "Exercise added", "OK");

                resultImage.Source = "photo.png";
                selectedSource = "photo.png";
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

            await Navigation.PushAsync(new ExcerciseDetails());

            if (PlanPage.addToThePlan)
            {
                //JEDEN PLAN SPOSÓB
                var n = new Name
                {
                    ExcerciseName = selectedExercise.Name
                };
                //albo jedno albo drugie
                App.Database.SaveNameAsync(n);
                //App.Database.UpdateNameAsync(n);

                //stary sposob
                //PlanPage.selectedItem.ExercisesInPlan = new List<Exercise> { selectedExercise };

                ////nowy sposob
                //PlanPage.selectedItem.ExercisesInPlan = new List<Exercise> { };
                //PlanPage.selectedItem.ExercisesInPlan.Add(selectedExercise);

                //plan1.ExercisesInPlan = new List<Exercise> { };
                //plan1.ExercisesInPlan.Add(exercise1);
                ////plan1.ExercisesInPlan = new List<Exercise> { exercise1 };            
                //_dbdb.UpdateAsync(plan1);

                ////Update Plan  
                //await App.Database.UpdateNameAsync(PlanPage.selectedItem);

                //moze dac tak jeszcze
                //App.Database._
                //var personStored = App.Database.GetPlansAsync();
                //var eventStored = App.Database.GetExercisesAsync();
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
                selectedExercise.Img = selectedSource;
                selectedExercise.Details = detailsEntry.Text;
                await App.Database.UpdateExerciseAsync(selectedExercise);

                await DisplayAlert("Success", "Exercise updated", "OK");

                nameEntry.Text = categoryEntry.Text = detailsEntry.Text = string.Empty;
                exerciseCollectionView.ItemsSource = await App.Database.GetExercisesAsync();
            }
        }

        private async void pickImg_Clicked_1(object sender, EventArgs e)
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Please pick a photo"
            });

            if (result != null)
            {
                var stream = await result.OpenReadAsync();
                resultImage.Source = ImageSource.FromStream(() => stream);
                selectedSource = result.FullPath;
                //String myPath = result.FullPath;
                //lblText.Text = myPath;
                //pickedImage.Source = myPath;
            }
        }

        //private void addExerciseToThePlan_Clicked(object sender, EventArgs e)
        //{            
        //    Navigation.PushAsync(new ExercisePage());
        //}
    }
}
