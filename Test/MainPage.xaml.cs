using System.Net.Http;
using System.Net.Http.Headers;

using Xamarin.Forms;

namespace Test
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            DownloadAndDisplayPdf();
        }

        public async void DownloadAndDisplayPdf() {

            var client = new HttpClient
            {
                MaxResponseContentBufferSize = 256000
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/pdf"));

            var response = await client.GetAsync("http://www.orimi.com/pdf-test.pdf");
            if (response.IsSuccessStatusCode)
            {
                byte[] contentAsByteArray = await response.Content.ReadAsByteArrayAsync();
                DependencyService.Get<ISaveFile>().SaveAndOpenFile("test.pdf", contentAsByteArray);
            }
        }
    }
}
