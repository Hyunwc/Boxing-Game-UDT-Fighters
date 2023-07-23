using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTitleManager : MonoBehaviour
{
    //�̱��÷��� ��ư ������Ʈ ����
    private Button singlebutton;
    //�ɼ� ��ư ������Ʈ ����
    private Button option;
    //�ɼ�â ������Ʈ ����
    private Transform optionWindow;
    //�ɼ�â ������ ��ư ������Ʈ ����
    private Button quitOption;
    //���� �����̴� ������Ʈ ����
    public Slider volumeSlider;


    // �� ���۽� ��ư ������Ʈ �����ϱ�
    public void Awake()
    {
        //�̱��÷��� ��ư ������Ʈ ã��
        singlebutton = transform.Find("SinglePlay").GetComponent<Button>();
        //�ɼ� ��ư ������Ʈ ã��
        option = transform.Find("Option").GetComponent<Button>();

        //�ɼ� ��ư�� ���� �ɼ�â ������Ʈ ã��
        optionWindow = option.transform.Find("Option Window");
        //�ɼ�â�� ���� �ɼ� ������ ��ư ������Ʈ ã��
        quitOption = optionWindow.transform.Find("Quit Option").GetComponent<Button>();
        //�ɼ�â�� ���� ���� �����̴� ������Ʈ ã��
        volumeSlider = optionWindow.transform.Find("Volume Slider").GetComponent<Slider>();
        Debug.Log(volumeSlider);

        //�ɼ�â ��Ȱ��ȭ
        optionWindow.gameObject.SetActive(false);

        //�̱��÷��� ��ư�� �ε� ȭ�� �Լ� ����
        singlebutton.onClick.AddListener(GoToSinglePlay);
        //�ɼ� ��ư�� �ɼ�â ���� �Լ� ����
        option.onClick.AddListener(OpenTheOption);
        //�ɼ� ������ ��ư�� �ɼ�â �ݴ� �Լ� ����
        quitOption.onClick.AddListener(CloseTheOption);
    }

    //�̱��÷��� ��ư�� ������ ��, �̵��� ���� �Ķ���ͷ� �ؼ� �ε�� �Լ� �θ���
    void GoToSinglePlay()
    {
        LoadingSceneManager.LoadScene("TestScene");
    }

    //�ɼ� ��ư ������ ��, �ɼ�â Ȱ��ȭ
    void OpenTheOption()
    {
        optionWindow.gameObject.SetActive(true);
    }

    //�ɼ� ������ ��ư ������ ��, �ɼ�â ��Ȱ��ȭ 
    void CloseTheOption() 
    {
        optionWindow.gameObject.SetActive(false);
    }
}
