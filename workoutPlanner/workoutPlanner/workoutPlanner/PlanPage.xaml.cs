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
        //variable that represents the selected plan        
        public static Plan selectedPlan = null;
        public static bool updateSelectedPlanBool = false;
        public static bool deleteSelectedPlanBool = false;
        public static bool addNewPlanBool = false;

        public PlanPage()
        {
            InitializeComponent();            
        }

        //method that shows current data in table
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            collectionView.ItemsSource = await App.Database.GetPlansAsync();

            //when data appearing, selectedPlan is null
            selectedPlan = null;
            //when data appearing, update/add bools are false
            updateSelectedPlanBool = false;
            addNewPlanBool = false;
            //when data appearing, every data from AddUpdatePlan are empty
            AddUpdatePlan.SemicoloneveryName = string.Empty;
            AddUpdatePlan.newEx = string.Empty;
            AddUpdatePlan.NamesList.Clear();
        }
 
        void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedPlan = e.CurrentSelection[0] as Plan;
        }

        private async void planDetails_Clicked(object sender, EventArgs e)
        {
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
            if (selectedPlan != null)
            {
                updateSelectedPlanBool = true;
                await Navigation.PushAsync(new AddUpdatePlan());
            }
            else
            {
                await DisplayAlert(":)", "Select a plan first", "OK");
            }
        }
        private async void AddPlan_Clicked(object sender, EventArgs e)
        { 
            selectedPlan = null;
            addNewPlanBool = true;
            await Navigation.PushAsync(new AddUpdatePlan());
        }
        private async void delete_Clicked(object sender, EventArgs e)
        {
            if (selectedPlan != null)
            {
                deleteSelectedPlanBool = await DisplayAlert("Are you sure?", "The plan will be deleted", "OK", "NO");
                if (deleteSelectedPlanBool)
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
                await DisplayAlert(":)", "Select a plan first", "OK");
            }
        }
    }
}
