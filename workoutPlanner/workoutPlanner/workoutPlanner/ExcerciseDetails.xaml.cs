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
        //
        public string selectedSource = "photo.png";
        public ExcerciseDetails()
        {
            InitializeComponent();
            excerciseImg.Source = ExercisePage.selectedExercise.Img;
            excerciseName.Text = ExercisePage.selectedExercise.Name;
            excerciseCategory.Text = ExercisePage.selectedExercise.Category;
            excerciseDetails.Text = ExercisePage.selectedExercise.Details;
        }

        private async void pickImg_Clicked(object sender, EventArgs e)
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Please pick a photo"
            });

            if (result != null)
            {
                var stream = await result.OpenReadAsync();
                excerciseImg.Source = ImageSource.FromStream(() => stream);
                selectedSource = result.FullPath;
                //String myPath = result.FullPath;
                //lblText.Text = myPath;
                //pickedImage.Source = myPath;
            }
        }

        private async void update_Clicked(object sender, EventArgs e)
        {
            //when name isn't empty
            if (!string.IsNullOrWhiteSpace(excerciseName.Text))
            {
                //different names
                foreach (string n in ExercisePage.ListOfNames)
                {
                    if (excerciseName.Text == n && excerciseName.Text != ExercisePage.selectedExercise.Name)
                    {
                        excerciseName.Text += "1";
                    }
                }
                //if (selectedSource.Equals("photo.png"))
                //{
                //    await DisplayAlert("", "Please pick an image first", "ok");
                //}
                //else { }
                await App.Database.UpdateExerciseAsync(new Exercise
                {
                    Name = excerciseName.Text,
                    //kategoria wybierana z listy
                    Category = excerciseName.Text,
                    Img = selectedSource

                });
                await DisplayAlert("Success", "Exercise updated", "OK");

                //resultImage.Source = "photo.png";
                selectedSource = "photo.png";
                //excerciseName.Text = categoryEntry.Text = string.Empty;
                //ExercisePage.exerciseCollectionView.ItemsSource = await App.Database.GetExercisesAsync();
            }
        }
    }
}