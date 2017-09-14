namespace AGL.Client.Tests
{
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class AglClientTests
    {
        private const string PeopleJsonStringForTesting =
            "[{\"name\":\"Bob\",\"gender\":\"Male\",\"age\":23,\"pets\":[{\"name\":\"Garfield\",\"type\":\"Cat\"},{\"name\":\"Fido\",\"type\":\"Dog\"}]},{\"name\":\"Jennifer\",\"gender\":\"Female\",\"age\":18,\"pets\":[{\"name\":\"Garfield\",\"type\":\"Cat\"}]},{\"name\":\"Steve\",\"gender\":\"Male\",\"age\":45,\"pets\":null},{\"name\":\"Fred\",\"gender\":\"Male\",\"age\":40,\"pets\":[{\"name\":\"Tom\",\"type\":\"Cat\"},{\"name\":\"Max\",\"type\":\"Cat\"},{\"name\":\"Sam\",\"type\":\"Dog\"},{\"name\":\"Jim\",\"type\":\"Cat\"}]},{\"name\":\"Samantha\",\"gender\":\"Female\",\"age\":40,\"pets\":[{\"name\":\"Tabby\",\"type\":\"Cat\"}]},{\"name\":\"Alice\",\"gender\":\"Female\",\"age\":64,\"pets\":[{\"name\":\"Simba\",\"type\":\"Cat\"},{\"name\":\"Nemo\",\"type\":\"Fish\"}]}]";


        [Fact]
        public void CanExtractPeopleFromJsonString()
        {
            var allPeople = AGLClient.ExtractPeopleFromJsonString(PeopleJsonStringForTesting);

            const int expectedNumberOfPeople = 6;
            Assert.Equal(expectedNumberOfPeople, allPeople.Count);

            var expectedGenders = new[] { "Female", "Male" };
            var orderedPeopleGenders = allPeople.Select(p => p.Gender).Distinct().OrderBy(x => x).ToArray();
            Assert.Equal(expectedGenders, orderedPeopleGenders);
        }

        [Fact]
        public void CanGetCatNamesFromFemaleOwners()
        {
            var allPeople = AGLClient.ExtractPeopleFromJsonString(PeopleJsonStringForTesting);
            var catNamesOrdered = AGLClient.PetNamesOrdered(allPeople, "Female", "Cat");

            const int expectedNumberOfFemaleOwnedCats = 3;
            Assert.Equal(expectedNumberOfFemaleOwnedCats, catNamesOrdered.Count);
        }

        [Fact]
        public void CanGetCatNamesFromMaleOwners()
        {
            var allPeople = AGLClient.ExtractPeopleFromJsonString(PeopleJsonStringForTesting);
            var catNamesOrdered = AGLClient.PetNamesOrdered(allPeople, "Male", "Cat");

            const int expectedNumberOfMaleOwnedCats = 4;
            Assert.Equal(expectedNumberOfMaleOwnedCats, catNamesOrdered.Count);
        }

        [Fact]
        public async Task CanGetAllPeopleFromApiAsync()
        {
            var aglClient = new AGLClient();
            var allPeople = await aglClient.GetAllPeopleAsync();

            var expectedGenders = new[] { "Female", "Male" };
            var orderedPeopleGenders = allPeople.Select(p => p.Gender).Distinct().OrderBy(x => x).ToArray();
            Assert.Equal(expectedGenders, orderedPeopleGenders);
        }
    }
}
