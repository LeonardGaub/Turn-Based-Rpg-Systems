using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    private PlayerDialogue _playerDialogue;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI speakerText;
    [SerializeField] private Button nextButton;
    
    [SerializeField] private Transform choiceParent;
    [SerializeField] private Transform textFieldParent;
    
    [SerializeField] private GameObject choicePrefab;

    private void Start()
    {
        _playerDialogue = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDialogue>();  
        _playerDialogue.OnDialogueUpdated += RedrawUi;
        nextButton.onClick.AddListener(OnNextButtonClick);
        RedrawUi();
    }
    
    private void RedrawUi()
    {
        gameObject.SetActive(_playerDialogue.HasDialogue());
        if(!_playerDialogue.HasDialogue()){return; }
        
        choiceParent.gameObject.SetActive(_playerDialogue.IsChoosing());
        textFieldParent.gameObject.SetActive(!_playerDialogue.IsChoosing());
        speakerText.text = _playerDialogue.GetSpeaker();
        Debug.Log(_playerDialogue.GetSpeaker());
        if (_playerDialogue.IsChoosing())
        {
            DestroyChoiceButtons();
            InstantiateChoiceButtons();   
        }
        else
        {
            dialogueText.text = _playerDialogue.GetText();
            nextButton.gameObject.SetActive(_playerDialogue.HasNext());
        }
    }

    
    private void InstantiateChoiceButtons()
    {
        foreach (DialogueNode choice in _playerDialogue.GetChoices())
        {
            GameObject newChoice = Instantiate(choicePrefab, choiceParent);
            newChoice.GetComponentInChildren<TextMeshProUGUI>().text = choice.GetText();
            Button button = newChoice.GetComponentInChildren<Button>();
            button.onClick.AddListener(() =>
            {
                _playerDialogue.SelectChoice(choice);
            });
        }
    } 

    private void DestroyChoiceButtons()
    {
        foreach (Transform choice in choiceParent)
        {
            Destroy(choice.gameObject);
        }
    }
    
    public void OnNextButtonClick()
    {
        _playerDialogue.Next();
    }

    public void OnQuitButtonClick()
    {
        _playerDialogue.Quit(); 
    }
}
