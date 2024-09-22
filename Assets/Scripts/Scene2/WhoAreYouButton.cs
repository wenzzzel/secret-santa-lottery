using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class WhoAreYouButton : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_Dropdown _whoAreYouDropdown;

    private IApiHelper _apiHelper;

    [Inject]
    public void Init(IApiHelper apiHelper) //Can this be private?
    {
        _apiHelper = apiHelper;
    }

    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() => OnClick());
    }

    private void Update()
    {
        
    }

    private void OnClick()
    {
        var selectedParticipantName = _whoAreYouDropdown.options[_whoAreYouDropdown.value].text;

        var allParticipants = _apiHelper.GetParticipants();

        var selectedParticipant = allParticipants.Participants.Find(p => p.Name == selectedParticipantName);

        GlobalVariables.Me = selectedParticipant;
    }
}
