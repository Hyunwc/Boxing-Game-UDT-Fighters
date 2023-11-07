using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MultiAttackCollider : MonoBehaviourPun
{
    public float damage = 10;
    public float MpUp = 10;

    [PunRPC]
    private void OnTriggerEnter(Collider other)
    {
        if (!photonView.IsMine)
        {
            return; // ���� �÷��̾ �ƴϸ� ó������ �ʽ��ϴ�.
        }

        if (other.CompareTag("Player") && !other.GetComponent<PhotonView>().IsMine)
        {
            int attackerID = photonView.ViewID; // ������ �÷��̾��� PhotonView ID�� �����ɴϴ�.
            int targetID = other.GetComponent<PhotonView>().ViewID; // ���� ��� �÷��̾��� PhotonView ID�� �����ɴϴ�.

            if (attackerID != targetID)
            {
                other.GetComponent<MultiPlayer>().TakeDamage(damage, attackerID); // ���� ��� �÷��̾�� �������� �ֵ��� �����մϴ�.
            }
        }
        else if (other.CompareTag("Player") && photonView.IsMine)
        {
            //int myId = photonView.ViewID;
            other.GetComponent<MultiPlayer>().TakeMp(MpUp);
        }
    }
}
