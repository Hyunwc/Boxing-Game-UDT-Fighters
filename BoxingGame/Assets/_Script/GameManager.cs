using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks, IPunObservable
{
    public static GameManager instance
    {
        get
        {
            // ���� �̱��� ������ ���� ������Ʈ�� �Ҵ���� �ʾҴٸ�
            if (m_instance == null)
            {
                // ������ GameManager ������Ʈ�� ã�� �Ҵ�
                m_instance = FindObjectOfType<GameManager>();
            }

            // �̱��� ������Ʈ�� ��ȯ
            return m_instance;
        }
    }
    private static GameManager m_instance; // �̱����� �Ҵ�� static ����

    public GameObject playerPrefab;

    private void Awake()
    {
        // ���� �̱��� ������Ʈ�� �� �ٸ� GameManager ������Ʈ�� �ִٸ�
        if (instance != this)
        {
            // �ڽ��� �ı�
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    private void Start()
    {
        // ������ ���� ��ġ ����
        Vector3 randomSpawnPos = Random.insideUnitSphere * 5f;
        // ��ġ y���� 0���� ����
        randomSpawnPos.y = 0f;

        // ��Ʈ��ũ ���� ��� Ŭ���̾�Ʈ�鿡�� ���� ����
        // ��, �ش� ���� ������Ʈ�� �ֵ�����, ���� �޼��带 ���� ������ Ŭ���̾�Ʈ���� ����
        PhotonNetwork.Instantiate(playerPrefab.name, randomSpawnPos, Quaternion.identity);
    }

    // Update is called once per frame
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

    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    throw new System.NotImplementedException();
    //}

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Sending data to other clients
            // Example: Serialize player position and rotation
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            // Receiving data from the server
            // Example: Deserialize player position and rotation
            transform.position = (Vector3)stream.ReceiveNext();
            transform.rotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
