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
    public partial class AddUpdateExcercise : ContentPage
    {
        public string selectedSource = "photo.png";        
        public string selectedCategory = "";
        public AddUpdateExcercise()
        {
            InitializeComponent();

            defaultImage.Source = "photo.png";

            MainPicker.Items.Add("Arms");
            MainPicker.Items.Add("Chest");
            MainPicker.Items.Add("Lower body");
            MainPicker.Items.Add("Abs");
            MainPicker.Items.Add("Legs");
            MainPicker.Items.Add("Other");
            MainPicker.Items.Add("Back");
            MainPicker.SelectedItem = "Other";

            if (ExercisePage.updateBool)
            {
                defaultImage.Source = ExercisePage.selectedExercise.Img;
                nameEntry.Text = ExercisePage.selectedExercise.Name;
                MainPicker.SelectedItem = ExercisePage.selectedExercise.Category;
                detailsEntry.Text = ExercisePage.selectedExercise.Details;
            }
        }

        private async void saveBtn_Clicked(object sender, EventArgs e)
        {
            if (ExercisePage.addBool)
            {
                //when name isn't empty
                if (!string.IsNullOrWhiteSpace(nameEntry.Text))
                {
                    ////different names
                    //foreach (string n in ListOfNames)
                    //{
                    //    if (nameEntry.Text == n)
                    //    {
                    //        nameEntry.Text += "1";
                    //    }
                    //}
                    //if (selectedSource.Equals("photo.png"))
                    //{
                    //    await DisplayAlert("", "Please pick an image first", "ok");
                    //}
                    //else { }
                    
                    await App.Database.SaveExerciseAsync(new Exercise
                    {
                        Name = nameEntry.Text,
                        //kategoria wybierana z listy moze 
                        Category = selectedCategory,
                        Details = detailsEntry.Text,
                        Img = selectedSource

                    });
                    await DisplayAlert("Success", "Exercise added", "OK");

                    //defaultImage.Source = "photo.png";
                    selectedSource = "";
                    //categoryEntry.Text = string.Empty;
                    nameEntry.Text = detailsEntry.Text = string.Empty;
                    ExercisePage.addBool = false; 
                    await Navigation.PushAsync(new ExercisePage());
                }
                else
                {
                    await DisplayAlert("", "Please enter a name first", "ok");
                }
            }

            if (ExercisePage.updateBool)
            {
                if (!string.IsNullOrWhiteSpace(nameEntry.Text))
                {
                    if(selectedSource != "photo.png")
                    {
                        ExercisePage.selectedExercise.Img = selectedSource;
                    }                 
                    ExercisePage.selectedExercise.Name = nameEntry.Text;
                    ExercisePage.selectedExercise.Category = selectedCategory;
                    ExercisePage.selectedExercise.Details = detailsEntry.Text;

                    await App.Database.UpdateExerciseAsync(ExercisePage.selectedExercise);
                    await DisplayAlert("Success", "Exercise updated", "OK");
                }
                //defaultImage.Source = "photo.png";
                selectedSource = "";
                //categoryEntry.Text = "";
                nameEntry.Text = detailsEntry.Text = "";
                ExercisePage.updateBool = false;
                await Navigation.PushAsync(new ExercisePage());
            }
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
                defaultImage.Source = ImageSource.FromStream(() => stream);
                selectedSource = result.FullPath;
            }
        }

        private void MainPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedCategory = MainPicker.Items[MainPicker.SelectedIndex];
        }
    }
}