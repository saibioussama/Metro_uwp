using Metro_UWP.SettingsViews;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Metro_UWP
{

    public class SettingsItem
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string Icon { get; set; }
        public Type Page { get; set; }
    }

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        List<SettingsItem> Items;

        public SettingsPage()
        {
            this.InitializeComponent();
            Items = new List<SettingsItem>()
            {
                new SettingsItem()
                {
                    Id = 1,
                    DisplayName = "Search for update",
                    Icon = "",
                    Page = typeof(UpdatePage),
                },
                new SettingsItem()
                {
                    Id= 2,
                    DisplayName = "Defaults stations",
                    Icon = "",
                    Page = typeof(FavoritesPage)
                },
                new SettingsItem()
                {
                    Id= 3,
                    DisplayName = "About",
                    Icon = "",
                    Page = typeof(AboutPage)
                },
                new SettingsItem()
                {
                    Id = 4,
                    DisplayName = "Contact",
                    Icon = "",
                    Page = typeof(ContactPage)
                }
            };
        }

        private void SettingsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = Items[SettingsListBox.SelectedIndex];
            Frame.Navigate(item.Page);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SettingsListBox.ItemsSource = Items;
        }
    }
}
