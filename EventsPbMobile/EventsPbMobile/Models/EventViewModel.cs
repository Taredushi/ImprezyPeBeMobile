using System;
using System.ComponentModel;

namespace EventsPbMobile.Models
{
    public class EventViewModel : INotifyPropertyChanged
    {
        public Event Event { get; set; }

        private TimeSpan _timeLeft;

        public TimeSpan TimeLeft {
            get { return _timeLeft; }
            set
            {
                _timeLeft = value;
                OnPropertyChanged("Countdown");
            }
        }

        public string Countdown
        {
            get { return $"{TimeLeft:dd\\:hh\\:mm\\:ss}"; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
