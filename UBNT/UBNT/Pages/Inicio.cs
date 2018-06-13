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

namespace UBNT
{

    public class Inicio : ContentPage
    {
        public Inicio()
        {
            var title = new Label
            {
                Text = "Bienvenido",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
            var aboutButton = new Button
            {
                Text = "Información"
            };

            var signupButton = new Button
            {
                Text = "Registrarse"
            };
            var email = new Entry
            {
                Placeholder = "Email",
            };

            var phone = new Entry
            {
                Placeholder = "Phone",
            };
            var login = new Button
            {
                Text = "Login"
            };
            login.Clicked += (sender, e) =>
            {
                Login(email.Text, phone.Text);
            };

            Content = new StackLayout
            {
                Padding = 30,
                Spacing = 10,
                Children = { title, email, phone, login }
            };
        }
        public async void Login(string usernamer, string phone)
        {
            var json = new StringContent("{ \"user\": \"daniel\",  \"password\": \"luisdaniel\",  \"expiration\": 604800,  \"sliding\": 1,  \"deviceName\": \"iphone X\" }", System.Text.Encoding.UTF8, "application / json");
            var client = new HttpClient()
            {
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.TryAddWithoutValidation("X-Auth-App-Key", "RxLodODMKZJjrYt9GLeP01tf9QVZsfh3mN9EE5qd9l5jstpN0hbNnPqLdhqTYamB");
            try
            {
                var URI = "https://portal.backnetwork.net/api/v1.0/clients";
                //URI += "(656)327-6846";
                using (var response = await client.GetAsync(URI))
                {
                    var username = usernamer;
                    var ident = phone;
                    ////////////////////
                    var separators = new string[] { " ", "-", "(", ")" };
                    foreach (var c in separators)
                    {
                       ident = ident.Replace(c, string.Empty);
                    }
                    ////////////////////
                    string responseData = await response.Content.ReadAsStringAsync();
                    var root = JToken.Parse(responseData);
                    var values = root.Where(innerItem => innerItem["username"].Value<string>() == username)
                    .SelectMany(innerItem => innerItem["contacts"]
                        .Where(itm => itm["phone"].Value<string>()== ident))
                           .Select(innerItem => innerItem["accountBalance"])
                    .ToList();
                    var message = string.Join(Environment.NewLine, values);
                    //var values = root.Where(t =>(string)t["contacts"] == ident).ToList();
                    //var message = string.Join(Environment.NewLine, values);
                    bool isEmpty = !values.Any();
                    if (isEmpty)
                    {
                        await DisplayAlert("Oops!", "Wrong Username or Password", "OK");
                    }
                    else
                    {
                        Application.Current.Properties["mail"] = usernamer;
                        Application.Current.Properties["phone"] = ident;
                        var qoot = JToken.Parse(responseData);
                        var palues = qoot.Where(innerItem => innerItem["username"].Value<string>() == username)
                        .Select(innerItem => innerItem["accountBalance"])
                        .ToList();
                        var mensage = string.Join(Environment.NewLine, palues);
                        Application.Current.Properties["Balance"] = mensage;
                        await Navigation.PushModalAsync(new Pages.Bill());
                    }
                }
            }
            catch (HttpRequestException e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
        }
    }
}