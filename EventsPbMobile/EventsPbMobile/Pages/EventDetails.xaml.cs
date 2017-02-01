﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using EventsPbMobile.Models;
using Xamarin.Forms;

namespace EventsPbMobile.Pages
{
    public partial class EventDetails
    {
        private readonly Event _event;

        public EventDetails(Event e)
        {
            _event = e;
            InitializeComponent();
            Title = e.Title;
            Counter();
            InitFavButton();
        }

        private async void Counter()
        {
            var rt = _event.Date.Subtract(DateTime.Now);
            while (rt.TotalSeconds > 0)
            {
                rt = _event.Date.Subtract(DateTime.Now);
                TitleLabel.Text = "Start za: " + rt.Days + " dni, " + rt.Hours + ":" + rt.Minutes + ":" + rt.Seconds;
                await Task.Delay(250);
            }
        }

        private async void MapButton_OnClicked(object sender, EventArgs e)
        {
            var activities = _event.Activities;
            foreach (var activity in activities)
                Debug.WriteLine(activity.Place == null ? "NULL" : "NIE NULL");
            await Navigation.PushAsync(new EventMap(activities));
        }

        private void Notification_Clicked(object sender, EventArgs e)
        {
            if (Settings.IsToggled)
                App.Notification.ShowNotification("Nadchodzące wydarzenie", _event.Title);
        }

        private void InitFavButton()
        {
            ToolbarItems.Add(new ToolbarItem("Remind", "alert.png", () =>
            {
                
            }));
        }
    }
}