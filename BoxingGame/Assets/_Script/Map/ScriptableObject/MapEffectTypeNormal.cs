using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "MapEffectTypeNormal", menuName = "ScriptableObject/MapEffect/MapEffectTypeNormal", order = 1)]
public class MapEffectTypeNormal : MapEffectStateData, IState
{
    
    public void Enter()
    {
        Debug.Log("NormalŸ�� ����");
    }

    public void Execute()
    {
    }

    public void Exit()
    {

    }
}
