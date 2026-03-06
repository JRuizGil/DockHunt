using System;
using Dino.UtilityTools.Singleton;
using UnityEngine;

public class DinoGameManager : Singleton<DinoGameManager>
{
    [SerializeField] private UIManager uiManager;
    public UIManager  UIManager => uiManager;


    private void Start()
    {
        
    }
    
}
