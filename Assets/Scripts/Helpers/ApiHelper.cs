using System.Net;
using System.Net.Http;
using System.Text.Json;

public interface IApiHelper
{
    ParticipantsApiResponse GetParticipants();
    HttpStatusCode UpdateParticipant(Participant participant);
}

public class ApiHelper : IApiHelper
{
    private HttpClient _client = new HttpClient();
    private JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    };

    public ParticipantsApiResponse GetParticipants()
    {
        var response = _client.GetAsync("http://localhost:5000/Participants");

        response.Result.EnsureSuccessStatusCode();

        var content = response.Result.Content.ReadAsStringAsync();
        
        var webResponse = JsonSerializer.Deserialize<ParticipantsApiResponse>(content.Result, _jsonSerializerOptions);
        
        return webResponse;
    }

    public HttpStatusCode UpdateParticipant(Participant participant)
    {
        var json = JsonSerializer.Serialize(participant);

        var body = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

        var response = _client.PutAsync($"http://localhost:5000/Participants", body);

        return response.Result.StatusCode;
    }
}
