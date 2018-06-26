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
            NavigationPage.SetHasNavigationBar(this, false);
            var image = new Image
            {
                Source = "logo.png",
                HeightRequest = 120,
                WidthRequest = 50,
                Aspect = Aspect.AspectFit
            };
            var aboutButton = new Button
            {
                Text = "Información",
                BackgroundColor = Color.Black,
                TextColor = Color.White,
            };
            var email = new Entry
            {
                Placeholder = "Email",
            };

            var phone = new Entry
            {
                Placeholder = "Teléfono",
            };
            var login = new Button
            {
                BorderRadius = 20,
                Text = "INICIAR SESIóN",
                BackgroundColor = Color.Black,
                TextColor = Color.White,
            };
            login.Clicked += (sender, e) =>
            {
                Login(email.Text, phone.Text);
            };

            Content = new StackLayout
            {
                Padding = 40,
                Spacing = 10,
                Children = { image, email, phone, login,
                    new StackLayout ()
                    {
                        VerticalOptions= LayoutOptions.EndAndExpand,
                        Padding = 10,
                        Spacing = 10,
                        Children =
                        {
                            aboutButton
                        }
                    },
                }
            };
        }
        public async void Login(string usernamer, string phone)
        {
            //var json = new StringContent("{ \"user\": \"daniel\",  \"password\": \"luisdaniel\",  \"expiration\": 604800,  \"sliding\": 1,  \"deviceName\": \"iphone X\" }", System.Text.Encoding.UTF8, "application / json");
            var client = new HttpClient()
            {
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.TryAddWithoutValidation("X-Auth-App-Key", "RxLodODMKZJjrYt9GLeP01tf9QVZsfh3mN9EE5qd9l5jstpN0hbNnPqLdhqTYamB");
            try
            {
                var URI = "https://portal.backnetwork.net/api/v1.0/clients";
                using (var response = await client.GetAsync(URI))
                {
                    var username = usernamer;
                    var ident = phone;
                    var separators = new string[] { " ", "-", "(", ")" };
                    foreach (var c in separators)
                    {
                       ident = ident.Replace(c, string.Empty);
                    }
                    string responseData = await response.Content.ReadAsStringAsync();
                    var root = JToken.Parse(responseData);
                    var values = root.Where(innerItem => innerItem["username"].Value<string>() == username)
                    .SelectMany(innerItem => innerItem["contacts"]
                        .Where(itm => itm["phone"].Value<string>()== ident))
                           .Select(innerItem => innerItem["accountBalance"])
                    .ToList();
                    var message = string.Join(Environment.NewLine, values);
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
                        .ToList();
                        var mensage = string.Join(Environment.NewLine, palues);
                        var mensageJson = JsonConvert.DeserializeObject<JObject>(mensage);
                        var name = mensageJson["firstName"].ToString();
                        var last = mensageJson["lastName"].ToString();
                        name = name + " " + last;
                        var j = mensageJson["contacts"].ToString();
                        var accBal = mensageJson["accountBalance"].ToString();
                        j = j.Remove(0, 1);
                        j = j.Remove(j.Length - 1);
                        var jId = JsonConvert.DeserializeObject<JObject>(j);
                        var clientId = jId["clientId"].ToString();
                        Application.Current.Properties["Balance"] = accBal;
                        //////////////////////////////////////////////////////
                        URI = "https://portal.backnetwork.net/api/v1.0/clients/"+clientId+ "/services";
                        using (var responso = await client.GetAsync(URI))
                        {
                            responseData = await responso.Content.ReadAsStringAsync();
                            root = JToken.Parse(responseData);
                            values = root.Where(innerItem => innerItem["clientId"].Value<string>() == clientId)
                                //.Select(innerItem => innerItem["downloadSpeed"])
                                    .ToList();
                            message = string.Join(Environment.NewLine, values);
                            mensageJson = JsonConvert.DeserializeObject<JObject>(message);
                            var download = mensageJson["downloadSpeed"].ToString();
                            var price = mensageJson["price"].ToString();
                            var factura = mensageJson["invoicingPeriodStartDay"].ToString();
                            Application.Current.Properties["Download"] = download;
                            Application.Current.Properties["Factura"] = factura;
                            Application.Current.Properties["Price"] = price;
                            Application.Current.Properties["Name"] = name;
                        }
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