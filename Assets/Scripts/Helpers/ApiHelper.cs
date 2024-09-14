using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using UnityEngine;

public interface IApiHelper
{
    ParticipantApiResponse GetParticipants();
    HttpStatusCode DeleteParticipantWithDelay(string participantId, string participantName, string partner, string santaForId);
}

public class ApiHelper : IApiHelper
{
    private HttpClient _client = new HttpClient();
    private JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    };

    public ParticipantApiResponse GetParticipants()
    {
        // var response = await client.GetAsync("https://wenzelapiman.azure-api.net/participant");
        var response = _client.GetAsync("http://localhost:5000/Participant");

        response.Result.EnsureSuccessStatusCode();

        var content = response.Result.Content.ReadAsStringAsync();
        
        var webResponse = JsonSerializer.Deserialize<ParticipantApiResponse>(content.Result, _jsonSerializerOptions);
        
        return webResponse;
    }

    public HttpStatusCode DeleteParticipantWithDelay(string participantId, string participantName, string partner, string santaForId)
    {
        Task.Delay(3000).Wait();

        var response = _client.DeleteAsync($"http://localhost:5000/Participant?id={participantId}&name={participantName}&partner={partner}&santaForId={santaForId}");
        // var response = _client.DeleteAsync($"https://wenzelapiman.azure-api.net/Participant?id={participantId}&name={participantName}&partner={partner}&santaForId={santaForId}");

        response.Result.EnsureSuccessStatusCode();
        
        Debug.Log($"Participant to delete has id: {participantId} and name: {participantName}.");
        
        return response.Result.StatusCode;
    }
}
