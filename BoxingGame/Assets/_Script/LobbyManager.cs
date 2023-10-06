using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; //����Ƽ�� ���� ������Ʈ
using Photon.Realtime; // ���� ���� ���� ���̺귯��
using UnityEngine.UI;

//���� pun �ݹ� �̺�Ʈ�� ������ �� �ִ� MoboBehaviour�� ��ӹޱ�
public class LobbyManager : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1"; //���� ����

    public Text connectionInfoText; // ��Ʈ��ũ ������ ǥ���� �ؽ�Ʈ
    public Button joinButton; // �� ���� ��ư
    // ���� ����� ���ÿ� ������ ���� ���� �õ�
    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        Screen.SetResolution(1280, 1080, false); // pc����� �ػ� ����
        // ���ӿ� �ʿ��� ����(���� ����) ����
        PhotonNetwork.GameVersion = gameVersion;
        // ������ ������ ������ ���� ���� �õ�
        PhotonNetwork.ConnectUsingSettings();

        //�� ���� ��ư ��� ��Ȱ��ȭ
        joinButton.interactable = false;
        // ���� �õ� ������ �ؽ�Ʈ�� ǥ��
        connectionInfoText.text = "������ ������ ���� ��...";
    }
    //������ ���� ���� ���� �� �ڵ� ����
    public override void OnConnectedToMaster()
    {
        //�� ���� ��ư Ȱ��ȭ
        joinButton.interactable = true;
        //���� ���� ǥ��
        connectionInfoText.text = "�¶��� : ������ ������ �����";
    }
    //������ ���� ���� ���� �� �ڵ� ����
    public override void OnDisconnected(DisconnectCause cause)
    {
        //�� ���� ��ư ��Ȱ��ȭ
        joinButton.interactable = false;
        //���� ���� ǥ��
        connectionInfoText.text = "�������� : ������ ������ ������� ����\n���� ��õ� ��...";

        //������ �������� ������ �õ�
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        //���� ���� ǥ��
        connectionInfoText.text = "�� ���� ���, ���ο� �� ����...";
        //�ִ� 2���� ���� ������ �� �� ����
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });
    }
    //�� ���� �õ�
    public void Connect()
    {
        //�ߺ� ���� �õ��� ���� ���� ���� ��ư ��� ��Ȱ��ȭ
        joinButton.interactable = false;
        //������ ���� ���� ���̶��
        if (PhotonNetwork.IsConnected)
        {
            //�� ���� ����
            connectionInfoText.text = "�뿡 ����...";
            PhotonNetwork.JoinRandomRoom();
            Debug.Log("aaa");
        }
        else
        {
            //������ ������ ���� ���� �ƴ϶�� ������ ������ ���� �õ�
            connectionInfoText.text = "�������� : ������ ������ ������� ����\n���� ��õ� ��...";
            //������ �������� ������ �õ�
            PhotonNetwork.ConnectUsingSettings();
            Debug.Log("reconnect");
        }
    }
    //(�� ���� ����) ���� �� ������ ������ ��� �ڵ� ����
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        //���� ���� ǥ��
        connectionInfoText.text = "�� ���� ����, ���ο� �� ����...";
        //�ִ� 2���� ���� ������ �� �� ����
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });
    }
    //�뿡 ���� �Ϸ�� ��� �ڵ� ����
    public override void OnJoinedRoom()
    {
        //���� ���� ǥ��
        connectionInfoText.text = "�� ���� ����";
        // ��� �� �����ڰ� Multi ���� �ε��ϰ� ��
        PhotonNetwork.LoadLevel("LobbyScene");
        //PhotonNetwork.LoadLevel("MultiScene");
    }


}
