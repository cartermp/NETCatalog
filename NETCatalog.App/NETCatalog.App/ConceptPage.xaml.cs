using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CommonMark;

using Xamarin.Forms;

namespace NETCatalog
{
    public partial class ConceptPage : ContentPage
    {
        private readonly HttpClient _client = new HttpClient();
        private readonly string _baseUrl = "http://dotnet-buildtwentysixteendemo.azurewebsites.net/topics";

        public ConceptPage(string category, string conceptName, string niceConceptName)
        {
            InitializeComponent();

            Title = niceConceptName;

            // It's okay not to await this here rather than blocking the UI thread.
            GetMarkdownAndDisplayIt(category, conceptName);
        }

        private async Task GetMarkdownAndDisplayIt(string category, string concept)
        {
            var markdown = await _client.GetStringAsync($"{_baseUrl}/{category}/{concept}");

            var html = new HtmlWebViewSource();

            html.Html = CommonMarkConverter.Convert(markdown);

            MarkdownWebView.Source = html;
        }
    }
}