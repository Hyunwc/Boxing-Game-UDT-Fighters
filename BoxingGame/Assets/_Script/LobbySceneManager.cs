using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class LobbySceneManager : MonoBehaviourPunCallbacks
{
    [SerializeField] Button GameStartButton;
    [SerializeField] Button ReadyButton;

    private bool player2Ready = false; //�÷��̾�2 �غ���� 
    void Start()
    {
        ReadyButton.interactable = true;
        GameStartButton.interactable = false; //ó���� ��Ȱ��ȭ
    }
    public void OnReadyButtonClicked()
    {
        //�����̾ƴ� �÷��̾ Ŭ���Ͽ��� ��
        if (!PhotonNetwork.IsMasterClient)
        {
            player2Ready = true;  //�غ���� true
            photonView.RPC("UpdateGameStartButton", RpcTarget.All, player2Ready); //���pc���� �غ���� ����

        }
    }
    [PunRPC]
    private void UpdateGameStartButton(bool isPlayer2Ready)
    {
        //���ӽ��۹�ư true��
        GameStartButton.interactable = isPlayer2Ready;
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
}
