using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapEffectTypeGiant", menuName = "ScriptableObject/MapEffect/MapEffectTypeGiant", order = 1)]
public class MapEffectTypeGiant : MapEffectStateData, IState
{
    public void Enter()
    {
        Debug.Log("���� Ÿ�� ����");
        //ĳ���� ũ�� ����
        //���ݷ� ����
    }
    public void Execute()
    {
        Debug.Log("���� Ÿ�� ����");
    }
    public void Exit()
    {
        //ĳ���� ũ�� ���� �ǵ�����
        //���ݷ� ���� �ǵ�����
    }
}
