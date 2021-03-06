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
using Telerik.Windows.Controls;
using PoliceSMS.Lib.SMS;
using PoliceSMS.ViewModel;

namespace PoliceSMS.Views
{
    public partial class GradeTypeForm : UserControl
    {
        GradeType obj = new GradeType();

        Action refreshAction;

        public GradeTypeForm(Action refreshAction)
        {
            InitializeComponent();
            this.refreshAction = refreshAction;
            this.Loaded += new RoutedEventHandler(GradeTypeForm_Loaded);
        }

        public GradeTypeForm(GradeType editObj, Action refreshAction)
            : this(refreshAction)
        {
            //复制可能被改变的属性，避免退出修改的刷新
            obj.Id = editObj.Id;
            obj.Name = editObj.Name;
            obj.Number = editObj.Number;
            obj.Score = editObj.Score;
            obj.IsUsed = editObj.IsUsed;
        }

        void GradeTypeForm_Loaded(object sender, RoutedEventArgs e)
        {
            comboUsed.ItemsSource = UsedState.CreateAry();
            this.DataContext = obj;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            GradeTypeService.GradeTypeServiceClient ser = new GradeTypeService.GradeTypeServiceClient();
            ser.SaveOrUpdateCompleted += new EventHandler<GradeTypeService.SaveOrUpdateCompletedEventArgs>(ser_SaveOrUpdateCompleted);
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            ser.SaveOrUpdateAsync(json);
        }

        void ser_SaveOrUpdateCompleted(object sender, GradeTypeService.SaveOrUpdateCompletedEventArgs e)
        {
            (this.Parent as RadWindow).Close();
            if (refreshAction != null)
                refreshAction();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as RadWindow).Close();
        }
    }
}
