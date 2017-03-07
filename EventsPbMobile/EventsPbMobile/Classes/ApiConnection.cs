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
        private readonly string ActivitiesUrl = "http://webapipb.azurewebsites.net/api/activities";
        private readonly HttpClient client = new HttpClient();
        private readonly string EventsUrl = "http://webapipb.azurewebsites.net/api/events";
        private readonly string PhotoEventsUrl = "http://webapipb.azurewebsites.net/api/photoevents";
        private readonly string PhotosUrl = "http://webapipb.azurewebsites.net/api/photos";
        private readonly string PlacessUrl = "http://webapipb.azurewebsites.net/api/places";

        private readonly string Url = "http://webapipb.azurewebsites.net";

        public void ConnectToAPI()
        {
            client.BaseAddress = new Uri(Url);
            var request = new HttpRequestMessage(HttpMethod.Post, "/oauth/token");
            var requestContent = "grant_type=password&username=politechnikamobile@gmail.com&password=Mobile123";
            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/x-www-form-urlencoded");
            var x = client.SendAsync(request);
        }

        public async Task<List<Event>> GetEventsAllAsync()
        {
            ConnectToAPI();
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
            ConnectToAPI();
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
            ConnectToAPI();
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
            ConnectToAPI();
            var url = PhotosUrl + "/" + id;
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
            ConnectToAPI();
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
            var url = ActivitiesUrl + "/" + id;
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
            var url = PlacessUrl + "/" + id;
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

        public async Task<List<PhotoEvent>> GetPhotoEventsAllAsync()
        {
            ConnectToAPI();
            var uri = new Uri(PhotosUrl);
            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var items = JsonConvert.DeserializeObject<List<PhotoEvent>>(content);
                return items;
            }

            return null;
        }
    }
}