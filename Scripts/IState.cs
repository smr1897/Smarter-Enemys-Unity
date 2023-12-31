using UnityEngine;
//using System.Drawing;


public interface IState
{
    void Tick();
    void OnEnter();
    void OnExit();

    Color GizmoColor();
}