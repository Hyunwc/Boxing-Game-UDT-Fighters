//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.InputSystem;


//public class GameMgr : MonoBehaviour
//{
//    public static GameMgr Instance { get; private set; }
//    [SerializeField] PlayerInput playerInput;

//    private void Awake()
//    {
//        if (null == Instance)
//        {
//            Instance = this;
//            DontDestroyOnLoad(gameObject);
//            return;
//        }
//        Destroy(gameObject);
//    }

//    private void Start()
//    {
//        //if (PhotonNetwork.IsConnected)
//        //{
//        //    Initialize();
//        //}
//        //else
//        //{
//        //    Debug.LogError("Not connected to Photon Server.");
//        //}
//    }

//    void Initialize()
//    {
        
//                playerInput.actions["Move"].performed += PlayerController.Instance.OnMove;
//                playerInput.actions["Move"].canceled += PlayerController.Instance.OnMove;

//                playerInput.actions["AttackA"].performed += PlayerController.Instance.OnAttackAButton;
//                playerInput.actions["AttackS"].performed += PlayerController.Instance.OnAttackSButton;
           
//    }

//    private void OnDisable()
//    {
        
//                playerInput.actions["Move"].performed -= PlayerController.Instance.OnMove;
//                playerInput.actions["Move"].canceled -= PlayerController.Instance.OnMove;

//                playerInput.actions["AttackA"].performed -= PlayerController.Instance.OnAttackAButton;
//                playerInput.actions["AttackS"].performed -= PlayerController.Instance.OnAttackSButton;
           
//    }
//}
