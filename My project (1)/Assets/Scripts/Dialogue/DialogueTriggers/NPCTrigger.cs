using UnityEngine;
using UnityEngine.InputSystem;

public class NPCTrigger : MonoBehaviour
{
    [SerializeField] private TextAsset _inkJSON;

    private bool _isPlayerEnter;

    private DialogueController _dialogueController;
    private DialogueWindow _dialogueWindow;
    private InputSystem_Actions playerControls;
    private InputAction interactionAction;

    public InputSystem_Actions GetInputActions()
    {
        return playerControls;
    }

    private void Start()
    {
        _isPlayerEnter = false;
        playerControls = new InputSystem_Actions();
        _dialogueController = FindObjectOfType<DialogueController>();
        _dialogueWindow = FindObjectOfType<DialogueWindow>();
    }

    private void Update()
    {
        if(_dialogueWindow.IsPlaying == true || _isPlayerEnter == false)
        {
            return;
        }
        playerControls.Player.Interact.started += OnSubmitPerformed;
        Debug.Log("Update работает!");
    }

    void OnSubmitPerformed(InputAction.CallbackContext context)
    {
        Debug.Log(" лавиша дл€ взаимодействи€ нажата!");
        _dialogueController.EnterDialogueMode(_inkJSON);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject obj = collider.gameObject;

        if (obj.GetComponent<playerController>() != null)
        {
            _isPlayerEnter = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        GameObject obj = collider.gameObject;

        if (obj.GetComponent<playerController>() != null)
        {
            _isPlayerEnter = false;
        }
    }
}
