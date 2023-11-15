using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.VisualScripting;

public class Bullet : MonoBehaviourPun
{
    private float damage = 25f;
    public float rotateSpeed;
    public Vector3 lookForward;
    MultiPlayer me;

    public void StartSkill(Vector3 skillLookForward)
    {
        lookForward = skillLookForward;
        StartCoroutine(Shoot());
    }
    private void Update()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }
    public void SetPlayer(MultiPlayer player)
    {
        me = player;
    }
    IEnumerator Shoot()
    {
        while (true)
        {
            transform.Translate(lookForward * 5 * Time.deltaTime);
            transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);

            yield return null;
        }
    }
    [PunRPC]
    public void OnTriggerEnter(Collider other)
    {
        //if (!photonView.IsMine)
        //{
        //    return; // ���� �÷��̾ �ƴϸ� ó������ �ʽ��ϴ�.
        //}
        Debug.Log("OnTriggerEnter called"); // ����� �α� �߰�

        if (other.gameObject.CompareTag("Player") && !other.GetComponent<PhotonView>().IsMine)
        {
            int attackerID = me.photonView.ViewID; // ������ �÷��̾��� PhotonView ID�� �����ɴϴ�.
            int targetID = other.GetComponent<PhotonView>().ViewID; // ���� ��� �÷��̾��� PhotonView ID�� �����ɴϴ�.

            Debug.Log("���⵵ ����");
            Debug.Log(attackerID);
            Debug.Log(targetID);
            if (attackerID != targetID)
            {
                Debug.Log("�¾ƶ� �̳��");
                MultiPlayer multiPlayer = other.GetComponent<MultiPlayer>();
                multiPlayer.TakeDamage(damage, other);
                //other.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, damage, other);


                //Destroy(this);
            }
        }
    }
}
