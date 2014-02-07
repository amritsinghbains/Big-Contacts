/* 
    Copyright (c) 2011 Microsoft Corporation.  All rights reserved.
    Use of this sample source code is subject to the terms of the Microsoft license 
    agreement under which you licensed this sample source code and is provided AS-IS.
    If you did not accept the terms of the license agreement, you are not authorized 
    to use this sample source code.  For the terms of the license, please see the 
    license agreement between you and Microsoft.
  
    To see all Code Samples for Windows Phone, visit http://go.microsoft.com/fwlink/?LinkID=219604 
  
*/
using System;
using Microsoft.Phone.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Tasks;
using System.Windows;





namespace sdkContactsCS
{
    public partial class ContactDetails : PhoneApplicationPage
    {

       
        public ContactDetails()
        {
            InitializeComponent();
        }

        private void TextBlock_Tap3(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (!(Application.Current as App).IsTrial)
            {




                PhoneCallTask phoneTask = new PhoneCallTask();

                try
                {
                    String a = yo.Items[0].ToString();

                    Clipboard.SetText(yo.Items[0].ToString());
                    MessageBox.Show("Phone Number Copied to ClipBoard");

                }
                catch (ArgumentOutOfRangeException e1)
                {
                    phoneTask.PhoneNumber = "NO NUMBERS DEFINED ";
                    phoneTask.Show();
                }

            }
            else
            {

                MessageBox.Show("This feature is not available in Free version.", "Buy App", MessageBoxButton.OK);
                MarketplaceDetailTask _marketPlaceDetailTask = new MarketplaceDetailTask();
                _marketPlaceDetailTask.Show();
            }

        }
        
       


        private void TextBlock_Tap2(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (!(Application.Current as App).IsTrial)
            {


                SmsComposeTask smsComposeTask = new SmsComposeTask();


                PhoneCallTask phoneTask = new PhoneCallTask();

                try
                {
                    String a = yo.Items[0].ToString();

                    smsComposeTask.To = yo.Items[0].ToString();


                    smsComposeTask.Show();
                }
                catch (ArgumentOutOfRangeException e1)
                {
                    phoneTask.PhoneNumber = "NO NUMBERS DEFINED ";
                    phoneTask.Show();
                }

            }
            else
            {

                MessageBox.Show("This feature is not available in Free version.", "Buy App", MessageBoxButton.OK);
                MarketplaceDetailTask _marketPlaceDetailTask = new MarketplaceDetailTask();
                _marketPlaceDetailTask.Show();
            }

        }
        
        private void TextBlock_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

            if (!(Application.Current as App).IsTrial)
            {


                PhoneCallTask phoneTask = new PhoneCallTask();
                phoneTask.DisplayName = App.con.DisplayName;
                try
                {
                    String a = yo.Items[0].ToString();

                    phoneTask.PhoneNumber = yo.Items[0].ToString();

                    phoneTask.Show();
                }
                catch (ArgumentOutOfRangeException e1)
                {
                    phoneTask.PhoneNumber = "NO NUMBERS DEFINED ";
                    phoneTask.Show();
                }

            }
            else
            {
             
                MessageBox.Show("This feature is not available in Free version.", "Buy App", MessageBoxButton.OK);
                MarketplaceDetailTask _marketPlaceDetailTask = new MarketplaceDetailTask();
                _marketPlaceDetailTask.Show();
            }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            

            //Set the data context for this page to the selected contact
            this.DataContext = App.con;
            
           

            try
            {
                //Try to get a picture of the contact
                BitmapImage img = new BitmapImage();
                img.SetSource(App.con.GetPicture());
                Picture.Source = img;

                
            }
            catch (Exception)
            {
                //can't get a picture of the contact
            }
        }

        

        

        
    }
}
