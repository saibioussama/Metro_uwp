using Metro_UWP.Repos;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Metro_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public delegate void OnSearchBoxTextChangedDelegate(string QueryText);

        public static event OnSearchBoxTextChangedDelegate OnSearchBoxTextChanged;

        public MainPage()
        {
            this.InitializeComponent();
            myFrame1.Navigate(typeof(HomePage));
            TimesPage.HideSearchBox += () =>
            {
                mySearchBox.Visibility = Visibility.Collapsed;
            };
            TimesPage.ShowSearchBox += () =>
            {
                mySearchBox.Visibility = Visibility.Visible;
            };
        }

        
        //private void MyNavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        //{
        //    var item = args.SelectedItem as NavigationViewItem;
        //    switch (item.Content)
        //    {
        //        case "Home":
        //            MyFrame.Navigate(typeof(HomePage));
        //            MyNavView.Header = "Home";
        //            break;
        //        case "Settings":
        //            MyFrame.Navigate(typeof(HomePage));
        //            MyNavView.Header = "Settings";
        //            break;
        //        default:break;
        //    }
        //}

        //private async void Page_Loaded(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        await StorageRepos.GetData();
        //    }
        //    catch (Exception ex) { }
        //}

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;

        }

        private void listitem1_Tapped(object sender, TappedRoutedEventArgs e)
        {
            HumburgerTB.Text = "Home";
            myFrame1.Navigate(typeof(HomePage));
            MySplitView.IsPaneOpen = false;
            mySearchBox.Visibility = Visibility.Visible;
        }

        private void listitem5_Tapped(object sender, TappedRoutedEventArgs e)
        {

            myFrame1.Navigate(typeof(SettingsPage));
            HumburgerTB.Text = "Settings";

            MySplitView.IsPaneOpen = false;
            MyListBox.SelectedItem = false;
            mySearchBox.Visibility = Visibility.Collapsed;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //await StorageRepos.ClearData();
                await StorageRepos.GetData();
                myFrame1.Navigate(typeof(HomePage));
            }
            catch (Exception ) { }
        }

        private void mySearchBox_QuerySubmitted(SearchBox sender, SearchBoxQuerySubmittedEventArgs args)
        {
            OnSearchBoxTextChanged?.Invoke(args.QueryText);
        }

        private void mySearchBox_QueryChanged(SearchBox sender, SearchBoxQueryChangedEventArgs args)
        {
            OnSearchBoxTextChanged?.Invoke(args.QueryText);
        }

        private void listitem2_Tapped(object sender, TappedRoutedEventArgs e)
        {
            HumburgerTB.Text = "Stations";
            myFrame1.Navigate(typeof(StationsPage));
            MySplitView.IsPaneOpen = false;
            mySearchBox.Visibility = Visibility.Visible;
        }

        private void listitem3_Tapped(object sender, TappedRoutedEventArgs e)
        {
            HumburgerTB.Text = "Map";
            myFrame1.Navigate(typeof(MapPage));
            MySplitView.IsPaneOpen = false;
            mySearchBox.Visibility = Visibility.Visible;
        }
    }

}
