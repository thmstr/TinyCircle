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

namespace GEETHREE.Networking
{
    public class Connection
    {

        public Connection(string userID, IPEndPoint endPoint)
        {
            UserEndPoint = endPoint;
            UserID = userID;
            IsSynchronized = false;
        }

        public string UserID { get; set; }
        public IPEndPoint UserEndPoint { get; set; }
        public bool IsSynchronized { get; set; }
    }
}