using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace FileRenamer
{
    /// <summary>
    /// Interaction logic for UserTextInput.xaml
    /// </summary>
    public partial class UserTextInput : UserControl
    {
        public UserTextInput()
        {
            InitializeComponent();
            UpdateOverlayLabelVisibility();
        }

        public static readonly DependencyProperty PlaceholderTextProperty =
            DependencyProperty.Register("PlaceholderText", typeof(string), typeof(UserTextInput), new PropertyMetadata("Enter text here..."));

        public static readonly DependencyProperty PlaceholderForegroundProperty =
            DependencyProperty.Register("PlaceholderForeground", typeof(Brush), typeof(UserTextInput), new PropertyMetadata(Brushes.Black));

        public static readonly DependencyProperty PlaceholderFontSizeProperty =
            DependencyProperty.Register("PlaceholderFontSize", typeof(double), typeof(UserTextInput), new PropertyMetadata(12.0));

        public static readonly DependencyProperty PlaceholderFontStyleProperty =
            DependencyProperty.Register("PlaceholderFontStyle", typeof(FontStyle), typeof(UserTextInput), new PropertyMetadata(FontStyles.Italic));

        public static readonly DependencyProperty PlaceholderBackgroundProperty =
            DependencyProperty.Register("PlaceholderBackground", typeof(Brush), typeof(UserTextInput), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#03b7a2"))));

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(UserTextInput), new PropertyMetadata(string.Empty, OnTextChanged));

        public string PlaceholderText
        {
            get { return (string)GetValue(PlaceholderTextProperty); }
            set { SetValue(PlaceholderTextProperty, value); }
        }

        public Brush PlaceholderForeground
        {
            get { return (Brush)GetValue(PlaceholderForegroundProperty); }
            set { SetValue(PlaceholderForegroundProperty, value); }
        }

        public double PlaceholderFontSize
        {
            get { return (double)GetValue(PlaceholderFontSizeProperty); }
            set { SetValue(PlaceholderFontSizeProperty, value); }
        }

        public FontStyle PlaceholderFontStyle
        {
            get { return (FontStyle)GetValue(PlaceholderFontStyleProperty); }
            set { SetValue(PlaceholderFontStyleProperty, value); }
        }

        public Brush PlaceholderBackground
        {
            get { return (Brush)GetValue(PlaceholderBackgroundProperty); }
            set { SetValue(PlaceholderBackgroundProperty, value); }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (UserTextInput)d;
            control.UpdateOverlayLabelVisibility();
        }

        private void InputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Text = InputTextBox.Text;
            UpdateOverlayLabelVisibility();
        }

        private void InputTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            OverlayLabel.Visibility = Visibility.Collapsed;
        }

        private void InputTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            UpdateOverlayLabelVisibility();
        }

        private void UpdateOverlayLabelVisibility()
        {
            if (string.IsNullOrEmpty(Text))
            {
                OverlayLabel.Visibility = Visibility.Visible;
            }
            else
            {
                OverlayLabel.Visibility = Visibility.Collapsed;
            }
        }
    }
}
