using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EventsPbMobile.Models;
using Newtonsoft.Json;
using Plugin.Connectivity;

namespace EventsPbMobile.Classes
{
    internal class ApiConnection
    {
        private readonly string ActivitiesUrl = "http://imprezy.pb.edu.pl/api/activities";
        private readonly HttpClient client = new HttpClient();
        private readonly string EventsUrl = "http://imprezy.pb.edu.pl/api/events";
        private readonly string PhotoEventsUrl = "http://imprezy.pb.edu.pl/api/photoevents";   
        private readonly string PhotosUrl = "http://imprezy.pb.edu.pl/api/photos";
        private readonly string PlacessUrl = "http://imprezy.pb.edu.pl/api/places";

        private readonly string Url = "http://imprezy.pb.edu.pl/api/";

        public async Task<List<Event>> GetEventsAllAsync()
        {
            var uri = new Uri(EventsUrl);
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var items = JsonConvert.DeserializeObject<List<Event>>(content);
                return items;
            }
            return null;
        }

        public async Task<Event> GetEvent_byIdAsync(int id)
        {
            var url = EventsUrl + "/" + id;
            var uri = new Uri(url);
            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var item = JsonConvert.DeserializeObject<Event>(content);
                return item;
            }

            return null;
        }

        public async Task<List<Photo>> GetPhotosAllAsync()
        {
            var uri = new Uri(PhotosUrl);
            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var items = JsonConvert.DeserializeObject<List<Photo>>(content);
                return items;
            }

            return null;
        }

        public async Task<List<Activity>> GetActivitiesAllAsync()
        {
            var uri = new Uri(ActivitiesUrl);
            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var items = JsonConvert.DeserializeObject<List<Activity>>(content);
                return items;
            }

            return null;
        }

        public async Task<List<Place>> GetPlacesAllAsync()
        {
            var uri = new Uri(PlacessUrl);
            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var items = JsonConvert.DeserializeObject<List<Place>>(content);
                return items;
            }

            return null;
        }
    }
}