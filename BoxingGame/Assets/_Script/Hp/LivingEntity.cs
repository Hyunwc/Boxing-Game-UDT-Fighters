//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Photon.Pun;
//public class LivingEntity : MonoBehaviourPun, IDamageable
//{
//    public float startingHealth = 100f; //����ü��
//    public float health { get; protected set; } //���� ü��
//    public bool dead { get; protected set; } // �������
//    public event Action onDeath; //����� �ߵ��ϴ� �̺�Ʈ

//    [PunRPC]
//    public void ApplyUpdateHealth(float newHealth, bool newDead)
//    {
//        health = newHealth;
//        dead = newDead;
//    }
//    //����ü�� Ȱ��ȭ�� �� ���¸� ����
//    protected virtual void OnEnable()
//    {
//        dead = false; //������� �ʴ� ���·� ����
//        health = startingHealth; // ü���� ���� ü������
//    }
//    //�������� �Դ� ���
//    [PunRPC]
//    public virtual void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
//    {
//        if(PhotonNetwork.IsMasterClient)
//        {
//            //��������ŭ ü�°���
//            health -= damage;
//            photonView.RPC("ApplyUpdateHealth", RpcTarget.Others, health,dead);
//            //�ٸ� Ŭ���̾�Ʈ�� OnDamage�� ����
//            photonView.RPC("OnDamage", RpcTarget.Others, damage, hitPoint, hitNormal);
//        }
//        //ü���� 0 ����&&���� ���� �ʾҴٸ� ���ó�� ����
//        if (health <= 0 && !dead)
//        {
//            Die();
//        }
//    }
//    //ü���� ȸ���ϴ� ��� < �̰� ���Ŀ�
//    //���ó��
//    public virtual void Die()
//    {
//        //onDeath �̺�Ʈ�� ��ϵ� �޼��尡 �ִٸ� ����
//        if (onDeath != null)
//        {
//            onDeath();
//        }
//        //��� ���¸� ������ ����
//        dead = true;
//    }
   
//}
