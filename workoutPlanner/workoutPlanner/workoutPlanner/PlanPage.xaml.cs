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
    public partial class PlanPage : ContentPage
    {
        //variable uded to add exercisees to the plan
        public static bool addToThePlan = false;

        //variable that represents the selected plan
        public static Plan selectedPlan = null;

        public static bool newPlanBool = false;
        public PlanPage()
        {
            InitializeComponent();
        }

        //method that shows current data in table
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            collectionView.ItemsSource = await App.Database.GetPlansAsync();
        }

        //adding new plan
        async void AddPlanClicked(object sender, EventArgs e)
        {
            newPlanBool = await DisplayAlert("Are you sure?", "This plan must be archived for a new plan to be created", "OK", "NO");
            if (newPlanBool)
            {
                Navigation.PushAsync(new ExercisePage());
            }
            ////when name isn't empty
            //if (!string.IsNullOrWhiteSpace(nameEntry.Text))
            //{
            //    await App.Database.SaveNameAsync(new Plan
            //    {
            //        ListOfExcercisesName = nameEntry.Text,
            //        Details = detailsEntry.Text
            //    });
            //    await DisplayAlert("Success", "Plan added", "OK");

            //    nameEntry.Text = detailsEntry.Text = string.Empty;
            //    collectionView.ItemsSource = await App.Database.GetPlansAsync();
            //}
        }
        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedPlan = e.CurrentSelection[0] as Plan;
            ////when selected, name and details show in entry
            //nameEntry.Text = selectedPlan.ListOfExcercisesName;
            //detailsEntry.Text = selectedPlan.Details;

            ////dla testu
            //if (selectedPlan.ExercisesInPlan != null)
            //{
            //    test.Text = selectedPlan.ExercisesInPlan[0].Category.ToString();
            //}
            //var databasePath = Path.Combine(FileSystem.AppDataDirectory, "_dbdb.db");

            //Console.WriteLine(databasePath);
            //test.Text = databasePath;
            ////C:\Users\pipat\AppData\Local\Packages\9edf7a7b-9cf2-4878-9e67-59ad451d441a_0f9wbgk7cg3pc\LocalState\_dbdb.db
            ////C:\Users\pipat\AppData\Local\Packages\9edf7a7b-9cf2-4878-9e67-59ad451d441a_0f9wbgk7cg3pc\LocalState\_dbdb.db
            ////dla testu, czy dziala dodawanie cwiczen do planu
            ////Get All Persons  
            ////exerciseCollectionView.ItemsSource = await App.Database.GetPlansAsync();
        }

        async private void DeleteClicked(object sender, EventArgs e)
        {
            if (selectedPlan != null)
            {
                //Delete Person  
                await App.Database.DeletePlanAsync(selectedPlan);
                await DisplayAlert("Success", "Plan deleted", "OK");

                //Get All Persons  
                //collectionView.ItemsSource = await App.Database.GetPlansAsync();
            }
        }

        async private void UpdateClicked(object sender, EventArgs e)
        {
            //if (!string.IsNullOrWhiteSpace(nameEntry.Text))
            //{
            //    selectedPlan.ListOfExcercisesName = nameEntry.Text;
            //    selectedPlan.Details = detailsEntry.Text;
            //    await App.Database.UpdateNameAsync(selectedPlan);
                
            //    await DisplayAlert("Success", "Plan updated", "OK");

            //    nameEntry.Text = detailsEntry.Text = string.Empty;
            //    collectionView.ItemsSource = await App.Database.GetPlansAsync();
            //}
        }

        private void addExerciseToThePlan_Clicked(object sender, EventArgs e)
        {
            addToThePlan = true;
            Navigation.PushAsync(new ExercisePage());
        }


        private async void planDetails_Clicked(object sender, EventArgs e)
        {
            //updateBool = true;
            if (selectedPlan != null)
            {
                await Navigation.PushAsync(new PlanDetails());
            }
            else
            {
                await DisplayAlert(":)", "Select plan first", "OK");
            }
        }
    }
}
