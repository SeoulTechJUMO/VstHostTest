﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using NAudio.Wave;

namespace VstHostTest
{
    sealed class AudioManager : INotifyPropertyChanged
    {
        #region Singleton

        private static readonly Lazy<AudioManager> _instance = new Lazy<AudioManager>(() => new AudioManager());
        private AudioManager()
        {
            PopulateAudioOutputDevices();
        }
        public static AudioManager Instance => _instance.Value;

        #endregion

        private ObservableCollection<IAudioOutputDevice> _outputDevices;
        private IAudioOutputDevice _currentOutputDevice = null;
        private AudioOutputEngine outputEngine = null;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICollectionView OutputDevices { get; private set; }
        public IAudioOutputDevice CurrentOutputDevice
        {
            get => _currentOutputDevice;
            set
            {
                _currentOutputDevice = value;
                outputEngine?.Dispose();
                outputEngine = value == null ? null : new AudioOutputEngine(value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentOutputDevice)));
            }
        }

        private void PopulateAudioOutputDevices()
        {
            if (_outputDevices == null)
            {
                _outputDevices = new ObservableCollection<IAudioOutputDevice>();
            }

            int numOfWaveOutDevices = WaveOut.DeviceCount;
            for (int i = 0; i < numOfWaveOutDevices; i++)
            {
                _outputDevices.Add(new WaveOutDevice(i));
            }

            foreach (var dsDev in DirectSoundOut.Devices)
            {
                _outputDevices.Add(new DirectSoundOutputDevice(dsDev.Guid, dsDev.Description));
            }

            foreach (var asio in AsioOut.GetDriverNames())
            {
                _outputDevices.Add(new AsioOutputDevice(asio));
            }

            OutputDevices = CollectionViewSource.GetDefaultView(_outputDevices);
        }
    }
}
