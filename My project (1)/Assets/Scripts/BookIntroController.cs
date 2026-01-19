using UnityEngine;
using UnityEngine.InputSystem;

public class BookIntroController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject mainMenuPanel;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    private InputSystem_Actions actions;
    private bool animationStarted = false;
    private bool skipPressed = false;

    private void Awake()
    {
        actions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        actions.Enable();
        actions.UI.AnyKey.performed += OnAnyKeyPressed;
    }

    private void OnDisable()
    {
        actions.Disable();
        actions.UI.AnyKey.performed -= OnAnyKeyPressed;
    }

    private void Start()
    {
        Screen.fullScreen = true;
        Time.timeScale = 0f;
        mainMenuPanel.SetActive(false);
        StartAnimation();
    }

    private void StartAnimation()
    {
        if (animationStarted) return;
        animationStarted = true;
        animator.SetBool("Start", true);
        Debug.Log("Анимация началась");
    }

    private void OnAnyKeyPressed(InputAction.CallbackContext ctx)
    {
        if (!animationStarted) return;
        skipPressed = true;
        animator.SetBool("Skip", true);
        Debug.Log("Анимация пропущена");
    }

    public void OnAnimationComplete()
    {
        if (skipPressed) return;
        FinishIntro();
    }

    private void FinishIntro()
    {
        Time.timeScale = 1f;
        mainMenuPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}