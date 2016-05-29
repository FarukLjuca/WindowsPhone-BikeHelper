using Bike_Helper.Model;
using System;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Bike_Helper.Controls
{
    class Pushpin : Button
    {
        public Store Store { get; set; }

        public Pushpin(String text, Geopoint geopoint, Store store, RoutedEventHandler pushpin_Click)
        {
            BorderThickness = new Thickness(0);

            StackPanel stackPanel = new StackPanel();

            TextBlock textBlock = new TextBlock();
            textBlock.Foreground = new SolidColorBrush(Colors.Black);
            textBlock.Text = text;
            textBlock.HorizontalAlignment = HorizontalAlignment.Center;
            stackPanel.Children.Add(textBlock);

            Polygon polygon = new Polygon();
            polygon.Points.Add(new Point(0, 0));
            polygon.Points.Add(new Point(60, 0));
            polygon.Points.Add(new Point(30, 10));
            polygon.Fill = new SolidColorBrush(Colors.Black);
            polygon.HorizontalAlignment = HorizontalAlignment.Center;
            stackPanel.Children.Add(polygon);

            if (store != null)
            {
                Store = store;
                Click += pushpin_Click;
            }

            Content = stackPanel;

            MapControl.SetLocation(this, geopoint);
            MapControl.SetNormalizedAnchorPoint(this, new Point(0.5, 1));
        }
    }
}
