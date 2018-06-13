using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Xamarin.Forms;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

using Xamarin.Forms;

namespace UBNT.Pages
{
    public class Bill : ContentPage
    {
        public Bill()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            var image = new Image
            {
                Source = "topKek.jpg",
                HeightRequest = 70,
                WidthRequest = 70,
                Aspect = Aspect.Fill
            };
            var image2 = new Image
            {
                Source = "botKek.jpg",
                HeightRequest = 50,
                WidthRequest = 70,
                Aspect = Aspect.Fill
            };
            var title = new Label
            {
                Text = "Balance",
                FontSize = 30,
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.Center
            };
            var username = new Label
            {
                BackgroundColor = Color.FromHex("C67110"),
                TextColor = Color.White,
                Text = Application.Current.Properties["mail"].ToString(),
                Margin = new Thickness(0, 7, 0, 0)
            };
            var phone = new Label
            {
                BackgroundColor = Color.FromHex("C67110"),
                TextColor = Color.White,
                Text = Application.Current.Properties["phone"].ToString(),
                Margin = new Thickness(0, 7, 0, 0)
            };
            var balance = new Label
            {
                BackgroundColor = Color.FromHex("C67110"),
                TextColor = Color.White,
                Text = Application.Current.Properties["Balance"].ToString(),
                Margin = new Thickness(0, 7, 0, 0)
            };
            var Action = new Button
            {
                Text = "Action",
                TextColor = Color.White,
                BackgroundColor = Color.FromHex("C67110"),
                Margin = new Thickness(0, 7, 0, 0)
            };
            ScrollView scroll = new ScrollView();
            var Contenti = new StackLayout()
            {
                BackgroundColor = Color.FromHex("#000000"),
                Padding = 0,
                Spacing = 0,
                Children =
                {
                    image, title,
                    new StackLayout ()
                    {
                        Padding = 60,
                        Spacing = 5,
                        Children =
                        {
                            username, phone, balance, Action
                        }
                    },
                    new StackLayout()
                    {
                        VerticalOptions= LayoutOptions.EndAndExpand,
                        Children =
                        {
                           image2
                        }
                    }
                }
            };
            scroll.Content = Contenti;
            Content = scroll;
        }
        public void EnviarCorreo(string email)
        {
            
        }
    }
}