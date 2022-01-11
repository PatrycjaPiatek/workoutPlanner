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
        public static string newEx = "";       
        public static string selectedExcerciseFromThePlan = null;
        private bool deleteExerciseFromThePlan = false;
        //allows to execute appropriate code from excercisePage.cs
        public static bool addExerciseToThePlan = false;
        //public static bool add = false;
        public AddUpdatePlan()
        {
            InitializeComponent();

            if (PlanPage.addNewPlanBool)
            {                
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

            if (PlanPage.updateSelectedPlanBool)
            {
                //if (add)
                //{
                //    PlanPage.selectedPlan = ExercisePage.sP;
                //    add = false;
                //}
                PlanPage.addNewPlanBool = false;
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
        //protected override async void OnAppearing()
        //{
        //    base.OnAppearing();
            
        //}
        private async void saveBtn_Clicked(object sender, EventArgs e)
        {
            if (PlanPage.addNewPlanBool)
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
                    PlanPage.updateSelectedPlanBool = false;
                    PlanPage.addNewPlanBool = false;
                    await Navigation.PushAsync(new PlanPage());
                }
                else
                {
                    await DisplayAlert("", "Please enter a name first", "ok");
                }
            }

            //plan name const?
            if (PlanPage.updateSelectedPlanBool)
            {
                if (!string.IsNullOrWhiteSpace(planName.Text))
                {
                    PlanPage.selectedPlan.NamePlan = planName.Text;
                    //string names = "";

                    PlanPage.selectedPlan.ListOfExcercisesName = SemicoloneveryName;

                    await App.Database.UpdatePlanAsync(PlanPage.selectedPlan);
                    await DisplayAlert("Success", "Plan updated", "OK");
                }

                //clear the data
                planName.Text = "";
                SemicoloneveryName = "";
                newEx = "";
                NamesList.Clear();
                PlanPage.updateSelectedPlanBool = false;
                PlanPage.addNewPlanBool = false;
                await Navigation.PushAsync(new PlanPage());
            }
        }
        private async void addNewExcerciseBtn_Clicked(object sender, EventArgs e)
        {
            addExerciseToThePlan = true;
            //add = true;
            await Navigation.PushAsync(new ExercisePage());
        }
        private async void deleteNewExcerciseBtn_Clicked(object sender, EventArgs e)
        {
            if (selectedExcerciseFromThePlan != null)
            {
                addExerciseToThePlan = false;
                deleteExerciseFromThePlan = await DisplayAlert("Are you sure?", "Exercise will be deleted", "OK", "NO");
                if (deleteExerciseFromThePlan)
                {
                    //test
                    string lt = "";
                    test.Text = selectedExcerciseFromThePlan; //1. Dmb
                    for (int i = 0; i < NamesList.Count; i++)
                    {
                        lt+= NamesList[i];
                    }

                    listTest.Text = lt; //1. Dmb2. Hip
                    // test

                    //Delete exercise
                    //selectedExcerciseFromThePlan = selectedExcerciseFromThePlan.Substring(3);
                    //selectedExcerciseFromThePlan = ';' + selectedExcerciseFromThePlan;
                    if (NamesList.Contains(selectedExcerciseFromThePlan)){
                        //test.Text = selectedExcerciseFromThePlan;
                        NamesList.Remove(selectedExcerciseFromThePlan);
                    }

                    if (NamesList.Count()>=1)
                    {
                        SemicoloneveryName = NamesList[0].Substring(3);
                        for (int i = 1; i < NamesList.Count; i++)
                        {
                            SemicoloneveryName += ';' + NamesList[i].Substring(3);
                        }
                    }
                    if (NamesList.Count() == 0)
                    {
                        SemicoloneveryName = "";
                    }
                    //List<string> NL = new List<string> {};
                    //NL = NamesList;
                    //myList.ItemsSource = NL;
                    
                    myList.ItemsSource = NamesList;
                    //selectedExcerciseFromThePlan = "deleted";

                    //im lost 
                    if (PlanPage.selectedPlan!=null)
                    {
                        PlanPage.selectedPlan.ListOfExcercisesName = SemicoloneveryName;
                        await App.Database.UpdatePlanAsync(PlanPage.selectedPlan);
                        //await App.Database.UpdatePlanAsync(PlanPage.selectedPlan);
                        //myList.ItemsSource = NamesList;
                    }
                    //else { }
                    await DisplayAlert("Success", "Exercise deleted from the plan, NO click \"save\" to save changes", "OK");
                    
                    await Navigation.PushAsync(new AddUpdatePlan());
                    
                    //Get All Exercises  
                    //myList.ItemsSource = NamesList;
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
            selectedExcerciseFromThePlan = e.CurrentSelection[0] as string;            
        }

        private async void refreshBtn_Clicked(object sender, EventArgs e)
        {
            
            await Navigation.PushAsync(new AddUpdatePlan());
        }
    }
}