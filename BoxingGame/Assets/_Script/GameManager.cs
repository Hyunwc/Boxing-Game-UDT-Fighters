using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (m_instance == null)
            {
                // 씬에서 GameManager 오브젝트를 찾아 할당
                m_instance = FindObjectOfType<GameManager>();
            }

            // 싱글톤 오브젝트를 반환
            return m_instance;
        }
    }
    private static GameManager m_instance; // 싱글톤이 할당될 static 변수

    public GameObject zombiePrefab;
    public GameObject horsePrefab;
    public GameObject ninjaPrefab;

    public static int player1 = 0;
    public static int player2 = 0;
    public PhotonView pv;

    public Transform player1SpawnPoint;
    public Transform player2SpawnPoint;

    [Header("Score")]
    //이부분은 플레이어가 이겼다는걸 보여주기 위해 나중에 bool형태로 수정할 예정
    public int m_player1Score = 0;
    public int m_player2Score = 0;
    //누적스코어
    public int P1WinScore = 0;
    public int P2WinScore = 0;
    //누가 이겼는지 보여주기 위한 이미지들
    public GameObject player1WinImage;
    public GameObject player2WinImage;
    //하드코딩으로 테스트 후 배열로 수정할 예정
    public GameObject P1Round1;
    public GameObject P1Round2;
    public GameObject P1Round3;
    public GameObject P2Round1;
    public GameObject P2Round2;
    public GameObject P2Round3;
    //최종 승리자
    public GameObject EndP1Win;
    public GameObject EndP2Win;
    //항복 패널
    public GameObject P1GiveUp;
    public GameObject P2GiveUp;
    public GameObject OptionPanel;
    public TextImageEffectManager textImageEffectMgr;
    private MultiPlayer multy;
    public Image roundImage;
    public Sprite[] roundArr;
    private int roundidx;
    public bool isEnd = false;
    private void Awake()
    {
        // 씬에 싱글톤 오브젝트가 된 다른 GameManager 오브젝트가 있다면
        //if (instance != this)
        //{
        //    // 자신을 파괴
        //    Destroy(gameObject);
        //}

        if (m_instance != null && m_instance != this)
        {
            Destroy(gameObject);
            return;
        }

        m_instance = this;
        //DontDestroyOnLoad(gameObject);
        pv = GetComponent<PhotonView>();
        roundidx = 0;
        isEnd = false;
        
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

        Quaternion playerRotation = Quaternion.identity;
        // 플레이어 1을 player1SpawnPoint에 스폰
        if (PhotonNetwork.IsMasterClient)
        {
            playerRotation = Quaternion.Euler(0f, 90f, 0f);
            SpawnPlayer(player1, player1SpawnPoint.position, playerRotation);
        }

        // 플레이어 2를 player2SpawnPoint에 스폰
        else
        {
            playerRotation = Quaternion.Euler(0f, -90f, 0f);
            SpawnPlayer(player2, player2SpawnPoint.position, playerRotation);
        }
        multy = FindObjectOfType<MultiPlayer>();
        textImageEffectMgr.ReadyFightTextStart();
    }
    void SpawnPlayer(int playerCharacter, Vector3 spawnPosition, Quaternion playerRotation)
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


        // 네트워크 상의 모든 클라이언트들에서 생성 실행
        // 단, 해당 게임 오브젝트의 주도권은, 생성 메서드를 직접 실행한 클라이언트에게 있음
        PhotonNetwork.Instantiate(selectedPrefab.name, spawnPosition, playerRotation);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PhotonNetwork.LeaveRoom();
        }

        photonView.RPC("RoundImageActive", RpcTarget.All);

    }

    [PunRPC]
    public void Player1Win()
    {
        m_player1Score++;
        P1WinScore++;


        if (P1WinScore < 3)
        {
            roundidx++;
            photonView.RPC("RoundWin", RpcTarget.All); // 모든 클라이언트에게 준비 상태 전달
        }
        else
        {
            photonView.RPC("EndGame", RpcTarget.All);
            photonView.RPC("KOCorou", RpcTarget.All);
        }
        photonView.RPC("ShowWinLoseEffect", RpcTarget.All, 1);
    }
    [PunRPC]
    public void Player2Win()
    {
        m_player2Score++;
        P2WinScore++;

        //roundidx++;
        //photonView.RPC("RoundWin", RpcTarget.All);
        if (P2WinScore < 3)
        {
            roundidx++;
            photonView.RPC("RoundWin", RpcTarget.All); // 모든 클라이언트에게 준비 상태 전달
        }
        else
        {
            photonView.RPC("EndGame", RpcTarget.All);
            photonView.RPC("KOCorou", RpcTarget.All);
            // 이겼을 경우에는 바로 게임 종료
        }
        photonView.RPC("ShowWinLoseEffect", RpcTarget.All, 2);
    }
    //누적 스코어 활성화 시키기위함
    [PunRPC]
    public void RoundImageActive()
    {
        if (P1WinScore == 1)
        {
            P1Round1.SetActive(true);
            //textImageEffectMgr.WinLoseTextStart(true);
        }
        else if (P1WinScore == 2)
        {
            P1Round2.SetActive(true);
            //textImageEffectMgr.WinLoseTextStart(true);
        }
        else if (P1WinScore == 3)
        {
            //textImageEffectMgr.isEnd = true;
            P1Round3.SetActive(true);
        }


        if (P2WinScore == 1)
        {
            P2Round1.SetActive(true);
            //textImageEffectMgr.WinLoseTextStart(true);
        }
        else if (P2WinScore == 2)
        {
            P2Round2.SetActive(true);
            //textImageEffectMgr.WinLoseTextStart(true);
        }
        else if (P2WinScore == 3)
        {
            //textImageEffectMgr.isEnd = true;
            P2Round3.SetActive(true);
        }

    }
    [PunRPC]
    public void ShowWinLoseEffect(int winner)
    {
        textImageEffectMgr.WinLoseTextStart(winner);
    }
    [PunRPC]
    public void RoundWin()
    {
        if (m_player1Score > 0)
        {
            //player1WinImage.SetActive(true); // 이미지 활성화
            //textImageEffectMgr.KOTextStart();
            StartCoroutine(DeactivateWinImage()); // 3초 뒤에 이미지 비활성화
        }

        if (m_player2Score > 0)
        {
            //player2WinImage.SetActive(true); // 이미지 활성화
            //textImageEffectMgr.KOTextStart();
            StartCoroutine(DeactivateWinImage()); // 3초 뒤에 이미지 비활성화
        }
        //if(isEnd != true)
        //{
        photonView.RPC("KOCorou", RpcTarget.All);
        //}
    }
    [PunRPC]
    public void KOCorou()
    {
        Debug.Log("KO");
        textImageEffectMgr.KOTextStart();
    }

    private IEnumerator DeactivateWinImage()
    {
        yield return new WaitForSeconds(6.0f);
        photonView.RPC("ChangeRoundImage", RpcTarget.All);
        photonView.RPC("ReadyRPC", RpcTarget.All);
        m_player1Score = 0;
        m_player2Score = 0;
        //player1WinImage.SetActive(false); // 이미지 비활성화
        //player2WinImage.SetActive(false);
        //textImageEffectMgr.ReadyFightTextStart();
    }
    [PunRPC]
    public void ReadyRPC()
    {
        textImageEffectMgr.ReadyFightTextStart();
    }
    [PunRPC]
    public void ChangeRoundImage()
    {
        roundImage.sprite = roundArr[roundidx];
    }


    [PunRPC]
    public void EndGame()
    {
        //Destroy(player1WinImage);
        //Destroy(player2WinImage);

        if (P1WinScore == 3)
        {
            pv.RPC("ShowEndWinImage", RpcTarget.All, 1);
            pv.RPC("ChanegeUseReset", RpcTarget.All);
            //EndP1Win.SetActive(true);
            //StartCoroutine(FinalWinImage());
        }

        if (P2WinScore == 3)
        {
            pv.RPC("ShowEndWinImage", RpcTarget.All, 2);
            pv.RPC("ChanegeUseReset", RpcTarget.All);
            //EndP2Win.SetActive(true);
            //StartCoroutine(FinalWinImage());
        }

    }
    //최종 승리자 이미지 보여주는 함수
    [PunRPC]
    public void ShowEndWinImage(int winner)
    {
        StartCoroutine(WinImage(winner));
    }
    //[PunRPC]
    //public void ChanegeUseReset()
    //{
    //    StartCoroutine(Cuse());
    //    //multy.useReset = false;
    //}
    //private IEnumerator Cuse()
    //{
    //    yield return new WaitForSeconds(5.0f);
    //    multy.useReset = false;
    //}
    private IEnumerator WinImage(int winner)
    {
        //textImageEffectMgr.isEnd = true;
        textImageEffectMgr.ChangeReset(false);
        textImageEffectMgr.canStartReadyFight = false;
       
        //multy.useReset = false;
        //Debug.Log("짜증나" + multy.useReset);
        yield return new WaitForSeconds(6.0f);
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



    //항복
    [PunRPC]
    public void GiveUpButton()
    {
        OptionPanel.SetActive(false);
        if (PhotonNetwork.IsMasterClient)
        {
            pv.RPC("GiveUp", RpcTarget.All, 1);
        }
        else
        {
            pv.RPC("GiveUp", RpcTarget.All, 2);
        }
    }
    [PunRPC]
    public void GiveUp(int giveUpUser)
    {
        if (giveUpUser == 1)
        {
            P1GiveUp.SetActive(true);
        }
        else if (giveUpUser == 2)
        {
            P2GiveUp.SetActive(true);
        }
        StartCoroutine(FinalWinImage());
    }

    private IEnumerator FinalWinImage()
    {
        yield return new WaitForSeconds(4.0f);
        //SceneManager.LoadScene("TitleScene");
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("TitleScene");
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
            stream.SendNext(roundidx);
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
            roundidx = (int)stream.ReceiveNext();
        }
    }
}
