//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class MoveSceneManager : MonoBehaviour
//{
//    //�̱��÷��� ��ư ������Ʈ ����
//    private Button singlebutton;

//    // �� ���۽� ��ư ������Ʈ �����ϱ�
//    public void Awake()
//    {
//        //�̱��÷��� ��ư ������Ʈ ã��
//        singlebutton = GetComponent<Button>();
//        //�̱��÷��� ��ư�� �ε� ȭ�� �Լ� ����
//        singlebutton.onClick.AddListener(GoToSinglePlay);
//    }

//    //�̱��÷��� ��ư�� ������ ��, �̵��� ���� �Ķ���ͷ� �ؼ� �ε�� �Լ� �θ���
//    void GoToSinglePlay()
//    {
//        LoadingSceneManager.LoadScene("TestScene");
//    }

//}
