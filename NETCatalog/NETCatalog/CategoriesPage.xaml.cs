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
        // I feel dirty for hard-coding this but it was honestly the best solution at the time.
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
                    { ".NET Standard",  "netstandard" },  // needs image!
                    { "NuGet",  "nuget" },
                    { "Open Standards",  "open-standards" }, // need image!
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
                     { "Cross Platform", "cross-platform" }, // needs image!
                     { "Modular", "modular" }, // needs image!
                     { "Multicore JIT", "multi-core-jit" },
                     { ".NET Core", "netcore" }, // needs image!
                     { "Open Source", "open-source" }, // needs image!
                     { "RyuJIT", "ryujit" },
                     { "Side by Side Installations", "side-by-side" } // needs image!
                 }
             },
             {
                 "netfx",
                 new Dictionary<string, string>
                 {
                     { "ASP.NET Web Forms", "asp-net-webforms" }, // needs image!
                     { "ClickOnce Deployment", "click-once" },
                     { "Multicore JIT", "multi-core-jit" },
                     { ".NET Framework", "netfx" }, // needs image!
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
                     { "ASP.NET Core", "aspnetcore" }, // needs image!
                     { "Cross Platform", "cross-platform" }, // needs image!
                     { "Microservices", "microservices" }, // needs image!
                     { "Modular", "modular" }, // needs image!
                     { "Open Source", "open-source" }, // needs image!
                     { "Performance", "performance" } // needs image!
                 }
             },
             {
                 "uwp",
                 new Dictionary<string, string>
                 {
                     { ".NET Native", "net-native" },
                     { "Universal Windows Platform", "uwp" }, // needs image!
                     { "Windows 10 Family of Devices", "win10-family" }, // needs image!
                     { "Windows Store", "windows-store" }, // needs image!
                     { "WinRT", "winrt" } // needs image!
                 }
             },
             {
                 "vs",
                 new Dictionary<string, string>
                 {
                     { "Activity Tracing", "activity-tracing" },
                     { "Async Debugging", "async-debugging" },
                     { "Awesome", "awesome" }, // needs image!
                     { "C# REPL", "csharp-repl" }, // needs image!
                     { "Data Oriented Breakpoints", "data-oriented-breakpoints" }, // needs image!
                     { "Edit and Continue", "edit-and-continue" },
                     { "Managed Return Values", "managed-return-values" }
                 }
             },
             {
                 "xamarin",
                 new Dictionary<string, string>
                 {
                     { "Native Mobile", "native-mobile" }, // needs image!
                     { "Xamarin Forms", "xamarin-forms" }, // needs image!
                     { "Xamarin Studio", "xamarin-studio" }, // needs image!
                     { "Xamarin", "xamarin" } // needs image!
                 }
             },
             {
                 "csharpvb",
                 new Dictionary<string, string>
                 {
                     { "C#", "chsarp" }, // needs image!
                     {"Visual Basic", "vb" } // needs image!
                 }
             },
             {
                 "fsharp",
                 new Dictionary<string, string>
                 {
                     { "Feature1", "feature1" } // needs image!
                 }
             }
        };

        public CategoriesPage(string category, string categoryTitle)
        {
            InitializeComponent();

            Title = categoryTitle;

            var concepts = _categoryToConcepts[category].Keys.ToArray();

            bool firstColumn = true;
            int row = 0;

            for (int i = 0; i < concepts.Length; i++)
            {
                var fileName = _categoryToConcepts[category][concepts[i]];
                var textualConceptName = concepts[i];

                var image = BuildImage(category, fileName, textualConceptName);
                var label = BuildLabel(category, fileName, textualConceptName);

                var sl = new StackLayout
                {
                    Spacing = 0,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Children = { image, label }
                };

                CategoriesGrid.Children.Add(sl, firstColumn ? 0 : 1, row);

                firstColumn = !firstColumn;

                if (i % 2 != 0)
                {
                    row++;
                }
            }
        }

        private Label BuildLabel(string category, string conceptName, string niceConceptName)
        {
            var label = new Label
            {
                Text = niceConceptName,
                HorizontalOptions = LayoutOptions.Center,
                FontSize = 15
            };

            var labelTapRecognizer = new TapGestureRecognizer();
            labelTapRecognizer.Tapped += async (s, e) => 
                await Navigation.PushAsync(new ConceptPage(category, conceptName, niceConceptName));

            label.GestureRecognizers.Add(labelTapRecognizer);
            return label;
        }

        private Image BuildImage(string category, string fileName, string niceConceptName)
        {
            var image = new Image
            {
                Aspect = Aspect.AspectFit,
                Source = ImageSource.FromResource($"NETCatalog.{category}.{fileName}.png"),
            };

            var imageTapRecognizer = new TapGestureRecognizer();
            imageTapRecognizer.Tapped += async (s, e) => 
                await Navigation.PushAsync(new ConceptPage(category, conceptName: fileName, niceConceptName: niceConceptName));

            image.GestureRecognizers.Add(imageTapRecognizer);
            return image;
        }
    }
}
