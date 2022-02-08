using JobSearch.APP.Models;
using JobSearch.APP.Services;
using JobSearch.APP.Utility.Converters;
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
    public partial class RegisterJob : ContentPage
    {
        private JobService _service;
        public RegisterJob()
        {
            InitializeComponent();
            _service = new JobService();
        }

        private void GoBack(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void Save(object sender, EventArgs e)
        {

            TxtMessanges.Text = string.Empty;

            await Navigation.PushPopupAsync(new Loading());

            User user = JsonConvert.DeserializeObject<User>(App.Current.Properties["User"].ToString());

            Job job = new Job()
            {
                Company = TxtCompany.Text,
                JobTitle = TxtJobTitle.Text,
                CityState = TxtCityState.Text,
                InitialSalary = TextToDoubleConverter.ToDouble(TxtInicialSalary.Text),
                FinalSalary = TextToDoubleConverter.ToDouble(TxtFinalSalary.Text),
                ContractType = (RBCLT.IsChecked)? "CLT" : "PJ",
                TecnologyTools = TxtTecnologyTools.Text,
                CompanyDescription = TxtCompanyDescription.Text,
                JobDescription = TxtJobDescription.Text,
                Benefits = TxtBenefits.Text,
                InterestedSendEmailTo = TxtInterestedSendToEmailTo.Text,
                PublicationDate = DateTime.Now,
                UserId = user.Id
            };

            ResponseService<Job> responseService = await _service.AddJob(job);

            if (responseService.IsSucess)
            {

                await Navigation.PopAllPopupAsync();
                await DisplayAlert("Vaga cadastrada!", "Vaga cadastrada com sucesso", "Ok");
                await Navigation.PopAsync();
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

                await Navigation.PopAllPopupAsync();
            }
            
        }
    }
}