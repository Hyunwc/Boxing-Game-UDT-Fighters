//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using Photon.Pun;

//public class PlayerHealth : LivingEntity
//{
//    public Slider healthSlider; //ü���� ǥ���� UI �����̴�

//    private PlayerController player;

//    private void Awake()
//    {
//        player = GetComponent<PlayerController>();
//    }

//    protected override void OnEnable()
//    {
//        //LivingEntity�� OnEnable()����(���� �ʱ�ȭ)
//        base.OnEnable();
//        healthSlider.gameObject.SetActive(true);
//        //ü�� �����̴��� �ִ��� �⺻ ü�°����� ����
//        healthSlider.maxValue = startingHealth;
//        //ü�� �����̴��� ���� ���� ü�°����� ����
//        healthSlider.value = health;

//        //�÷��̾� ������ �޴� ������Ʈ Ȱ��ȭ
//        player.enabled = true;
//    }
//    //ü��ȸ�� < �̰� ���Ŀ�
//    //������ ó��
//    [PunRPC]
//    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
//    {
//        //LivingEntity�� OnDamage()����(������ ����)
//        base.OnDamage(damage, hitPoint, hitNormal);
//        //���ŵ� ü���� ü�� �����̴��� �ݿ�
//        healthSlider.value = health;
//    }
//    //���ó��
//    public override void Die()
//    {
//        //LivingEntity�� Die()����(��� ����)
//        base.Die();
//        //ü�½����̴� ��Ȱ��ȭ
//        healthSlider.gameObject.SetActive(false);

//        //�÷��̾� ������ �޴� ������Ʈ ���Ҽ�ȭ
//        player.enabled = false;
//    }

//}
