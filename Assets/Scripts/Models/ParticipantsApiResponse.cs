using System.Collections.Generic;

public class ParticipantsApiResponse
{
    public List<Participant> Participants { get; set; }
}

public class Participant 
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Partner { get; set; }
    public string SantaFor { get; set; }
    public bool AlreadyTaken { get; set; }
}