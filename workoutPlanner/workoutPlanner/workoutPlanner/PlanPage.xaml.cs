using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public static Plan selectedItem = null;
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
            //when name isn't empty
            if (!string.IsNullOrWhiteSpace(nameEntry.Text))
            {
                await App.Database.SavePlanAsync(new Plan
                {
                    Name = nameEntry.Text,
                    Details = detailsEntry.Text
                });
                await DisplayAlert("Success", "Plan added", "OK");

                nameEntry.Text = detailsEntry.Text = string.Empty;
                collectionView.ItemsSource = await App.Database.GetPlansAsync();
            }
        }
        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedItem = e.CurrentSelection[0] as Plan;
            //when selected, name and details show in entry
            nameEntry.Text = selectedItem.Name;
            detailsEntry.Text = selectedItem.Details;

            //dla testu
            if (selectedItem.ExercisesInPlan != null)
            {
                test.Text = selectedItem.ExercisesInPlan[0].Category.ToString();
            }
            

            //dla testu, czy dziala dodawanie cwiczen do planu
            //Get All Persons  
            //exerciseCollectionView.ItemsSource = await App.Database.GetPlansAsync();
        }

        async private void DeleteClicked(object sender, EventArgs e)
        {
            if (selectedItem != null)
            {
                //Delete Person  
                await App.Database.DeletePlanAsync(selectedItem);
                await DisplayAlert("Success", "Plan deleted", "OK");

                //Get All Persons  
                collectionView.ItemsSource = await App.Database.GetPlansAsync();
            }
        }

        async private void UpdateClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(nameEntry.Text))
            {
                selectedItem.Name = nameEntry.Text;
                selectedItem.Details = detailsEntry.Text;
                await App.Database.UpdatePlanAsync(selectedItem);
                
                await DisplayAlert("Success", "Plan updated", "OK");

                nameEntry.Text = detailsEntry.Text = string.Empty;
                collectionView.ItemsSource = await App.Database.GetPlansAsync();
            }
        }

        private void addExerciseToThePlan_Clicked(object sender, EventArgs e)
        {            
            addToThePlan = true;
            Navigation.PushAsync(new ExercisePage());
        }
    }
}
