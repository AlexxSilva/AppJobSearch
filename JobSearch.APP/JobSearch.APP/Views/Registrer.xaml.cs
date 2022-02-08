using JobSearch.APP.Models;
using JobSearch.APP.Services;
using JobSearch.APP.Utility.Load;
using JobSearch.Domain.Models;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JobSearch.APP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registrer : ContentPage
    {
        private UserService _service;
        public Registrer()
        {
            InitializeComponent();
            _service = new UserService();
        }

        private void GoBack(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void SaveAction(object sender, EventArgs e)
        {
            string Name = TxtNome.Text;
            string email = TxtEmail.Text;
            string password = TxtPassword.Text;
            TxtMessanges.Text = string.Empty;

            await Navigation.PushPopupAsync(new Loading());

            User user = new User()
            {
                Name = Name,
                Email = email,
                Password = password
            };

            ResponseService<User> responseService = await _service.AddUser(user);

            if (responseService.IsSucess)
            {
                //Grava a informacao no projeto
                App.Current.Properties.Add("User", JsonConvert.SerializeObject(responseService.Data));
                //Sem essa parte , perde ao fechar o app
                await App.Current.SavePropertiesAsync();
                App.Current.MainPage = new NavigationPage(new Start());

            }
            else
            {
                if (responseService.StatusCode == 400)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var dicKey in responseService.Errors)
                    {
                        foreach (var message in dicKey.Value)
                        {
                            sb.AppendLine(message);
                        }
                    }

                    TxtMessanges.Text = sb.ToString();
                }
                else
                {
                    await DisplayAlert("Erro", "Oops! Ocorreu um erro inesperado. Tente novamente mais tarde.", "OK");
                }
            }

            await Navigation.PopAllPopupAsync();
        }
    }
}