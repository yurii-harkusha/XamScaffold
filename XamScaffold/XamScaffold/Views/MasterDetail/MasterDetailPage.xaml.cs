﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamScaffold.Views.MasterDetail
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailPage : Xamarin.Forms.MasterDetailPage
    {
        public MasterDetailPage()
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterDetailPageMasterMenuItem;
            if (item == null)
                return;
            var itemTargetType = item.TargetType;
            IsPresented = false;
            MasterPage.ListView.SelectedItem = null;
        }
    }
}