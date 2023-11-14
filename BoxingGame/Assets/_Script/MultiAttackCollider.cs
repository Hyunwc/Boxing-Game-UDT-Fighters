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
    public void SetDamage(float dmg)
    {
        damage = dmg;

    }
    [PunRPC]
    public void OnTriggerEnter(Collider other)
    {
        if (!photonView.IsMine)
        {
            return; // ���� �÷��̾ �ƴϸ� ó������ �ʽ��ϴ�.
        }
        if (player.useAttack && player.useMove)
        {
            if (other.CompareTag("Player") && !other.GetComponent<PhotonView>().IsMine)
            {

                Debug.Log("noOfClicks : " + a);
                int attackerID = photonView.ViewID; // ������ �÷��̾��� PhotonView ID�� �����ɴϴ�.
                int targetID = other.GetComponent<PhotonView>().ViewID; // ���� ��� �÷��̾��� PhotonView ID�� �����ɴϴ�.

                if (attackerID != targetID)
                {
                    MultiPlayer multiPlayer = other.GetComponent<MultiPlayer>();
                    if (multiPlayer.isSkill)
                    {
                        multiPlayer.TakeDamage(damage * 2, other);
                        photonView.RPC("SyncParticle", RpcTarget.All);
                        
                        return;
                    }

                    multiPlayer.TakeDamage(damage, other);               // ���� ��� �÷��̾�� �������� �ֵ��� �����մϴ�.
                    photonView.RPC("SyncParticle", RpcTarget.All);
                    
                    Debug.Log("play particle");
                    player.TakeMp(MpUp); //�ڽ��� Mp�� ȸ���ϰ�
                }

            }
        }

    }
    [PunRPC]
    public void SyncParticle()
    {
        attack.Play();
    }
}
