﻿using System.ComponentModel;

namespace JUMO.UI.ViewModels
{
    public class PatternPlacementViewModel : IMusicalItem, INotifyPropertyChanged
    {
        private bool _updating = false;

        private int _start;
        private int _length;
        private int _trackIndex;

        public event PropertyChangedEventHandler PropertyChanged;

        public PatternPlacement Source { get; }

        public Pattern Pattern { get; }

        public int Start
        {
            get => _start;
            set
            {
                if (_start != value)
                {
                    _start = value;
                    OnPropertyChanged(nameof(Start));
                }
            }
        }

        public int Length
        {
            get => _length;
            set
            {
                if (_length != value)
                {
                    _length = value;
                    OnPropertyChanged(nameof(Length));
                }
            }
        }

        public int TrackIndex
        {
            get => _trackIndex;
            set
            {
                if (_trackIndex != value)
                {
                    _trackIndex = value;
                    OnPropertyChanged(nameof(TrackIndex));
                }
            }
        }

        public PatternPlacementViewModel(PatternPlacement source)
        {
            Source = source;
            Start = Source.Start;
            Length = Source.Length;
            Pattern = Source.Pattern;
            TrackIndex = Source.TrackIndex;

            Source.PropertyChanged += OnSourcePropertyChanged;
        }

        public void UpdateSource()
        {
            _updating = true;

            Source.Start = Start;
            Source.Length = Length;
            Source.TrackIndex = TrackIndex;

            _updating = false;
        }

        private void OnSourcePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (_updating)
            {
                return;
            }

            var srcValue = sender.GetType().GetProperty(e.PropertyName).GetValue(sender);
            GetType().GetProperty(e.PropertyName)?.SetValue(this, srcValue);
        }

        private void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
