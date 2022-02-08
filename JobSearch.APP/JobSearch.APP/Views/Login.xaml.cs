﻿using JobSearch.APP.Models;
using JobSearch.APP.Services;
using JobSearch.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Extensions;
using JobSearch.APP.Utility.Load;

namespace JobSearch.APP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        private UserService _service;
        public Login()
        {
            InitializeComponent();
            _service = new UserService();

        }

        private void GoRegistrer(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Registrer());
        }

        private async void GoStart(object sender, EventArgs e)
        {
            string email = TxtEmail.Text;
            string password = TxtPassword.Text;

            await Navigation.PushPopupAsync(new Loading());

            ResponseService<User> responseService = await _service.GetUser(email, password);
            
            if  (responseService.IsSucess)
            {
                //Grava a informacao no projeto
                App.Current.Properties.Add("User", JsonConvert.SerializeObject(responseService.Data));
                //Sem essa parte , perde ao fechar o app
                await App.Current.SavePropertiesAsync();
                App.Current.MainPage = new NavigationPage(new Start());
               
            }else
            {
                if (responseService.StatusCode == 404)
                {
                    await DisplayAlert("Erro", "Usuário não encontrado", "OK");
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