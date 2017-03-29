using System;

namespace EventsPbMobile.Models
{
    public class MenuItem
    {
        private bool _isSelected;
        public string Title { get; set; }

        public string IconSource { get; set; }

        public Type TargetType { get; set; }
    }
}