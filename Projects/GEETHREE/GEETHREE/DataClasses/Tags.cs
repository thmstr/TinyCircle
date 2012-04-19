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
    public class Tags : INotifyPropertyChanged
    {

        // Define ID: private field, public property, and database column.
        private int _tagDbId;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int tagDbId
        {
            get
            {
                return _tagDbId;
            }
            set
            {
                if (_tagDbId != value)
                {
                    _tagDbId = value;
                    NotifyPropertyChanged("tagDbId");
                }
            }
        }

        private string _tagName;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        ///
        [Column]
        public string TagName
        {
            get
            {
                return _tagName;
            }
            set
            {
                if (value != _tagName)
                {
                    _tagName = value;
                    NotifyPropertyChanged("TagName");
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