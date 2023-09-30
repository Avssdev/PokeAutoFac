using Autofac;
using AutoFacTest.ViewModels;
using Windows.UI.Xaml.Controls;

namespace AutoFacTest
{
    public sealed partial class MainPage : Page
    {
        public readonly MainPageViewModel ViewModel;
        public MainPage()
        {
            this.InitializeComponent();
            ViewModel = App.Container.Resolve<MainPageViewModel>();
            DataContext = ViewModel;
            LoadToolBar();
        }

        private async void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await ViewModel.SearchPokemon();
        }

        private void LoadToolBar()
        {
            foreach(var item in ViewModel.GetToolbarButtons())
            {
                var newMenuItem = new MenuFlyoutItem();
                newMenuItem.Text = item.Name;
                newMenuItem.Click += (s, e) =>
                {
                    item.Click(GridViewItems.SelectedItem);
                };
                MenuToolBar.Items.Add(newMenuItem);
            }
        }
    }
}
