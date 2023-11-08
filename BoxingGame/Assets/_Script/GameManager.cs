using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks, IPunObservable
{
    public enum Character
    {
        Empty = 0,
        Random,
        Horse,
        Zombie,
        Ninja,
        Count,
    }
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

    public GameObject zombiePrefab;
    public GameObject horsePrefab;
    public GameObject ninjaPrefab;

    public static int player1 = 0;
    public static int player2 = 0;
    public PhotonView pv;

    public Transform player1SpawnPoint;
    public Transform player2SpawnPoint;

    [Header("Score")]
    //�̺κ��� �÷��̾ �̰�ٴ°� �����ֱ� ���� ���߿� bool���·� ������ ����
    public int m_player1Score = 0;
    public int m_player2Score = 0;
    //�������ھ�
    public int P1WinScore = 0;
    public int P2WinScore = 0;
    //���� �̰���� �����ֱ� ���� �̹�����
    public GameObject player1WinImage;
    public GameObject player2WinImage;
    //�ϵ��ڵ����� �׽�Ʈ �� �迭�� ������ ����
    public GameObject P1Round1;
    public GameObject P1Round2;
    public GameObject P1Round3;
    public GameObject P2Round1;
    public GameObject P2Round2;
    public GameObject P2Round3;
    //���� �¸���
    public GameObject EndP1Win;
    public GameObject EndP2Win;

    private void Awake()
    {
        // ���� �̱��� ������Ʈ�� �� �ٸ� GameManager ������Ʈ�� �ִٸ�
        //if (instance != this)
        //{
        //    // �ڽ��� �ı�
        //    Destroy(gameObject);
        //}

        if (m_instance != null && m_instance != this)
        {
            Destroy(gameObject);
            return;
        }

        m_instance = this;
        DontDestroyOnLoad(gameObject);
        pv = GetComponent<PhotonView>();
    }
    // Start is called before the first frame update
    void Start()
    {
        int player1Character = PlayerPrefs.GetInt("Player1Character", 1);
        int player2Character = PlayerPrefs.GetInt("Player2Character", 1);
        player1 = player1Character;
        player2 = player2Character;
        Debug.Log(player1);
        Debug.Log(player2);

        // �÷��̾� 1�� player1SpawnPoint�� ����
        if (PhotonNetwork.IsMasterClient)
        {
            SpawnPlayer(player1, player1SpawnPoint.position);
        }

        // �÷��̾� 2�� player2SpawnPoint�� ����
        else
        {
            SpawnPlayer(player2, player2SpawnPoint.position);
        }
    }
    void SpawnPlayer(int playerCharacter, Vector3 spawnPosition)
    {
        GameObject selectedPrefab = null;

        if (playerCharacter == 2)
        {
            selectedPrefab = horsePrefab;
        }
        else if (playerCharacter == 3)
        {
            selectedPrefab = zombiePrefab;
        }
        else if (playerCharacter == 4)
        {
            selectedPrefab = ninjaPrefab;
        }
        else if (playerCharacter == 1)
        {
            int randC = Random.Range(2, 5);
            selectedPrefab = (randC == 2) ? horsePrefab : (randC == 3) ? zombiePrefab : ninjaPrefab;
        }

        // ��Ʈ��ũ ���� ��� Ŭ���̾�Ʈ�鿡�� ���� ����
        // ��, �ش� ���� ������Ʈ�� �ֵ�����, ���� �޼��带 ���� ������ Ŭ���̾�Ʈ���� ����
        PhotonNetwork.Instantiate(selectedPrefab.name, spawnPosition, Quaternion.identity);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PhotonNetwork.LeaveRoom();
        }

        RoundImageActive();
    }

    [PunRPC]
    public void Player1Win()
    {
        m_player1Score++;
        P1WinScore++;
        photonView.RPC("RoundWin", RpcTarget.All); //���pc���� �غ���� ����
    }
    [PunRPC]
    public void Player2Win()
    {
        m_player2Score++;
        P2WinScore++;
        photonView.RPC("RoundWin", RpcTarget.All);
    }
    [PunRPC]
    public void RoundWin()
    {
        if (m_player1Score > 0)
        {
            player1WinImage.SetActive(true); // �̹��� Ȱ��ȭ
           
            StartCoroutine(DeactivateWinImage()); // 3�� �ڿ� �̹��� ��Ȱ��ȭ
        }

        if (m_player2Score > 0)
        {
            player2WinImage.SetActive(true); // �̹��� Ȱ��ȭ
           
            StartCoroutine(DeactivateWinImage()); // 3�� �ڿ� �̹��� ��Ȱ��ȭ
        }
    }
    private IEnumerator DeactivateWinImage()
    {
        yield return new WaitForSeconds(3.0f);
        m_player1Score = 0;
        m_player2Score = 0;
        player1WinImage.SetActive(false); // �̹��� ��Ȱ��ȭ
        player2WinImage.SetActive(false);
    }

    //���� ���ھ� Ȱ��ȭ ��Ű������
    void RoundImageActive()
    {
        if (P1WinScore == 1)
            P1Round1.SetActive(true);
        if (P1WinScore == 2)
            P1Round2.SetActive(true);
        if (P1WinScore == 3)
        {
            P1Round3.SetActive(true);
            EndGame();
        }
        if (P2WinScore == 1)
            P2Round1.SetActive(true);
        if (P2WinScore == 2)
            P2Round2.SetActive(true);
        if (P2WinScore == 3)
        {
            P2Round3.SetActive(true);
            EndGame();
        }
    }

    [PunRPC]
    public void EndGame()
    {
        Destroy(player1WinImage);
        Destroy(player2WinImage);

        if (P1WinScore == 3)
        {
            pv.RPC("ShowEndWinImage", RpcTarget.All, 1);
            //EndP1Win.SetActive(true);
            //StartCoroutine(FinalWinImage());
        }

        if (P2WinScore == 3)
        {
            pv.RPC("ShowEndWinImage", RpcTarget.All, 2);
            //EndP2Win.SetActive(true);
            //StartCoroutine(FinalWinImage());
        }

    }
    //���� �¸��� �̹��� �����ִ� �Լ�
    [PunRPC]
    public void ShowEndWinImage(int winner)
    {
        if (winner == 1)
        {
            EndP1Win.SetActive(true);
        }
        else if (winner == 2)
        {
            EndP2Win.SetActive(true);
        }
        StartCoroutine(FinalWinImage());
    }
   
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("TitleScene");
    }
    private IEnumerator FinalWinImage()
    {
        yield return new WaitForSeconds(3.0f);
        //SceneManager.LoadScene("TitleScene");
        OnLeftRoom();
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Sending data to other clients
            // Example: Serialize player position and rotation
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(player1WinImage.activeSelf);
            stream.SendNext(player2WinImage.activeSelf);
            stream.SendNext(P1Round1.activeSelf);
            stream.SendNext(P1Round2.activeSelf);
            stream.SendNext(P1Round3.activeSelf);
            stream.SendNext(P2Round1.activeSelf);
            stream.SendNext(P2Round2.activeSelf);
            stream.SendNext(P2Round3.activeSelf);
            stream.SendNext(EndP1Win.activeSelf);
            stream.SendNext(EndP2Win.activeSelf);
        }
        else
        {
            // Receiving data from the server
            // Example: Deserialize player position and rotation
            transform.position = (Vector3)stream.ReceiveNext();
            transform.rotation = (Quaternion)stream.ReceiveNext();
            player1WinImage.SetActive((bool)stream.ReceiveNext());
            player2WinImage.SetActive((bool)stream.ReceiveNext());
            P1Round1.SetActive((bool)stream.ReceiveNext());
            P1Round2.SetActive((bool)stream.ReceiveNext());
            P1Round3.SetActive((bool)stream.ReceiveNext());
            P2Round1.SetActive((bool)stream.ReceiveNext());
            P2Round2.SetActive((bool)stream.ReceiveNext());
            P2Round3.SetActive((bool)stream.ReceiveNext());
            EndP1Win.SetActive((bool)stream.ReceiveNext());
            EndP2Win.SetActive((bool)stream.ReceiveNext());
        }
    }
}
