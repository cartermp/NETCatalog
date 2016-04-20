using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace NETCatalog
{
    public partial class TopicsPage : ContentPage
    {
        private readonly Dictionary<string, string> _categoryLookup;
        public TopicsPage()
        {
            InitializeComponent();
            _categoryLookup = new Dictionary<string, string>
            {
                { ".NET Platform", "platform" },
                { ".NET Core", "netcore" },
                { ".NET Framework", "netfx" },
                { "ASP.NET Core", "aspnetcore" },
                { "Universal Windows Platform", "uwp" },
                { "Visual Studio", "vs" },
                { "Xamarin", "xamarin" },
                { "C# and VB", "csharpvb" },
                { "F#", "fsharp" },
            };
        }

        public async void Item_Clicked(object sender, EventArgs e)
        {
            var b = (Button)sender;
            var category = _categoryLookup[b.Text];
            await Navigation.PushAsync(new CategoriesPage(category));
        }
    }
}
