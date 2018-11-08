﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace JUMO.UI.Views
{
    public partial class PatternNameView : UserControl
    {
        #region Dependency Properties

        public static readonly DependencyProperty PatternNameProperty =
            DependencyProperty.Register("PatternName", typeof(string), typeof(PatternNameView));

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(PatternNameView));

        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(PatternNameView));

        #endregion

        #region Properties

        public string PatternName
        {
            get => (string)GetValue(PatternNameProperty);
            set => SetValue(PatternNameProperty, value);
        }

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public bool IsOpen
        {
            get => (bool)GetValue(IsOpenProperty);
            set
            {
                SetValue(IsOpenProperty, value);
            }
        }

        #endregion

        public PatternNameView()
        {
            InitializeComponent();
            Popup.DataContext = this;
            nameTextBox.Focus();
            nameTextBox.SelectAll();
        }

        private void PopupCancelButtonClick(object sender, RoutedEventArgs e)
        {
            IsOpen = false;
        }
    }
}
