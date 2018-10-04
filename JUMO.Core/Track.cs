﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace JUMO
{
    /// <summary>
    /// 패턴을 배치할 수 있는 트랙을 나타냅니다.
    /// </summary>
    public class Track : INotifyPropertyChanged
    {
        private readonly Song _song;
        private string _name;

        #region Properties

        /// <summary>
        /// 트랙의 인덱스 번호를 가져옵니다.
        /// </summary>
        public int Index { get; }

        /// <summary>
        /// 트랙의 이름을 가져오거나 설정합니다.
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        // TODO: It seems to be very inefficient.
        public IEnumerable<PatternPlacement> PlacedPatterns
            => _song.PlacedPatterns.Where(pp => pp.TrackIndex == Index).OrderBy(pp => pp.Start);

        #endregion

        /// <summary>
        /// 새로운 Track 인스턴스를 생성합니다.
        /// </summary>
        /// <param name="song">새 트랙을 소유하는 Song 인스턴스</param>
        /// <param name="index">새 트랙의 인덱스 번호</param>
        /// <param name="name">새 트랙의 이름</param>
        public Track(Song song, int index, string name)
        {
            _song = song ?? throw new ArgumentNullException(nameof(song));
            Index = index;
            Name = name;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
