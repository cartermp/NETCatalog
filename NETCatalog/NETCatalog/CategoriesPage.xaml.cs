using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace NETCatalog
{
    public partial class CategoriesPage : ContentPage
    {
        private readonly Dictionary<string, Dictionary<string, string>> _categoryToConcepts = new Dictionary<string, Dictionary<string, string>>
        {
            {
                "platform",
                new Dictionary<string, string>
                {
                    { "Async and Await",  "async-await" },
                    { "Delegates",  "delegates" },
                    { "Dynamic",  "dynamic" },
                    { "Extension Methods",  "extension-methods" },
                    { "Garbage Collection",  "garbage-collection" },
                    { "Generics",  "generics" },
                    { "Lambdas",  "lambdas" },
                    { "LINQ",  "linq" },
                    { ".NET Standard",  "netstandard" },
                    { "NuGet",  "nuget" },
                    { "Open Standards",  "open-standards" },
                    { "P/Invoke",  "p-invoke" },
                    { "Portable Class Libraries",  "portable-class-libraries" },
                    { "Reflection",  "reflection" },
                    { "SIMD",  "simd" },
                    { "Task Parallel Library",  "task-parallel-library" }
                }
             },
             {
                 "netcore",
                 new Dictionary<string, string>
                 {
                     { "Cross Platform", "cross-platform" },
                     { "Modular", "modular" },
                     { "Multicore JIT", "multi-core-jit" },
                     { ".NET Core", "netcore" },
                     { "Open Source", "open-source" },
                     { "RyuJIT", "ryujit" },
                     { "Side by Side Installations", "side-by-side" }
                 }
             },
             {
                 "netfx",
                 new Dictionary<string, string>
                 {
                     { "ASP.NET Web Forms", "asp-net-webforms" },
                     { "ClickOnce Deployment", "click-once" },
                     { "Multicore JIT", "multi-core-jit" },
                     { ".NET Framework", "netfx" },
                     { "NGEN", "ngen" },
                     { "RyuJIT", "ryujit" },
                     { "WCF", "wcf" },
                     { "Windows Forms", "windows-forms" },
                     { "WPF", "wpf" }
                 }
             },
             {
                 "aspnetcore",
                 new Dictionary<string, string>
                 {
                     { "ASP.NET Core", "aspnetcore" },
                     { "Cross Platform", "cross-platform" },
                     { "Microservices", "microservices" },
                     { "Modular", "modular" },
                     { "Open Source", "open-source" },
                     { "Performance", "performance" }
                 }
             },
             {
                 "uwp",
                 new Dictionary<string, string>
                 {
                     { ".NET Native", "net-native" },
                     { "Universal Windows Platform", "uwp" },
                     { "Windows 10 Family of Devices", "win10-family" },
                     { "Windows Store", "windows-store" },
                     { "WinRT", "winrt" }
                 }
             },
             {
                 "vs",
                 new Dictionary<string, string>
                 {
                     { "Activity Tracing", "activity-tracing" },
                     { "Async Debugging", "async-debugging" },
                     { "Awesome", "awesome" },
                     { "C# REPL", "csharp-repl" },
                     { "Data Oriented Breakpoints", "data-oriented-breakpoints" },
                     { "Edit and Continue", "edit-and-continue" },
                     { "Managed Return Values", "managed-return-values" }
                 }
             },
             {
                 "xamarin",
                 new Dictionary<string, string>
                 {
                     { "Native Mobile", "native-mobile" },
                     { "Xamarin Forms", "xamarin-forms" },
                     { "Xamarin Studio", "xamarin-studio" },
                     { "Xamarin", "xamarin" }
                 }
             },
             {
                 "csharpvb",
                 new Dictionary<string, string>
                 {
                     { "C#", "chsarp" },
                     {"Visual Basic", "vb" }
                 }
             },
             {
                 "fsharp",
                 new Dictionary<string, string>
                 {
                     { "Feature1", "feature1" }
                 }
             }
        };

        private readonly string _currentCategory;

        public CategoriesPage(string category)
        {
            _currentCategory = category;
            InitializeComponent();

            var darkerButton = new Style(typeof(Button))
            {
                Setters =
                {
                  new Setter { Property = Button.BackgroundColorProperty, Value = Color.FromHex ("#ddd") },
                  new Setter { Property = Button.TextColorProperty, Value = Color.Black },
                  new Setter { Property = Button.BorderRadiusProperty, Value = 0 },
                  new Setter { Property = Button.FontSizeProperty, Value = 40 }
                }
            };

            var concepts = _categoryToConcepts[category].Keys.ToList();

            bool firstColumn = true;
            int row = 0;

            for (int i = 0; i < concepts.Count; i++)
            {
                var b = new Button
                {
                    Text = concepts[i],
                    Style = darkerButton
                };
                b.Clicked += Item_Clicked;

                CategoriesGrid.Children.Add(b, firstColumn ? 0 : 1, row);

                firstColumn = !firstColumn;

                if (i % 2 != 0)
                {
                    row++;
                }
            }
        }

        public async void Item_Clicked(object sender, EventArgs e)
        {
            var b = (Button)sender;
            var concept = _categoryToConcepts.Values.First(lookup => lookup.ContainsKey(b.Text))[b.Text];
            await Navigation.PushAsync(new ConceptPage(_currentCategory, concept));
        }
    }
}
