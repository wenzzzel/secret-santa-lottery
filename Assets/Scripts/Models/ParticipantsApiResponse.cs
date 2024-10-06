using System.Collections.Generic;

[System.Serializable]
public class ParticipantsApiResponse
{
    public List<Participant> participants;
}

[System.Serializable]
public class Participant 
{
    public string id;
    public string name;
    public string partner;
    public string santaFor;
    public bool alreadyTaken;
}