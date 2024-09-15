using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class WhoAreYouDropdown : MonoBehaviour
{
    TMPro.TMP_Dropdown _dropdown;

    IApiHelper _apiHelper;

    [Inject]
    public void Init(IApiHelper apiHelper)
    {
        _apiHelper = apiHelper;
    }

    void Start()
    {
        _dropdown = GetComponent<TMPro.TMP_Dropdown>();

        var participantsWebResponse = _apiHelper.GetParticipants();

        _dropdown.options.Clear();

        foreach (var participant in participantsWebResponse.Participants)
            _dropdown.options.Add(new(participant.Name));

        _dropdown.RefreshShownValue();
    }

    void Update()
    {

    }
}
