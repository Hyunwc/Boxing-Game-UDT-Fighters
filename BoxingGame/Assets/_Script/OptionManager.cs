using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    //�ɼ� ��ư ������Ʈ ����
    private Button option;
    //�ɼ�â ������Ʈ ����
    private Transform optionWindow;
    //�ɼ�â ������ ��ư ������Ʈ ����
    private Button quitOption;
    //���� �����̴� ������Ʈ ����
    public Slider volumeSlider;

    // Start is called before the first frame update
    void Awake()
    {
        //�ɼ� ��ư ������Ʈ ã��
        option = GetComponent<Button>();
        //�ɼ� ��ư�� ���� �ɼ�â ������Ʈ ã��
        optionWindow = transform.Find("Option Window");

        //�ɼ�â�� ���� �ɼ� ������ ��ư ������Ʈ ã��
        quitOption = optionWindow.transform.Find("Quit Option").GetComponent<Button>();
        //�ɼ�â�� ���� ���� �����̴� ������Ʈ ã��
        volumeSlider = optionWindow.transform.Find("Volume Slider").GetComponent<Slider>();

        //�ɼ�â ��Ȱ��ȭ
        optionWindow.gameObject.SetActive(false);

        //�ɼ� ��ư�� �ɼ�â ���� �Լ� ����
        option.onClick.AddListener(OpenTheOption);
        //�ɼ� ������ ��ư�� �ɼ�â �ݴ� �Լ� ����
        quitOption.onClick.AddListener(CloseTheOption);
        volumeSlider.onValueChanged.AddListener(SoundManager.Instance.ChangeVolumeBG);
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
