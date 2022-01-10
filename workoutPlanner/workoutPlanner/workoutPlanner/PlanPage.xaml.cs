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

        //do usuniecia
        public static bool newPlanBool = false;

        public static bool updateBool = false;

        public static bool deleteBool = false;

        public static bool addBool = false;

        public PlanPage()
        {
            InitializeComponent();
            AddUpdatePlan.SemicoloneveryName = string.Empty;
            AddUpdatePlan.newEx = string.Empty;
            AddUpdatePlan.NamesList.Clear();
            updateBool = false;
            addBool = false;
            selectedPlan = null;
        }

        //method that shows current data in table
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            collectionView.ItemsSource = await App.Database.GetPlansAsync();
        }

        ////adding new plan
        //async void AddPlanClicked(object sender, EventArgs e)
        //{
        //    //newPlanBool = await DisplayAlert("Are you sure?", "This plan must be archived for a new plan to be created", "OK", "NO");
        //    //if (newPlanBool)
        //    //{
        //    //    Navigation.PushAsync(new ExercisePage());
        //    //}
        //    ////when name isn't empty
        //    //if (!string.IsNullOrWhiteSpace(nameEntry.Text))
        //    //{
        //    //    await App.Database.SavePlanAsync(new Plan
        //    //    {
        //    //        ListOfExcercisesName = nameEntry.Text,
        //    //        Details = detailsEntry.Text
        //    //    });
        //    //    await DisplayAlert("Success", "Plan added", "OK");

        //    //    nameEntry.Text = detailsEntry.Text = string.Empty;
        //    //    collectionView.ItemsSource = await App.Database.GetPlansAsync();
        //    //}
        //}
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
        //do usuniecia
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

        private async void update_Clicked(object sender, EventArgs e)
        {
            //updateBool = true;
            if (selectedPlan != null)
            {
                updateBool = true;
                await Navigation.PushAsync(new AddUpdatePlan());
            }
            else
            {
                await DisplayAlert(":)", "Select plan first", "OK");
            }
        }

        private async void delete_Clicked(object sender, EventArgs e)
        {
            if (selectedPlan != null)
            {
                deleteBool = await DisplayAlert("Are you sure?", "Exercise will be deleted", "OK", "NO");
                if (deleteBool)
                {
                    //Delete exercise  
                    await App.Database.DeletePlanAsync(selectedPlan);
                    await DisplayAlert("Success", "Plan deleted", "OK");

                    //Get All Exercises  
                    collectionView.ItemsSource = await App.Database.GetPlansAsync();
                }
            }
            else
            {
                await DisplayAlert(":)", "Select excercise first", "OK");
            }
        }

        private async void AddPlan_Clicked(object sender, EventArgs e)
        {
            selectedPlan = null;
            AddUpdatePlan.SemicoloneveryName = String.Empty;
            addBool = true;
            await Navigation.PushAsync(new AddUpdatePlan());
        }
    }
}
