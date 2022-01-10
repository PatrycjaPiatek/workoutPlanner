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
    public partial class AddUpdatePlan : ContentPage
    {
        public static List<string> NamesList = new List<string> {};
        public static string SemicoloneveryName = "";
        //allows to execute appropriate code from excercisePage.cs
        public static bool addExerciseToThePlan = false;
        public static string newEx = "";
        public static string selectedItem = null;
        private bool deleteBool = false;
        public AddUpdatePlan()
        {
            InitializeComponent();
            //SemicoloneveryName = "";
            if (PlanPage.addBool)
            {                
                PlanPage.updateBool = false;
                planName.Text = "";
                planID.Text = "";

                SemicoloneveryName += newEx;

                //if first ex is added by user
                if (SemicoloneveryName != "")
                {
                    if (SemicoloneveryName[0] == ';')
                    {
                        SemicoloneveryName = SemicoloneveryName.Substring(1);
                    }
                }

                NamesList = SemicoloneveryName.Split(';').ToList();
                for (int i = 0; i < NamesList.Count(); i++)
                {
                    NamesList[i] = (i + 1).ToString() + ". " + NamesList[i];
                }
                myList.ItemsSource = NamesList;
            }

            if (PlanPage.updateBool)
            {
                PlanPage.addBool = false;
                planName.Text = PlanPage.selectedPlan.NamePlan;
                planID.Text = PlanPage.selectedPlan.IDPlan.ToString();

                SemicoloneveryName = PlanPage.selectedPlan.ListOfExcercisesName + newEx;

                //if first ex is added by user
                if (SemicoloneveryName != "")
                {
                    if (SemicoloneveryName[0] == ';')
                    {
                        SemicoloneveryName = SemicoloneveryName.Substring(1);
                    }
                }

                NamesList = SemicoloneveryName.Split(';').ToList();
                for (int i = 0; i < NamesList.Count(); i++)
                {
                    NamesList[i] = (i + 1).ToString() + ". " + NamesList[i];
                }
                myList.ItemsSource = NamesList;
            }
        }
        private async void saveBtn_Clicked(object sender, EventArgs e)
        {
            if (PlanPage.addBool)
            {
                //when name isn't empty
                if (!string.IsNullOrWhiteSpace(planName.Text))
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

                    await App.Database.SavePlanAsync(new Plan
                    {
                        NamePlan = planName.Text,
                        ListOfExcercisesName = SemicoloneveryName
                    //kategoria wybierana z listy moze
                    });
                    await DisplayAlert("Success", "Plan added", "OK");

                    //clear the data
                    planName.Text = "";
                    SemicoloneveryName = "";
                    newEx = "";
                    NamesList.Clear();
                    PlanPage.updateBool = false;
                    PlanPage.addBool = false;
                    await Navigation.PushAsync(new PlanPage());
                }
                else
                {
                    await DisplayAlert("", "Please enter a name first", "ok");
                }
            }

            //plan name const?
            if (PlanPage.updateBool)
            {
                if (!string.IsNullOrWhiteSpace(planName.Text))
                {
                    PlanPage.selectedPlan.NamePlan = planName.Text;
                    //string names = "";

                    PlanPage.selectedPlan.ListOfExcercisesName = SemicoloneveryName;

                    await App.Database.UpdatePlanAsync(PlanPage.selectedPlan);
                    await DisplayAlert("Success", "Plan updated", "OK");
                }
                //defaultImage.Source = "photo.png";
                //selectedSource = "";
                //categoryEntry.Text = "";

                //clear the data
                planName.Text = "";
                SemicoloneveryName = "";
                newEx = "";
                NamesList.Clear();
                PlanPage.updateBool = false;
                PlanPage.addBool = false;
                await Navigation.PushAsync(new PlanPage());
            }
        }
        private async void addNewExcerciseBtn_Clicked(object sender, EventArgs e)
        {
            addExerciseToThePlan = true;
            await Navigation.PushAsync(new ExercisePage());
        }
        private async void deleteNewExcerciseBtn_Clicked(object sender, EventArgs e)
        {
            if (selectedItem != null)
            {
                deleteBool = await DisplayAlert("Are you sure?", "Exercise will be deleted", "OK", "NO");
                if (deleteBool)
                {
                    //Delete exercise
                    //selectedItem = selectedItem.Substring(3);
                    //selectedItem = ';' + selectedItem;
                    if (NamesList.Contains("selectedItem")){
                        NamesList.Remove("selectedItem");
                    }

                    SemicoloneveryName = NamesList[0].Substring(3);
                    for (int i = 1; i < NamesList.Count; i++)
                    {                        
                        SemicoloneveryName += ';' + NamesList[i].Substring(3);
                    }

                    //im lost 
                    PlanPage.selectedPlan.ListOfExcercisesName = SemicoloneveryName;
                    await App.Database.UpdatePlanAsync(PlanPage.selectedPlan);
                    await DisplayAlert("Success", "Exercise deleted", "OK");

                    //Get All Exercises  
                    myList.ItemsSource = NamesList;
                }
            }
            else
            {
                await DisplayAlert(":)", "Select excercise first", "OK");
            }

            //await Navigation.PushAsync(new PlanPage());
        }

        private void myList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedItem = e.CurrentSelection[0] as string;
        }
    }
}