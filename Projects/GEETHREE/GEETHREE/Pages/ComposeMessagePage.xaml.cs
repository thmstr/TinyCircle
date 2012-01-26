﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using GEETHREE.DataClasses;
using Microsoft.Phone.Tasks;
using System.Windows.Media.Imaging;

namespace GEETHREE.Pages
{
    public partial class ComposeMessagePage : PhoneApplicationPage
    {
        Controller ctrl;
        PhotoChooserTask photoChooserTask;
        CameraCaptureTask cameraCaptureTask;
        public ComposeMessagePage()
        {
            
            InitializeComponent();
            DataContext = App.ViewModel;

            ctrl = Controller.Instance;
            ctrl.registerCurrentPage(this, "compose");
 
            foreach (User u in App.ViewModel.Users)
            {
                receiverListPicker.Items.Add(u.UserName);
            }

            // Photochoosertask : initializes the task object, and identifies the method to run after the user completes the task
            photoChooserTask = new PhotoChooserTask();
            photoChooserTask.Completed += new EventHandler<PhotoResult>(photoChooserTask_Completed);

            //Cameracapturetask : initializes the task object, and identifies the method to run after the user completes the task.
            cameraCaptureTask = new CameraCaptureTask();
            cameraCaptureTask.Completed += new EventHandler<PhotoResult>(cameraCaptureTask_Completed);

        }

        private void image1_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            receiverListPicker.Visibility = System.Windows.Visibility.Visible;
        }

        private void receiverListPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txt_compose_receipient.Text = receiverListPicker.SelectedItem.ToString();
            receiverListPicker.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void txt_compose_message_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (txt_compose_message.Text == "Type your message here...")
            txt_compose_message.Text = "";
        }

        // start camera
        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {

            cameraCaptureTask.Show();
   
        }

        // ** start picture browser
        private void ApplicationBarIconButton_Click_1(object sender, EventArgs e)
        {
            photoChooserTask.Show();
        }

        //  send this message
        private void ApplicationBarIconButton_Click_2(object sender, EventArgs e)
        {
            // ** ... communicate with networker to create a packet and send it ...
            Message msg =new Message();
            msg.TextContent=txt_compose_message.Text;
            msg.SenderID=Controller.Instance.getCurrentUserID();
            msg.ReceiverID="0";
            msg.PrivateMessage=false;
            msg.outgoing=true;
            Controller.Instance.mh.SendMessage(msg);
            MessageBox.Show("Message sent.");
        }

        //browses for the photos and gets the picture in imagebox after selection
        void photoChooserTask_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                // ...communication with the isolated storage...

                BitmapImage bitImage = new BitmapImage();
	            bitImage.SetSource(e.ChosenPhoto);
                attachedImage.Source = bitImage;

                //Write image to isolated storage
                //ctrl.changeCurrentAvatar(e.ChosenPhoto);
            }
        }

        //Captures the picture using the camera and gets the picture in the imagebox 
        void cameraCaptureTask_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                // ...communication with the isolated storage...

                BitmapImage bitImage = new BitmapImage();
                bitImage.SetSource(e.ChosenPhoto);
                attachedImage.Source = bitImage;

                //Write image to isolated storage
                //ctrl.changeCurrentAvatar(e.ChosenPhoto);
            }
        }
        // ** some kind of popup needed to announce about the message that is just arrived
        public void messageArrived()
        {
            // **  ...get the message from datamaster and display it in canvas.
            var m = MessageBox.Show("Read it?", "You have reveived a message.", MessageBoxButton.OKCancel);

            if (m == MessageBoxResult.OK)
            {
                NavigationService.Navigate(new Uri("/Pages/MessagesPage.xaml", UriKind.Relative));

            }
        }


    }
}