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
        public static string planNameStatic;
        public AddUpdatePlan()
        {
            InitializeComponent();

            if (PlanPage.addNewPlanBool)
            {
                planName.Text = planNameStatic;
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
                //planNameStatic = PlanPage.selectedPlan.NamePlan;
                PlanPage.addNewPlanBool = false;
                planName.Text = planNameStatic;
                //planName.Text = PlanPage.selectedPlan.NamePlan;
                //planID.Text = PlanPage.selectedPlan.IDPlan.ToString();

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
        protected override bool OnBackButtonPressed()
        {
            BackToMainMenu();
            return true;
        }

        private async void BackToMainMenu()
        {
            //oproznia stos
            var existingPages = Navigation.NavigationStack.ToList();
            for (int i = 0; i < existingPages.Count - 1; i++)
            {
                Navigation.RemovePage(existingPages[i]);
            }

            bool sure = await DisplayAlert("Are you sure?", "Changes will not be saved ", "OK", "NO");
            if (sure)
            {
                await Navigation.PushAsync(new PlanPage());
            }            
        }
        private async void saveBtn_Clicked(object sender, EventArgs e)
        {
            addExerciseToThePlan = false;
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

                    AddUpdatePlan.planNameStatic = "";

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
                    //AddUpdatePlan.planName.Text = AddUpdatePlan.planNameStatic;
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

                AddUpdatePlan.planNameStatic = "";

                await Navigation.PushAsync(new PlanPage());
            }
        }
        private async void addNewExcerciseBtn_Clicked(object sender, EventArgs e)
        {
            planNameStatic = planName.Text;
            addExerciseToThePlan = true;
            await Navigation.PushAsync(new ExercisePage());
        }
        private async void deleteNewExcerciseBtn_Clicked(object sender, EventArgs e)
        {
            if (addExerciseToThePlan)
            {
                await DisplayAlert(":)", "Save excercises before", "OK");
            }
            else
            {
                if (selectedExcerciseFromThePlan != null)
                {
                    deleteExerciseFromThePlan = await DisplayAlert("Are you sure?", "The exercise will be permanently deleted from the plan", "OK", "NO");
                    if (deleteExerciseFromThePlan)
                    {
                        ////test
                        //string lt = "";
                        //test.Text = selectedExcerciseFromThePlan; //1. Dmb
                        //for (int i = 0; i < NamesList.Count; i++)
                        //{
                        //    lt += NamesList[i];
                        //}

                        //listTest.Text = lt; //1. Dmb2. Hip
                        //// test

                        //Delete exercise
                        //selectedExcerciseFromThePlan = selectedExcerciseFromThePlan.Substring(3);
                        //selectedExcerciseFromThePlan = ';' + selectedExcerciseFromThePlan;
                        if (NamesList.Contains(selectedExcerciseFromThePlan))
                        {
                            //test.Text = selectedExcerciseFromThePlan;
                            NamesList.Remove(selectedExcerciseFromThePlan);
                        }

                        if (NamesList.Count() >= 1)
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
                        if (PlanPage.selectedPlan != null)
                        {
                            PlanPage.selectedPlan.ListOfExcercisesName = SemicoloneveryName;
                            await App.Database.UpdatePlanAsync(PlanPage.selectedPlan);
                            //await App.Database.UpdatePlanAsync(PlanPage.selectedPlan);
                            //myList.ItemsSource = NamesList;
                        }
                        //else { }
                        await DisplayAlert("Success", "Exercise deleted from the plan", "OK");

                        await Navigation.PushAsync(new AddUpdatePlan());

                        //Get All Exercises  
                        //myList.ItemsSource = NamesList;
                    }
                }
                else
                {
                    await DisplayAlert(":)", "Select excercise first", "OK");
                }
            }            

            //await Navigation.PushAsync(new PlanPage());
        }
        private void myList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedExcerciseFromThePlan = e.CurrentSelection[0] as string;
        }
    }
}