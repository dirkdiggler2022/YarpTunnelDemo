namespace YarpTunnelDemo.Tests
{
    public class UnitTest1
    {
        public const string PUBLIC_SERVER_URL = "http://localhost:7244";
        public const string PUBLIC_SERVER_H2_ENDPOINT = "h2-connect";
        [Fact]
        public async Task Test1()
        {
            using (HttpClient client = new HttpClient())
            {

                HttpResponseMessage response = await client.PostAsync(new Uri($"{PUBLIC_SERVER_URL}/{PUBLIC_SERVER_H2_ENDPOINT}"), new StringContent(""));
                //HttpResponseMessage response = await client.GetAsync("http://localhost:5244/");
                response.EnsureSuccessStatusCode(); // Ensure success status code, throws exception otherwise

                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);


            }
        }
    }
}