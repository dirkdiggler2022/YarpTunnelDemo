namespace YarpTunnelDemo.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync("http://localhost:5244/");
                    response.EnsureSuccessStatusCode(); // Ensure success status code, throws exception otherwise

                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseBody);
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Request exception: {e.Message}");
                }
            }
        }
    }
}