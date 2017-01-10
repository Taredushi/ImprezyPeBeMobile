using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using EventsPbMobile.Models;
using Newtonsoft.Json;

namespace EventsPbMobile.Classes
{
    class ApiConnection
    {
        private HttpClient client = new HttpClient();
        private string EventsUrl = "http://webapipb.azurewebsites.net/api/events";
        private string PhotosUrl = "http://webapipb.azurewebsites.net/api/photos";
        private string ActivitiesUrl = "http://webapipb.azurewebsites.net/api/activities";
        private string PlacessUrl = "http://webapipb.azurewebsites.net/api/places";

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
            string url = EventsUrl + "/" + id;
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

        public async Task<Photo> GetPhoto_byIdAsync(int id)
        {
            string url = PhotosUrl + "/" + id;
            var uri = new Uri(url);
            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var item = JsonConvert.DeserializeObject<Photo>(content);
                return item;
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

        public async Task<Activity> GetActivity_byIdAsync(int id)
        {
            string url = ActivitiesUrl + "/" + id;
            var uri = new Uri(url);
            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var item = JsonConvert.DeserializeObject<Activity>(content);
                return item;
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

        public async Task<Place> GetPlace_byIdAsync(int id)
        {
            string url = PlacessUrl + "/" + id;
            var uri = new Uri(url);
            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var item = JsonConvert.DeserializeObject<Place>(content);
                return item;
            }

            return null;
        }
    }
}
