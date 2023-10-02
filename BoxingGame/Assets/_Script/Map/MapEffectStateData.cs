using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public void Enter();        //���½���
    public void Excute();       //��������
    public void Exit();         // ����Ż��
}
public class MapEffectStateData : ScriptableObject
{
    public IState MapEffectNormal { get; private set; }
    public IState MapEffectTypeGiant { get; private set; }
    public IState MapEffectTypeCheetah { get; private set; }

    public void SetData(IState Normal,IState Giant, IState Cheetah)
    {
        MapEffectNormal = Normal;
        MapEffectTypeGiant = Giant;
        MapEffectTypeCheetah = Cheetah;
    }
}




