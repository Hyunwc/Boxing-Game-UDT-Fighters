using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapEffectTypeA", menuName = "ScriptableObject/MapEffect/MapEffectTypeA", order = 1)]
public class MapEffectTypeGiant : MapEffectStateData, IState
{
    public void Enter()
    {
        Debug.Log("AŸ�� ����");
        //ĳ���� ũ�� ����
        //���ݷ� ����
    }
    public void Excute()
    {
        Debug.Log("AŸ�� ����");
    }
    public void Exit()
    {
        //ĳ���� ũ�� ���� �ǵ�����
        //���ݷ� ���� �ǵ�����
    }
}
