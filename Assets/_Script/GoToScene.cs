using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoToScene : MonoBehaviour
{
    //��ư ������Ʈ ����
    private Button singlebutton;

    // �� ���۽� ��ư ������Ʈ �����ϱ�
    void Start()
    {
        singlebutton = GetComponent<Button>();
        singlebutton.onClick.AddListener(GoToSinglePlay);
    }

    //�̱��÷��� ��ư�� ������ ��, �̵��� ���� �Ķ���ͷ� �ؼ� �ε�� �Լ� �θ���
    void GoToSinglePlay()
    {
        LoadingSceneSystem.LoadScene("TestScene");
    }
}
