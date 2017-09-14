namespace AGL.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Net.Http;
    using ServiceStack.Text;

    // ReSharper disable once InconsistentNaming
    public class AGLClient
    {
        private readonly HttpClient _httpClient;

        private const string ServiceBaseUri = "http://agl-developer-test.azurewebsites.net/";
        private const int TimeoutSeconds = 60;

        public AGLClient()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(ServiceBaseUri),
                Timeout = TimeSpan.FromSeconds(TimeoutSeconds)
            };
        }

        public async Task<List<Person>> GetAllPeopleAsync()
        {
            var response = await _httpClient.GetAsync("people.json");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Would actually do logging etc");
            }
            var resultContents = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return ExtractPeopleFromJsonString(resultContents);
        }

        public static List<Person> ExtractPeopleFromJsonString(string jsonString)
        {
            var allPeople = JsonSerializer.DeserializeFromString<List<Person>>(jsonString);
            return allPeople;
        }

        public static List<string> PetNamesOrdered(IEnumerable<Person> allPeople, string personGender, string petType)
        {
            var people = allPeople.Where(p => p.Gender == personGender);
            var pets = people
                .Where(p => p.Pets != null)
                .SelectMany(p => p.Pets).ToList(); // .ToList() to force enumeration (really for debugging)

            var catNamesOrdered = pets.Where(pet => pet.Type == petType).Select(pet => pet.Name).OrderBy(x => x).ToList();
            return catNamesOrdered;
        }
    }
}
