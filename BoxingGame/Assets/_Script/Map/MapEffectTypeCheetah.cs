using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapEffectTypeB", menuName = "ScriptableObject/MapEffect/MapEffectTypeB", order = 2)]
public class MapEffectTypeCheetah : MapEffectStateData, IState
{
    public void Enter()
    {
        Debug.Log("BŸ�� ����");
        Time.timeScale = 2.0f;
        //�÷��̾��� ������ ���̱�
    }
    public void Excute()
    {
        Debug.Log("BŸ�� ����");
    
    }
    public void Exit()
    {
        Time.timeScale = 1.0f;
        //�÷��̾��� ������ ������� �����
    }
}
