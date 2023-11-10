using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;
using TMPro;
public class LobbySceneManager : MonoBehaviourPunCallbacks
{
    [SerializeField] Button GameStartButton;
    [SerializeField] public Button ReadyButton;
    [SerializeField] public Button ReadyButtonRed;
    [SerializeField] GameObject CharacterInfoPanel;
	[SerializeField] private Button characterInfoLeft;
	[SerializeField] private Button characterInfoRight;
	[SerializeField] private GameObject[] characterInfo;
	[SerializeField] private TextMeshProUGUI[] infoTexts;
	int characterInfoindex;

	private bool player2Ready = false; //�÷��̾�2 �غ���� 
    void Start()
    {
        ReadyButton.interactable = false;
        GameStartButton.interactable = false; //ó���� ��Ȱ��ȭ
        characterInfoindex = 0;

	}
    //����on
    public void OnReadyButtonClicked()
    {
        //�����̾ƴ� �÷��̾ Ŭ���Ͽ��� ��
        if (!PhotonNetwork.IsMasterClient)
        {
            player2Ready = true;  //�غ���� true
            photonView.RPC("UpdateGameStartButton", RpcTarget.All, player2Ready); //���pc���� �غ���� ����

        }
    }
    //����off
    public void OnReadyOffButtonClicked()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            player2Ready = false;  //�غ���� false
            photonView.RPC("UpdateGameStartButton", RpcTarget.All, player2Ready); //���pc���� �غ���� ����

        }
    }
    [PunRPC]
    private void UpdateGameStartButton(bool isPlayer2Ready)
    {
        if(isPlayer2Ready == true)
        {
            ReadyButton.gameObject.SetActive(false);
            ReadyButtonRed.gameObject.SetActive(true);
            //���ӽ��۹�ư true��
            GameStartButton.interactable = isPlayer2Ready;
        }
        else
        {
            ReadyButton.gameObject.SetActive(true);
            ReadyButtonRed.gameObject.SetActive(false);
            //���ӽ��۹�ư true��
            GameStartButton.interactable = isPlayer2Ready;
        }
        
    }
    public void OnGameStartButtonClicked()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            //��� �÷��̾�� ���� ������ �˸�
            photonView.RPC("StartGame", RpcTarget.All);
        }
    }
    [PunRPC]
    private void StartGame()
    {
        PhotonNetwork.LoadLevel("ActionTestScene");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PhotonNetwork.LeaveRoom();
        }
    }
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("TitleScene");
    }
    public void OpenCharacterInfo()
    {
        Debug.Log("Character Info Open");
        if (CharacterInfoPanel)
        {
            CharacterInfoPanel.SetActive(true);
        }
    }
    public void CloseCharacterInfo()
    {
        Debug.Log("Character Info Close");
        CharacterInfoPanel.SetActive(false);
    }
	public void ClickCharacterInfoLeftRightButton(int type)
	{
		characterInfoindex += type;
		if (characterInfoindex < 1)
		{
			characterInfoLeft.gameObject.SetActive(false);
		}
		else if (characterInfoindex > 1)
		{
			characterInfoRight.gameObject.SetActive(false);
		}
		else
		{
			characterInfoLeft.gameObject.SetActive(true);
			characterInfoRight.gameObject.SetActive(true);
		}
        for(int i =0;i<3;i++)
        {
            characterInfo[i].SetActive(false);
            infoTexts[i].gameObject.SetActive(false);
		}
        characterInfo[characterInfoindex].SetActive(true);
		infoTexts[characterInfoindex].gameObject.SetActive(true);
	}
}
