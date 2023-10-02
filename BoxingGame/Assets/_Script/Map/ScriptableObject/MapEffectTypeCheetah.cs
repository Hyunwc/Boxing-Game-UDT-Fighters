using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapEffectTypeCheetah", menuName = "ScriptableObject/MapEffect/MapEffectTypeCheetah", order = 2)]
public class MapEffectTypeCheetah : MapEffectStateData, IState
{
    public void Enter()
    {
        Debug.Log("ġŸ Ÿ�� ����");
        //��������
        Time.timeScale = 2.0f;
        //�÷��̾��� ������ ���̱�
    }
    public void Execute()
    {
        Debug.Log("ġŸ Ÿ�� ����");
    
    }
    public void Exit()
    {
        Time.timeScale = 1.0f;
        //�÷��̾��� �ӵ� ������� �����
    }
}
