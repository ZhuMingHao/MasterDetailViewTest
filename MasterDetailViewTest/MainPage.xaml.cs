using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MasterDetailViewTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public List<Email> Emails { get; set; }
        public MainPage()
        {
            this.InitializeComponent();
            this.DataContext = this;
            Emails = MyEmailManager.GetEmails();
        }
        public ICommand OpenDialog
        {
            get
            {
                return new CommadEventHandler<Email>((s) => OpenDialogCommandFun(s));
            }
        }
        private async void OpenDialogCommandFun(Email s)
        {
            ContentDialog dialogServiceUpdates = new ContentDialog
            {
                Title = s.From,
                Content = s.Body,
                CloseButtonText = "OK"
            };

            ContentDialogResult result = await dialogServiceUpdates.ShowAsync();
        }
        private void MoreBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MyMasterDetailsView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }

    public class PageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Type page = null;
            switch (value as string)
            {
                case "Steve Johnson":
                    page = typeof(FistPage);
                    break;
                case "Pete Davidson":
                    page = typeof(SecondPage);
                    break;
                case "OneDrive":
                    page = typeof(FistPage);
                    break;
                case "Twitter":
                    page = typeof(SecondPage);
                    break;
                default:
                    break;
            }

            return page;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class Email
    {
        public string From { get; set; }
        public string Body { get; set; }
        public bool ShowButton { get; set; }
    }

    public class MyEmailManager
    {
        public static List<Email> GetEmails()
        {
            var MyEmails = new List<Email>
        {
            new Email
            {
                From = "Steve Johnson",
                Body = "Are you available for lunch tomorrow? A client would like to discuss a project with you.",
                ShowButton = true
            },
            new Email
            {
                From = "Pete Davidson",
                Body = "Don't forget the kids have their soccer game this Friday. We have to supply end of game snacks.",
                ShowButton = false
            },
            new Email
            {
                From = "OneDrive",
                Body = "Your new album.\r\nYou uploaded some photos to your OneDrive and automatically created an album for you.",
                ShowButton = false
            },
            new Email
            {
                From = "Twitter",
                Body = "Here are some people we think you might like to follow:\r\n.@randomPerson\r\nAPersonYouMightKnow",
                ShowButton = true
            }
        };

            return MyEmails;
        }
    }

    public class CommadEventHandler<T> : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public Action<T> action;
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this.action((T)parameter);
        }
        public CommadEventHandler(Action<T> action)
        {
            this.action = action;

        }
    }
}
