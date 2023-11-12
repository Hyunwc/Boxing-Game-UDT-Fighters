using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MultiAttackCollider : MonoBehaviourPun
{
    public float damage = 10f;
    public float MpUp = 25f;

    private MultiPlayer player;
    public ParticleSystem attack;
    public int a;

    void Start()
    {
        player = transform.parent.GetComponent<MultiPlayer>();
        a = player.noOfClicks;
    }
    [PunRPC]
    public void OnTriggerEnter(Collider other)
    {
        if (!photonView.IsMine)
        {
            return; // ���� �÷��̾ �ƴϸ� ó������ �ʽ��ϴ�.
        }

        if (other.CompareTag("Player") && !other.GetComponent<PhotonView>().IsMine)
        {
            Debug.Log("noOfClicks : " + a);
            int attackerID = photonView.ViewID; // ������ �÷��̾��� PhotonView ID�� �����ɴϴ�.
            int targetID = other.GetComponent<PhotonView>().ViewID; // ���� ��� �÷��̾��� PhotonView ID�� �����ɴϴ�.
       
            if (attackerID != targetID)
            {
                other.GetComponent<MultiPlayer>().TakeDamage(damage, other); // ���� ��� �÷��̾�� �������� �ֵ��� �����մϴ�.
                attack.Play();
                Debug.Log("play particle");
                player.TakeMp(MpUp); //�ڽ��� Mp�� ȸ���ϰ�
            }

        }
    }
}
