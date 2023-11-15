using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.VisualScripting;

public class Bullet : MonoBehaviourPun
{
    public int damage;
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
		    transform.Rotate(Vector3.up*rotateSpeed * Time.deltaTime);

            yield return null;
        }
    }
    private void OnTriggerEnter(Collider other)
    {

            if (other.gameObject.CompareTag("Player") && !other.GetComponent<PhotonView>().IsMine)
            {
			    int attackerID = me.photonView.ViewID; // ������ �÷��̾��� PhotonView ID�� �����ɴϴ�.
			    int targetID = other.GetComponent<PhotonView>().ViewID; // ���� ��� �÷��̾��� PhotonView ID�� �����ɴϴ�.

			    if (attackerID != targetID)
			    {
				    other.gameObject.GetComponent<MultiPlayer>().TakeDamage(30, other);



                    Destroy(this);
                }
            }   
    }
}
