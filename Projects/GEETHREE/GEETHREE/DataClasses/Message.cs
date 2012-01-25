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
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using Microsoft.Phone.Data.Linq;
using Microsoft.Phone.Data.Linq.Mapping;
using System.Collections.Generic;

namespace GEETHREE.DataClasses
{
    [Table]
    public class Message : INotifyPropertyChanged
    {
        // Define ID: private field, public property, and database column.
        private int _msgDbId;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int msgDbId
        {
            get
            {
                return _msgDbId;
            }
            set
            {
            }
        }

        [Column]
        private string _header;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string Header
        {
            get
            {
                return _header;
            }
            set
            {
                if (value != _header)
                {
                    _header = value;
                    NotifyPropertyChanged("Header");
                }
            }
        }

        [Column]
        private string _textContent;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string TextContent 
        {
            get
            {
                return _textContent;
            }
            set
            {
                if (value != _textContent)
                {
                    _textContent = value;
                    NotifyPropertyChanged("TextContent");
                }
            }
        }
        [Column]
        private string _senderid;

        public string SenderID
        {
            get
            {
                return _senderid;
            }
            set
            {
                if (value != _senderid)
                {
                    _senderid = value;
                    NotifyPropertyChanged("SenderID");
                }
            }
        }
        [Column]
        private string _receiverid;

        public string ReceiverID
        {
            get
            {
                return _receiverid;
            }
            set
            {
                if (value != _receiverid)
                {
                    _receiverid = value;
                    NotifyPropertyChanged("ReceiverID");
                }
            }
        }
        [Column]
        private byte[] _hash;

        public byte[] Hash
        {
            get
            {
                return _hash;
            }
            set
            {
                if (value != _hash)
                {
                    _hash = value;
                    NotifyPropertyChanged("Hash");
                }
            }
        }

        [Column]
        private bool _privatemessage;

        public bool PrivateMessage
        {
            get
            {
                return _privatemessage;
            }
            set
            {
                if (value != _privatemessage)
                {
                    _privatemessage = value;
                    NotifyPropertyChanged("PrivateMessage");
                }
            }
        }
 

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
