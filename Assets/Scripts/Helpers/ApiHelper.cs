using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using UnityEngine;

public interface IApiHelper
{
    ParticipantsApiResponse GetParticipants();
    // HttpStatusCode DeleteParticipantWithDelay(string participantId, string participantName, string partner, string santaForId);
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
        // var response = await client.GetAsync("https://wenzelapiman.azure-api.net/participant");
        var response = _client.GetAsync("http://localhost:5000/Participant");

        response.Result.EnsureSuccessStatusCode();

        var content = response.Result.Content.ReadAsStringAsync();
        
        var webResponse = JsonSerializer.Deserialize<ParticipantsApiResponse>(content.Result, _jsonSerializerOptions);
        
        return webResponse;
    }

    // public HttpStatusCode DeleteParticipantWithDelay(string participantId, string participantName, string partner, string santaForId)
    // {
    //     Task.Delay(3000).Wait();

    //     var response = _client.DeleteAsync($"http://localhost:5000/Participant?id={participantId}&name={participantName}&partner={partner}&santaForId={santaForId}");
    //     // var response = _client.DeleteAsync($"https://wenzelapiman.azure-api.net/Participant?id={participantId}&name={participantName}&partner={partner}&santaForId={santaForId}");

    //     response.Result.EnsureSuccessStatusCode();
        
    //     Debug.Log($"Participant to delete has id: {participantId} and name: {participantName}.");
        
    //     return response.Result.StatusCode;
    // }

    //TODO: THis is not working. Most likely something in the API because I get 400 from CosmosDB
    public HttpStatusCode UpdateParticipant(Participant participant)
    {
        var id = GlobalVariables.Me.Id;
        var name = GlobalVariables.Me.Name;
        var partner = GlobalVariables.Me.Partner;
        var santaForId = GlobalVariables.Me.SantaForId;

        var response = _client.PutAsync($"http://localhost:5000/Participant?Id={id}&Name={name}&Partner={partner}&SantaForId={santaForId}", 
                            new StringContent(JsonSerializer.Serialize(participant)
                            , System.Text.Encoding.UTF8, "application/json"));

        return response.Result.StatusCode;
    }
}
