using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TextGeometry
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ConvertButton_OnClick(object sender, RoutedEventArgs e)
        {
            var fontStyle = FontStyles.Normal;
            var fontWeight = FontWeights.Medium;

            var pixelsPerDip = VisualTreeHelper.GetDpi(this).PixelsPerDip;
            // Create the formatted text based on the properties set.
            var formattedText = new FormattedText(
                InputText.Text,
                CultureInfo.GetCultureInfo("en-us"),
                FlowDirection.LeftToRight,
                new Typeface(
                    new FontFamily("Times New Roma"),
                    fontStyle,
                    fontWeight,
                    FontStretches.Normal),
                14,
                System.Windows.Media.Brushes.Black,
                pixelsPerDip // This brush does not matter since we use the geometry of the text. 
            );

            // Build the geometry object that represents the text.
            var buildGeometry = formattedText.BuildGeometry(new System.Windows.Point(0, 0));
            Path.Data = buildGeometry;
            var s = PathGeometry.CreateFromGeometry(buildGeometry).ToString();
            var replace1 = Regex.Replace(s, @"\s", "\n");
            var replace = Regex.Replace(replace1, "([A-Za-z][0-9,.]*)", "\n\n$1");
            PathDataText.Text = replace;
        }
    }
}
