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
    public partial class Plans : ContentPage
    {
        static public bool del = false;
        Person selectedItem = null;
        public Plans()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            collectionView.ItemsSource = await App.Database.GetPeopleAsync();
        }

        async void OnButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(nameEntry.Text) && !string.IsNullOrWhiteSpace(ageEntry.Text))
            {
                await App.Database.SavePersonAsync(new Person
                {
                    Name = nameEntry.Text,
                    Age = int.Parse(ageEntry.Text)
                });
                await DisplayAlert("Success", "Person Added", "OK");

                nameEntry.Text = ageEntry.Text = string.Empty;
                collectionView.ItemsSource = await App.Database.GetPeopleAsync();
            }
        }
        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedItem = e.CurrentSelection[0] as Person;

        }

        async private void Button_Clicked(object sender, EventArgs e)
        {
            //del = true;
            if (selectedItem != null)
            {

                //Delete Person  
                await App.Database.DeleteItemAsync(selectedItem);
                await DisplayAlert("Success", "Person Deleted", "OK");

                //Get All Persons  
                collectionView.ItemsSource = await App.Database.GetPeopleAsync();

            }
        }
        
    }
}
