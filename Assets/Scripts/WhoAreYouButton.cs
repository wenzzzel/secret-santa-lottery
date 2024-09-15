using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class WhoAreYouButton : MonoBehaviour
{
    public TMPro.TMP_Dropdown _whoAreYouDropdown;

    IApiHelper _apiHelper;

    [Inject]
    public void Init(IApiHelper apiHelper)
    {
        _apiHelper = apiHelper;
    }

    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() => OnClick());
    }

    void Update()
    {
        
    }

    void OnClick()
    {
        var selectedParticipantName = _whoAreYouDropdown.options[_whoAreYouDropdown.value].text;

        var allParticipants = _apiHelper.GetParticipants();

        var selectedParticipant = allParticipants.Participants.Find(p => p.Name == selectedParticipantName);

        GlobalVariables.Me = selectedParticipant;
    }
}
