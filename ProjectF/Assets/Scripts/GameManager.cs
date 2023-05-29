using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }    //�̱���
    [SerializeField] PlayerInput input;
    PlayerMovement player;

    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
    // Update is called once per frame
    void Start() { Initialize(); }
    void Initialize()
    {
        player = FindObjectOfType<PlayerMovement>();
        player.Initialize();
        input.SwitchCurrentActionMap("PlayerControls");
        input. actions["Move"].performed += player.OnMove; // performed : Input�� ���ӵ� ��(Update() �ƴ�).
        input.actions["Attack"].canceled += player.OnAttack; // canceled : Input�� ����� ��.

    }


}
