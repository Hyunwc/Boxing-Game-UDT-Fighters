using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    //����Ŵ��� �̱��� �ν��Ͻ� ���� ����
    private static SoundManager instance;

    //����Ŵ��� �̱��� �ν��Ͻ� ������Ƽ ����
    public static SoundManager Instance
    {
        get
        {
            //�ν��Ͻ��� �ƹ��͵� ����Ǿ����� ���� ���
            if(instance == null)
            {
                //������ ����Ŵ��� ������Ʈ�� ���� ������Ʈ�� �ִ��� �˻�
                var obj = FindObjectOfType<SoundManager>();
                //���� ����Ŵ��� ������Ʈ ������Ʈ�� ������ �ν��Ͻ� ������ ����
                if(obj != null) 
                {
                    instance = obj;
                }
                //���� ���ٸ� ����Ŵ��� ������Ʈ�� �ִ� ������Ʈ �����ϰ� �ν��Ͻ� ������ ����
                else
                {
                    var newobj = new GameObject().AddComponent<SoundManager>();
                    instance = newobj;
                }
            }
            //�ν��Ͻ� ��ȯ
            return instance;
        }
    }

    //��������� ���� ����
    private AudioSource bgSound;
    //�����Ŭ�� ����Ʈ ����
    public AudioClip[] bgList;

    //����ũ�� ��� ���� ����
    private float bgVolume = 0.1f;

    private void Awake()
    {
        //���� ����Ŵ��� ������Ʈ �ߺ� ���� �˻�
        var objs = FindObjectsOfType<SoundManager>();
        //���� ���� �Ŵ��� ������Ʈ�� �ߺ��� ��� ����  ������ ��ü ����
        if(objs.Length != 1) 
        {
            Destroy(gameObject);
            return;
        }
        //���� ����� �׻� �����ϵ��� �Լ� ȣ��
        DontDestroyOnLoad(gameObject);

        //����� ���� ������Ʈ ����
        bgSound = GetComponent<AudioSource>();

        //������� ���� �Լ� ȣ��
        PlayingBackgroundSound(bgList[0]);
    }

    //������� ���� �Լ�
    public void PlayingBackgroundSound(AudioClip clip)
    {
        //������� Ŭ�� ���� ����
        bgSound.clip = clip;
        //������� ���ѹݺ�
        bgSound.loop = true;
        //����ũ�⸦ ������� ������ ����
        bgSound.volume = bgVolume;
        //������� ���
        bgSound.Play();
    }

    //������� ���� ���� �Լ�
    public void ChangeVolumeBG(float value)
    {
        //�������� �����̵��� ��������� ����ũ�� ������ ����
        bgVolume = value;
        //����ũ�⸦ ������� �ҷ��� ����
        bgSound.volume = bgVolume;
    }
}
