using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Teleportation : MonoBehaviour
{
    private bool playerInRange = false;
    private InputSystem_Actions inputActions;
    private int index;

    private void Start()
    {
         index = SceneManager.GetActiveScene().buildIndex;
}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            playerInRange = true;
    }

    void Update()
    {
        bool interactPressed = (inputActions != null && inputActions.Player.Interact.triggered) ||
                              Keyboard.current.eKey.wasPressedThisFrame;

        if (playerInRange && interactPressed)
        {
            SceneManager.LoadScene($"FLoor{index+1}");
            Debug.Log("—цена загружена");
        }
    }
}
