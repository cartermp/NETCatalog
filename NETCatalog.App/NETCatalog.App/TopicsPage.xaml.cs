﻿using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace NETCatalog
{
    public partial class TopicsPage : ContentPage
    {
        public TopicsPage()
        {
            InitializeComponent();

            var topics = new List<Tuple<string, string>>
            {
                Tuple.Create("platform", ".NET Platform"),
                Tuple.Create("netcore", ".NET Core"),
                Tuple.Create("netfx", ".NET Framework"),
                Tuple.Create("aspnetcore", "ASP.NET Core"),
                Tuple.Create("uwp", "Universal Windows"),
                Tuple.Create("vs", "Visual Studio"),
                Tuple.Create("xamarin", "Xamarin"),
                Tuple.Create("csharpvbfsharp", "C#, VB and F#")
            };

            Title = ".NET Catalog";
            BackgroundColor = Color.FromHex("#35396b");

            // The following will not overlay on top of the background color.
            //BackgroundImage = "NETCatalog.App.backgroundLowerLeft.png";

            bool firstColumn = true;
            int row = 0;

            for (int i = 0; i < topics.Count; i++)
            {
                var image = BuildImage(topics[i].Item1, topics[i].Item2);
                var label = BuildLabel(topics[i].Item1, topics[i].Item2);

                var sl = new StackLayout
                {
                    Padding = new Thickness(20, 5, 5, 5),
                    Spacing = 10,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Children = { image, label },
                    BackgroundColor = Color.Transparent
                };

                TopicsGrid.Children.Add(sl, firstColumn ? 0 : 1, row);

                firstColumn = !firstColumn;

                if (i % 2 != 0)
                {
                    row++;
                }
            }
        }

        private Label BuildLabel(string category, string categoryTitle)
        {
            var label = new Label
            {
                Text = categoryTitle,
                HorizontalOptions = LayoutOptions.Center,
                FontSize = 15,
                BackgroundColor = Color.Transparent
            };

            var labelTapRecognizer = new TapGestureRecognizer();
            labelTapRecognizer.Tapped += async (s, e) =>
                await Navigation.PushAsync(new CategoriesPage(category, categoryTitle));

            label.GestureRecognizers.Add(labelTapRecognizer);
            return label;
        }

        private Image BuildImage(string category, string categoryTitle)
        {
            var image = new Image
            {
                Aspect = Aspect.AspectFit,
                Source = ImageSource.FromResource($"NETCatalog.App.{category}.{category}.png"),
                BackgroundColor = Color.Transparent
            };

            var imageTapRecognizer = new TapGestureRecognizer();
            imageTapRecognizer.Tapped += async (s, e) =>
                await Navigation.PushAsync(new CategoriesPage(category, categoryTitle));

            image.GestureRecognizers.Add(imageTapRecognizer);
            return image;
        }
    }
}