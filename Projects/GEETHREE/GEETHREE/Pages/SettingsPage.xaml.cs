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
using Microsoft.Phone.Tasks;
using System.Windows.Media.Imaging;
using System.IO.IsolatedStorage;


namespace GEETHREE.Pages
{
    public partial class SettingsPage : PhoneApplicationPage
    {
        PhotoChooserTask photoChooserTask;
        CameraCaptureTask cameraCaptureTask;
        Controller ctrl;

        public SettingsPage()
        {
            InitializeComponent();
            ctrl = Controller.Instance;

            ctrl.registerAvatarUpdates(this);

            refreshAvatar();

            // Photochoosertask : initializes the task object, and identifies the method to run after the user completes the task
            photoChooserTask = new PhotoChooserTask();
            photoChooserTask.Completed += new EventHandler<PhotoResult>(photoChooserTask_Completed);

            //Cameracapturetask : initializes the task object, and identifies the method to run after the user completes the task.
            cameraCaptureTask = new CameraCaptureTask();
            cameraCaptureTask.Completed += new EventHandler<PhotoResult>(cameraCaptureTask_Completed);
        }

        public void refreshAvatar(){
            img_Settings_avatar.Source = ctrl.getCurrentAvatar();
        }

        private void img_Settings_avatar_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {

                photoChooserTask.Show();
            }
            catch (System.InvalidOperationException ex)
            {
                MessageBox.Show("An error occurred.");
            }
        }

        private void img_Settings_camera_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {
                cameraCaptureTask.Show();
            }
            catch (System.InvalidOperationException ex)
            {
                MessageBox.Show("An error occurred.");
            }

        }

        //browses for the photos and gets the picture in imagebox after selection
        void photoChooserTask_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                //Write image to isolated storage
                ctrl.changeCurrentAvatar(e.ChosenPhoto);

                //appSetting.SaveToIsolatedStorage(e.ChosenPhoto, "Avatar.jpg");

                //display image on imagebox from isolated storage
                //img_Settings_avatar.Source = appSetting.ReadFromIsolatedStorage("Avatar.jpg");
               // img_Base_Avatar.Source = appSetting.ReadFromIsolatedStorage("Avatar.jpg");
            }
        }

        //Captures the picture using the camera and gets the picture in the imagebox 
        void cameraCaptureTask_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                //Write image to isolated storage
                ctrl.changeCurrentAvatar(e.ChosenPhoto);
            }
        }
    }
}