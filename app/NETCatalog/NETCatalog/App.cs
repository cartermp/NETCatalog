using Xamarin.Forms;

namespace NETCatalog
{
    public class App : Application
    {
        public App()
        {
            MainPage = new NavigationPage(new TopicsPage());
        }
    }
}
