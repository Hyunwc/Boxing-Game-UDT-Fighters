using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapEffectTypeDark", menuName = "ScriptableObject/MapEffect/MapEffectTypeDark", order = 1)]
public class MapEffectTypeDark : MapEffectStateData, IState
{
    public void Enter()
    {
        Debug.Log("��� Ÿ�� ����");
        //ĳ���Ϳ��� ��� ȿ�� ����
    }
    public void Execute()
    {
        Debug.Log("��� Ÿ�� ����");
    }
    public void Exit()
    {
        //ĳ���� ũ�� ���� �ǵ�����
        //���ݷ� ���� �ǵ�����
    }
}
