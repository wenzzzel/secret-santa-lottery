using UnityEngine;
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

    private void Start()
    {
        _dropdown = GetComponent<TMPro.TMP_Dropdown>();

        var participantsWebResponse = _apiHelper.GetParticipants();

        _dropdown.options.Clear();

        foreach (var participant in participantsWebResponse.Participants)
            _dropdown.options.Add(new(participant.Name));

        _dropdown.RefreshShownValue();
    }

    private void Update()
    {

    }
}
