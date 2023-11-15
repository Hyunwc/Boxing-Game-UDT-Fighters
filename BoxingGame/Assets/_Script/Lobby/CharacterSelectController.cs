using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using UnityEngine.SceneManagement;

public enum Character
{
    Empty = 0,
    Random,
    Horse,
    Zombie,
    Ninja,
    Count ,
}
public enum Direction
{
    Left = -1,
    Right = 1,
}
public class CharacterSelectController : MonoBehaviourPunCallbacks
{
    public Button leftButtonP1;
    public Button rightButtonP1;
    public Button leftButtonP2;
    public Button rightButtonP2;
    [SerializeField] private GameObject[]characters;


    //public bool isReady = false;
    [SerializeField] private Button ReadyButton;

    private Character curCharacterP1 = Character.Empty;
    private Character curCharacterP2 = Character.Empty;
    private LobbySceneManager lobbyScene;
    void Start()
    {
        leftButtonP1.interactable = false;
        rightButtonP1.interactable = true;
        leftButtonP2.interactable = false;
        rightButtonP2.interactable = true;
        lobbyScene = FindObjectOfType<LobbySceneManager>();
		//������ Ŭ���̾�Ʈ�� ȭ�鿡�� P2�� ��ư false
		if (PhotonNetwork.IsMasterClient)
        {
            leftButtonP2.gameObject.SetActive(false);
            rightButtonP2.gameObject.SetActive(false);

            if (!photonView.IsMine)
            {
                //�Ϲ� �÷��̾ �濡 �������� ������ ����
                photonView.RPC("SyncCharacterChangeP1", RpcTarget.Others, (int)curCharacterP1);
                PlayerPrefs.SetInt("Player1Character", (int)curCharacterP1);
            }
        }
        else
        {
            leftButtonP1.gameObject.SetActive(false);
            rightButtonP1.gameObject.SetActive(false);
            //�����Ϳ��� ������ ����
            photonView.RPC("RequestMasterClientCharacterSelection", RpcTarget.MasterClient);
        }

        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("SyncCharacterChangeP1", RpcTarget.Others, (int)curCharacterP1);
            PlayerPrefs.SetInt("Player1Character", (int)curCharacterP1);
        }
   //     else
   //     {
   //         //���Ⱑ �������� �÷��̾�2�� ����ĳ���ͷ� �����ϴ� �κ� ���⼭ �غ� Ȱ��ȭ �Ǿ���� ��ġ?
   //         curCharacterP2 = Character.Random;
           
			//photonView.RPC("SyncCharacterChangeP2", RpcTarget.All, (int)curCharacterP2);
   //         PlayerPrefs.SetInt("Player2Character", (int)curCharacterP2);
   //     }
    }
    
    //������ ���� ��ưŬ�� �̺�Ʈ
    public void ClickLeftButtonP1()
    {
        if ((int)curCharacterP1 > (int)Character.Empty + 1)
        {
            rightButtonP1.interactable = true;
            curCharacterP1--;
            ChangeCharacter((int)curCharacterP1);
            if ((int)curCharacterP1 <= 1)
            {
                leftButtonP1.interactable = false;
            }
        }
        OnCharacterSelected((int)curCharacterP1);
        if (photonView.IsMine)
        {
            photonView.RPC("SyncCharacterChangeP1", RpcTarget.Others, (int)curCharacterP1);
            //���ӸŴ����� ������ �������� ����
            PlayerPrefs.SetInt("Player1Character", (int)curCharacterP1);

            Debug.Log((int)curCharacterP1);
        }
    }
    //������ ������ ��ưŬ�� �̺�Ʈ
    public void ClickRightButtonP1()
    {
        if ((int)curCharacterP1 < (int)Character.Count - 1)
        {
            leftButtonP1.interactable = true;
            curCharacterP1++;
            ChangeCharacter((int)curCharacterP1);
            if ((int)curCharacterP1 >= (int)Character.Count - 1)
            {
                rightButtonP1.interactable = false;
            }
        }
        OnCharacterSelected((int)curCharacterP1);
        if (photonView.IsMine)
        {
            photonView.RPC("SyncCharacterChangeP1", RpcTarget.Others, (int)curCharacterP1);
            //���ӸŴ����� ������ �������� ����
            PlayerPrefs.SetInt("Player1Character", (int)curCharacterP1);
            Debug.Log((int)curCharacterP1);
        }
    }
    [PunRPC]
    void RequestMasterClientCharacterSelection()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            // ������ �ƴ� ��쿡�� ������ ĳ���� ���� ������ ��û
            photonView.RPC("SyncCharacterChangeP1", RpcTarget.Others, (int)curCharacterP1);
            PlayerPrefs.SetInt("Player1Character", (int)curCharacterP1);
        }
    }

    [PunRPC]
    void SyncCharacterChangeP1(int curCharacter)
    {
        ChangeCharacter(curCharacter);
    }
    public void ClickLeftButtonP2()
    {
        Debug.Log("����");
        if ((int)curCharacterP2 > (int)Character.Empty + 1)
        {
            rightButtonP2.interactable = true;
            curCharacterP2--;
            ChangeCharacter((int)curCharacterP2);
            if ((int)curCharacterP2 <= 1)
            {
                leftButtonP2.interactable = false;
            }
        }
        if (!photonView.IsMine)
        {
            photonView.RPC("SyncCharacterChangeP2", RpcTarget.Others, (int)curCharacterP2);

            PlayerPrefs.SetInt("Player2Character", (int)curCharacterP2);
            Debug.Log((int)curCharacterP2);
        }

    }
    public void ClickRightButtonP2()
    {
        Debug.Log("����");
        if ((int)curCharacterP2 < (int)Character.Count - 1)
        {
            leftButtonP2.interactable = true;
            curCharacterP2++;
            ChangeCharacter((int)curCharacterP2);
            if ((int)curCharacterP2 >= (int)Character.Count - 1)
            {
                rightButtonP2.interactable = false;
            }
        }
        if (!photonView.IsMine)
        {
            photonView.RPC("SyncCharacterChangeP2", RpcTarget.Others, (int)curCharacterP2);

            PlayerPrefs.SetInt("Player2Character", (int)curCharacterP2);
            Debug.Log((int)curCharacterP2);

            
            if (lobbyScene != null)
            {
                lobbyScene.ReadyButton.interactable = true;
            }
        }

    }
    [PunRPC]
    void SyncCharacterChangeP2(int curCharacter)
    {
		//lobbyScene.ReadyButton.interactable = true;
		ChangeCharacter(curCharacter);

        //PlayerPrefs.SetInt("Player2Character", curCharacter);
    }
    public void OnCharacterSelected(int selectedCharacter)
    {

        // ���õ� ĳ���� ������ RPC�� �ٸ� �÷��̾�鿡�� ����
        photonView.RPC("SyncCharacterSelection", RpcTarget.OthersBuffered, selectedCharacter);
    }

    [PunRPC]
    void SyncCharacterSelection(int selectedCharacter)
    {
        // �ٸ� �÷��̾ �濡 ������ �� �� RPC �޽����� �޾� ĳ���͸� ����
        ChangeCharacter(selectedCharacter);
    }

    void ChangeCharacter(int curCharacter)
    {
        Debug.Log(curCharacter);

        if(curCharacter == 0)
        {
            return;
        }
        
        foreach (GameObject c in characters) 
        {
            c.SetActive(false);
        }

        characters[curCharacter].SetActive(true);
    }
    public void Player2Delete(bool p2Exit)
    {
        Debug.Log(p2Exit);
        photonView.RPC("ResetPlayer2", RpcTarget.MasterClient);
    }

    [PunRPC]
    public void ResetPlayer2()
    {
        Debug.Log("RPCȣ��");
        characters[1].SetActive(false);
        characters[2].SetActive(false);
        characters[3].SetActive(false);
        characters[4].SetActive(false);
        lobbyScene.GameStartButton.interactable = false;
        lobbyScene.ReadyButtonRed.gameObject.SetActive(false);
        lobbyScene.ReadyButton.gameObject.SetActive(false);
        rightButtonP1.interactable = true;
        leftButtonP1.interactable = true;
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("TitleScene");
    }

}
