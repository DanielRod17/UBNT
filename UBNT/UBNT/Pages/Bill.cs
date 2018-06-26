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

namespace UBNT.Pages
{
    public class Bill : ContentPage
    {
        public Bill()
        {
            var name = Application.Current.Properties["Name"].ToString();
            var download = Application.Current.Properties["Download"].ToString();
            var factura = Application.Current.Properties["Factura"].ToString();
            var price = Application.Current.Properties["Price"].ToString();
            var bal = Application.Current.Properties["Balance"].ToString();
            var mail = Application.Current.Properties["mail"].ToString();
            var phoneNo = Application.Current.Properties["phone"].ToString();
            var src = "";
            NavigationPage.SetHasNavigationBar(this, false);
            var title = new Label
            {
                Text = "Balance",
                FontSize = 30,
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.Center
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
            var grid = new Grid ();
            /////////////////////
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(120) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(5) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(100) });

            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            if (Application.Current.Properties["Download"].ToString() == "10")
            {
                src = "10M.png";
            }
            if (Application.Current.Properties["Download"].ToString() == "20")
            {
                src = "20M.png";
            }
            if (Application.Current.Properties["Download"].ToString() == "40")
            {
                src = "40M.png";
            }
            var image = new Image
            {

                Source = src,
                HeightRequest = 120,
                WidthRequest = 125,
                Aspect = Aspect.Fill
            };
            var info = new Label
            {
                Text = name+"\nPaquete Actual: "+download+"Mb\nPrecio: $"+price,
                FontSize = 18,
                TextColor = Color.Black,
                BackgroundColor = Color.White,
            };
            var Balance = new Label
            {
                Text = "Balance Actual: \n" + bal,
                FontSize = 18,
                TextColor = Color.Black,
                BackgroundColor = Color.FromHex("#4286f4"),
            };
            var Ticket = new Label
            {
                Text = "Crear un Ticket",
                FontSize = 18,
                TextColor = Color.Black,
                BackgroundColor = Color.FromHex("#f4f142"),
            };
            var Pagar = new Label
            {
                Text = "Pagar Servicio",
                FontSize = 18,
                TextColor = Color.Black,
                BackgroundColor = Color.FromHex("#41f47d"),
            };
            var Separador = new Label
            {
                BackgroundColor = Color.Black,
            };
            grid.Children.Add(image, 0, 0);
            Grid.SetColumnSpan(image, 4);

            grid.Children.Add(info, 4, 0);
            Grid.SetColumnSpan(info, 6);

            grid.Children.Add(Separador, 0, 1);
            Grid.SetColumnSpan(Separador, 10);

            grid.Children.Add(Balance, 0, 2);
            Grid.SetColumnSpan(Balance, 6);
            Grid.SetRowSpan(Balance, 2);

            grid.Children.Add(Ticket, 6, 2);
            Grid.SetColumnSpan(Ticket, 4);
            Grid.SetRowSpan(Ticket, 2);

            grid.Children.Add(Pagar, 0, 4);
            Grid.SetColumnSpan(Pagar, 10);
            Grid.SetRowSpan(Pagar, 3);

            scroll.Content = grid;
            scroll.BackgroundColor = Color.White;
            Content = scroll;
        }
        public void EnviarCorreo(string email)
        {
            
        }
    }
}