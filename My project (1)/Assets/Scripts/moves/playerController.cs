using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 12f;
    private Coroutine hintCoroutine;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private InputSystem_Actions playerControls;
    private Vector2 movement;

    [Header("UI")]
    public Canvas canvas;
    public GameObject healthBarBG;
    public Image healthBarFill;
    public TextMeshProUGUI hintText;

    [Header("Health")]
    public float currentHP = 100f;
    public float maxHP = 100f;

    [Header("Hint Settings")]
    [SerializeField] private float hintDelay = 3f; 

    private bool hasMoved = false; 
    private bool hintShown = false;

    public InputSystem_Actions GetInputActions()
    {
        return playerControls;
    }

    private void Awake()
    {
        try
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            playerControls = new InputSystem_Actions();

            UpdateHealthBar();
        } catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(canvas.gameObject);

        string hint = "hint_movement";
        if (!hasMoved && hintText != null)
            hintCoroutine = StartCoroutine(ShowHintDelayed(hint, hintDelay));
    }

    public void OnEnable() => playerControls.Enable();
    public void OnDisable() => playerControls.Disable();

    private IEnumerator ShowHintDelayed(string hint, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (!hasMoved && hintText != null)
        {
            ShowHint(hint);
            hintShown = true;
        }
    }


    private void Update()
    {
        movement = playerControls.Player.Move.ReadValue<Vector2>();
   
        animator.SetFloat("moveX", movement.x);
        animator.SetFloat("moveY", movement.y);

        if (movement.x != 0) spriteRenderer.flipX = movement.x < 0;

        if (!hasMoved && movement.sqrMagnitude > 0.01f)
        {
            Debug.Log("Игрок начал движение");
            StopCoroutine(hintCoroutine);
            hasMoved = true;
            HideHint();
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void ShowHint(string hint)
    {
        try
        {
            if (!hintShown)
            {
                hintText.text = LanguageManager.Instance.Get(hint);
                if (hintText != null)
                {
                    hintText.gameObject.SetActive(true);
                }
                else Debug.Log("Текст в подсказке пустой");
            }
        }
        catch (Exception e) { Debug.LogException(e); }
    }

    private void LateUpdate()
    {
        UpdateHealthBarPosition();
    }

    public void HideHint()
    {
        if (hintText != null && hintShown)
        {
            hintText.gameObject.SetActive(false);
            hintShown = false;
        }
    }

    public void UpdateHealthBar()
    {
        if (healthBarFill != null) healthBarFill.fillAmount = Mathf.Clamp01(currentHP / maxHP);
        Debug.Log("Изменение HP");
    }

    private void UpdateHealthBarPosition()
    {
        if (healthBarBG == null) return;

        RectTransform rt = healthBarBG.GetComponent<RectTransform>();
        rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 1f);  
        rt.anchoredPosition = new Vector2(0, -50f);
    }

    public void TakeDamage(float damage)
    {
        currentHP = Mathf.Max(0, currentHP - damage);
        UpdateHealthBar();
        if (currentHP <= 0) Die();
    }

    private void Die()
    {
        Debug.Log("Игрок умер!");
        //Здесь реализовать экран смерти (синий)
        this.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Object")
        {
            ShowHint("hint_interact");
            hintShown = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        HideHint();
    }
}