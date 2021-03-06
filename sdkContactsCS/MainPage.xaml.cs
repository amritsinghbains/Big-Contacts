using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Phone.Controls;
using Microsoft.Phone.UserData;

namespace sdkContactsCS
{
    public partial class MainPage : PhoneApplicationPage
    {
        FilterKind contactFilterKind = FilterKind.None;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            name.IsChecked = true;

            //ContactAccounts.DataContext = (new Contacts()).Accounts;
            //CalendarAccounts.DataContext = (new Appointments()).Accounts;
        }


        private void SearchContacts_Click(object sender, RoutedEventArgs e)
        {
            //Add code to validate the contactFilterString.Text input.
            if (contactFilterString.Text.Length >= 3)
            {
                ContactResultsLabel.Text = "Loading... :) ";
                ContactResultsData.DataContext = null;

                Contacts cons = new Contacts();

                cons.SearchCompleted += new EventHandler<ContactsSearchEventArgs>(Contacts_SearchCompleted);

                cons.SearchAsync(contactFilterString.Text, contactFilterKind, "Contacts Test #1");
            }
        }


        void Contacts_SearchCompleted(object sender, ContactsSearchEventArgs e)
        {
            //MessageBox.Show(e.State.ToString());

            try
            {
                //Bind the results to the list box that displays them in the UI
                ContactResultsData.DataContext = e.Results;
            }
            catch (System.Exception)
            {
                //That's okay, no results
            }

            if (ContactResultsData.Items.Count > 0)
            {
                ContactResultsLabel.Text = "Tap name for details :D";
            }
            else
            {
                ContactResultsLabel.Text = "No Results :( ";
            }
        }


        private void ContactResultsData_Tap(object sender, GestureEventArgs e)
        {
            App.con = ((sender as ListBox).SelectedValue as Contact);

            NavigationService.Navigate(new Uri("/ContactDetails.xaml", UriKind.Relative));
        }


        private void FilterChange(object sender, RoutedEventArgs e)
        {
            String option = ((RadioButton)sender).Name;

            InputScope scope = new InputScope();
            InputScopeName scopeName = new InputScopeName();

            switch (option)
            {
                case "name":
                    contactFilterKind = FilterKind.DisplayName;
                    scopeName.NameValue = InputScopeNameValue.Text;
                    break;

                /* case "phone":
                     contactFilterKind = FilterKind.PhoneNumber;
                     scopeName.NameValue = InputScopeNameValue.TelephoneNumber;
                     break;
                */
                case "email":
                    contactFilterKind = FilterKind.EmailAddress;
                    scopeName.NameValue = InputScopeNameValue.EmailSmtpAddress;
                    break;
                

                default:
                    contactFilterKind = FilterKind.None;
                    break;
            }

            scope.Names.Add(scopeName);
            contactFilterString.InputScope = scope;
            contactFilterString.Focus();
        }


        

    }//End page class


    public class ContactPictureConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Contact c = value as Contact;
            if (c == null) return null;

            System.IO.Stream imageStream = c.GetPicture();
            if (null != imageStream)
            {
                return Microsoft.Phone.PictureDecoder.DecodeJpeg(imageStream);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }//End converter class


}//End namespace
