using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLiteNetExtensionsAsync;

namespace workoutPlanner
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExercisePage : ContentPage
    {
        public IList<Exercise> Exercises { get; private set; }
        public ExercisePage()
        {
            InitializeComponent();

            Exercises = new List<Exercise>();
            Exercises.Add(new Exercise
            {
                ID = 1,
                Name = "bench press",
                Category = "chest",
                Img = "benchPress.png"
            });


            Exercises.Add(new Exercise
            {
                ID = 2,
                Name = "Dumbbell bent-over row",
                Category = "back",
                Img = "dumbbellBentOverRow.png"
            });

            Add();


            BindingContext = this;
        }
        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //wybrane cwiczenie
            Exercise selectedItem = e.CurrentSelection[0] as Exercise;
            //Navigation.PushAsync(new PlanPage());

            if (PlanPage.addToThePlan)
            {
                PlanPage.selectedItem.ExercisesInPlan = new List<Exercise> { selectedItem };

                //Delete Person  
                await App.Database.UpdatePlanAsync(PlanPage.selectedItem);
                //Get All Persons  
                PlanPage.addToThePlan = false;

                //person1.ExercisesInPlan = new List<Event> { event1 };
                //db.UpdateWithChildren(person1);
                await DisplayAlert("Success", "Exercise added", "OK");
                await Navigation.PushAsync(new PlanPage());
            }
        }
        async void Add()
        {
            await App.Database.SaveExerciseAsync(new Exercise
            {
                ID = 1,
                Name = "bench press",
                Category = "chest",
                Img = "benchPress.png"
            });
            await App.Database.SaveExerciseAsync(new Exercise
            {
                ID = 2,
                Name = "Dumbbell bent-over row",
                Category = "back",
                Img = "dumbbellBentOverRow.png"
            });
            //await DisplayAlert("Success", "Plan added", "OK");
        }
    }
}