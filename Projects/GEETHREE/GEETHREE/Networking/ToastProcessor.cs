﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Notification;
using System.Text;
using System.Collections.Generic;

namespace GEETHREE.Networking
{
    public class ToastProcessor
    {
        private string userId;
        private List<PushListener> pushListeners;

        public ToastProcessor(string userId)
        {
            this.userId = userId;
            pushListeners = new List<PushListener>();
        }

        public void registerPushListener(PushListener ps)
        {
            pushListeners.Add(ps);
        }

        public void registerToast()
        {
            //Open web service for notifications
            WebServiceConnector ws = new WebServiceConnector();

            // Holds the push channel that is created or found.
            HttpNotificationChannel pushChannel;

            //Set channel name
            string channelName = "TinyCircleNewMessageChannel";

            // Try to find the push channel.
            pushChannel = HttpNotificationChannel.Find(channelName);

            // If the channel was not found, then create a new connection to the push service.
            if (pushChannel == null)
            {
                pushChannel = new HttpNotificationChannel(channelName);

                // Register for all the events before attempting to open the channel.
                pushChannel.ChannelUriUpdated += new EventHandler<NotificationChannelUriEventArgs>(PushChannel_ChannelUriUpdated);
                pushChannel.ErrorOccurred += new EventHandler<NotificationChannelErrorEventArgs>(PushChannel_ErrorOccurred);

                // Register for this notification only if you need to receive the notifications while your application is running.
                pushChannel.ShellToastNotificationReceived += new EventHandler<NotificationEventArgs>(PushChannel_ShellToastNotificationReceived);

                pushChannel.Open();

                // Bind this new channel for toast events.
                pushChannel.BindToShellToast();
            }
            else
            {
                // The channel was already open, so just register for all the events.
                pushChannel.ChannelUriUpdated += new EventHandler<NotificationChannelUriEventArgs>(PushChannel_ChannelUriUpdated);
                pushChannel.ErrorOccurred += new EventHandler<NotificationChannelErrorEventArgs>(PushChannel_ErrorOccurred);

                // Register for this notification only if you need to receive the notifications while your application is running.
                pushChannel.ShellToastNotificationReceived += new EventHandler<NotificationEventArgs>(PushChannel_ShellToastNotificationReceived);

                try
                {
                    //Display the URI for testing purposes
                    System.Diagnostics.Debug.WriteLine(pushChannel.ChannelUri.ToString());

                    //Registering the URI in the web service
                    ws.registerToast(pushChannel.ChannelUri.ToString(), userId);
                }
                catch (Exception ex)
                {

                    System.Diagnostics.Debug.WriteLine("TP: Failed to register toast in web service " + ex.ToString());
                }
            }
        }

        private void PushChannel_ChannelUriUpdated(object sender, NotificationChannelUriEventArgs e)
        {
            //Open web service for notifications
            WebServiceConnector ws = new WebServiceConnector();

            try
            {
                //Getting channel URI
                string ChannelUri = e.ChannelUri.ToString();

                //Display the URI for testing purposes
                System.Diagnostics.Debug.WriteLine(ChannelUri);

                //Registering the URI in the web service
                ws.registerToast(ChannelUri, userId);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("TP: Failed to register toast in web service " + ex.ToString());
            }
        }

        private void PushChannel_ErrorOccurred(object sender, NotificationChannelErrorEventArgs e)
        {

        }

        private void PushChannel_ShellToastNotificationReceived(object sender, NotificationEventArgs e)
        {
            //StringBuilder message = new StringBuilder();
            //string relativeUri = string.Empty;

            //message.AppendFormat("Received Toast {0}:\n", DateTime.Now.ToShortTimeString());
            System.Diagnostics.Debug.WriteLine("TP: Received push notification from server");
            System.Diagnostics.Debug.WriteLine("TP: Notifying " + pushListeners.Count.ToString() + " listeners");

            foreach (PushListener ps in pushListeners)
            {
                ps.ReceivedServerPush();
            }

            // Parse out the information that was part of the message.
            //foreach (string key in e.Collection.Keys)
            //{
            //    message.AppendFormat("{0}: {1}\n", key, e.Collection[key]);

            //    if (string.Compare(
            //        key,
            //        "wp:Param",
            //        System.Globalization.CultureInfo.InvariantCulture,
            //        System.Globalization.CompareOptions.IgnoreCase) == 0)
            //    {
            //        relativeUri = e.Collection[key];
            //    }
            //}

            // Display a dialog of all the fields in the toast.
            //Dispatcher.BeginInvoke(() => MessageBox.Show(message.ToString()));
        }
    }
}
