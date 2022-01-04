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
    public partial class ExercisePage : ContentPage
    {
        public IList<Exercise> Exercises { get; private set; }
        public ExercisePage()
        {
            InitializeComponent();

            Exercises = new List<Exercise>();
            Exercises.Add(new Exercise
            {
                Name = "bench press",
                Category = "chest",
                Img = "benchPress.png"
            });

            Exercises.Add(new Exercise
            {
                Name = "Dumbbell bent-over row",
                Category = "back",
                Img = "dumbbellBent-overRow.png"
            });

            Exercises.Add(new Exercise
            {
                Name = "Blue Monkey",
                Category = "Central and East Africa",
                Img = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/83/BlueMonkey.jpg/220px-BlueMonkey.jpg"
            });

            Exercises.Add(new Exercise
            {
                Name = "Squirrel Monkey",
                Category = "Central & South America",
                Img = "https://upload.wikimedia.org/wikipedia/commons/thumb/2/20/Saimiri_sciureus-1_Luc_Viatour.jpg/220px-Saimiri_sciureus-1_Luc_Viatour.jpg"
            });

            Exercises.Add(new Exercise
            {
                Name = "Golden Lion Tamarin",
                Category = "Brazil",
                Img = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/87/Golden_lion_tamarin_portrait3.jpg/220px-Golden_lion_tamarin_portrait3.jpg"
            });

            Exercises.Add(new Exercise
            {
                Name = "Howler Monkey",
                Category = "South America",
                Img = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/0d/Alouatta_guariba.jpg/200px-Alouatta_guariba.jpg"
            });

            Exercises.Add(new Exercise
            {
                Name = "Japanese Macaque",
                Category = "Japan",
                Img = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c1/Macaca_fuscata_fuscata1.jpg/220px-Macaca_fuscata_fuscata1.jpg"
            });

            Exercises.Add(new Exercise
            {
                Name = "Mandrill",
                Category = "Southern Cameroon, Gabon, Equatorial Guinea, and Congo",
                Img = "https://upload.wikimedia.org/wikipedia/commons/thumb/7/75/Mandrill_at_san_francisco_zoo.jpg/220px-Mandrill_at_san_francisco_zoo.jpg"
            });

            Exercises.Add(new Exercise
            {
                Name = "Proboscis Monkey",
                Category = "Borneo",
                Img = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/e5/Proboscis_Monkey_in_Borneo.jpg/250px-Proboscis_Monkey_in_Borneo.jpg"
            });

            Exercises.Add(new Exercise
            {
                Name = "Red-shanked Douc",
                Category = "Vietnam, Laos",
                Img = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/9f/Portrait_of_a_Douc.jpg/159px-Portrait_of_a_Douc.jpg"
            });

            Exercises.Add(new Exercise
            {
                Name = "Gray-shanked Douc",
                Category = "Vietnam",
                Img = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/0b/Cuc.Phuong.Primate.Rehab.center.jpg/320px-Cuc.Phuong.Primate.Rehab.center.jpg"
            });

            Exercises.Add(new Exercise
            {
                Name = "Golden Snub-nosed Monkey",
                Category = "China",
                Img = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c8/Golden_Snub-nosed_Monkeys%2C_Qinling_Mountains_-_China.jpg/165px-Golden_Snub-nosed_Monkeys%2C_Qinling_Mountains_-_China.jpg"
            });

            Exercises.Add(new Exercise
            {
                Name = "Black Snub-nosed Monkey",
                Category = "China",
                Img = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/59/RhinopitecusBieti.jpg/320px-RhinopitecusBieti.jpg"
            });

            Exercises.Add(new Exercise
            {
                Name = "Tonkin Snub-nosed Monkey",
                Category = "Vietnam",
                Img = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/9c/Tonkin_snub-nosed_monkeys_%28Rhinopithecus_avunculus%29.jpg/320px-Tonkin_snub-nosed_monkeys_%28Rhinopithecus_avunculus%29.jpg"
            });

            Exercises.Add(new Exercise
            {
                Name = "Thomas's Langur",
                Category = "Indonesia",
                Img = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/31/Thomas%27s_langur_Presbytis_thomasi.jpg/142px-Thomas%27s_langur_Presbytis_thomasi.jpg"
            });

            Exercises.Add(new Exercise
            {
                Name = "Purple-faced Langur",
                Category = "Sri Lanka",
                Img = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/02/Semnopithèque_blanchâtre_mâle.JPG/192px-Semnopithèque_blanchâtre_mâle.JPG"
            });

            Exercises.Add(new Exercise
            {
                Name = "Gelada",
                Category = "Ethiopia",
                Img = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/13/Gelada-Pavian.jpg/320px-Gelada-Pavian.jpg"
            });

            BindingContext = this;
        }
        void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Exercise selectedItem = e.CurrentSelection[0] as Exercise;
            Navigation.PushAsync(new Plans());
        }
    }
}