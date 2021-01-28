using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Sample
{
    public class User : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Name { get; set; } = "Sam";
        public string Family { get; set; } = "Washington";
        public DateTime DateBirth { get; set; } = new DateTime(1992, 6, 10);
        public string Email { get; set; } = "samwww3@gmail.com";
        public string AboutMe { get; set; } = 
            "Works hard all week so I can take off for the mountain on the weekend, loves my dog Eddie, loves to argue on first dates, isn't afraid to make a fool of myself on a dance floor";
    }
}
