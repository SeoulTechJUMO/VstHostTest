﻿using System.Windows;
using JUMO.UI.ViewModels;

namespace JUMO.UI.Layouts
{
    /// <summary>
    /// NoteChopperWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class NoteChopperWindow : Window
    {
        public NoteChopperWindow()
        {
            InitializeComponent();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            ChopperViewModel cvm = (ChopperViewModel)DataContext;
            if (!cvm.WillInsert) { cvm.ChopperReset(); }
        }

        private void CheckButtonClick(object sender, RoutedEventArgs e)
        {
            NoteToolsViewModel nvm = (NoteToolsViewModel)DataContext;
            nvm.WillInsert = true;
            Close();
        }

        private void ExitButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
