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
    public partial class PlanDetails : ContentPage
    {
        public List<string> NamesList = new List<string>{};
        public string everyName = "";
        //public static string selectedExcerciseFromPlan = null;
        //public static bool exName = false;
        public PlanDetails()
        {
            InitializeComponent();

            //var template = new DataTemplate(typeof(TextCell));
            //template.SetValue(TextCell.TextColorProperty, Color.Black);
            //myList.ItemTemplate = template;

            planName.Text = PlanPage.selectedPlan.NamePlan;
            planID.Text = PlanPage.selectedPlan.IDPlan.ToString();

            everyName = PlanPage.selectedPlan.ListOfExcercisesName;
            NamesList = everyName.Split(';').ToList();
            for( int i = 0; i < NamesList.Count(); i++)
            {
                NamesList[i] = (i+1).ToString() +". "+ NamesList[i];
            }
            myList.ItemsSource = NamesList;
        }

        //private void myList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    ////wybrane cwiczenie
        //    //selectedExcerciseFromPlan = e.CurrentSelection[0] as string;
        //    //exName = true;
        //    //Navigation.PushAsync(new ExcerciseDetails());
        //}
    }
}