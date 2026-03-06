
using UnityEngine;

using UnityEngine.UI;

public class PopupUI : UIWindow
{
    #region Popup implementation
    
    #region serialized Fields
    [Header("Popup Settings")]
    [SerializeField] private Button _buttonYes;
    [SerializeField] private Button _buttonNo;
    #endregion
    
    public override void Initialize()
    {
        base.Initialize();
        _buttonNo.onClick.AddListener(NoClick);
        _buttonYes.onClick.AddListener(YesClick);
    }
    private void YesClick()
    {
        Debug.Log("Yes Clicked");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    private void NoClick()
    {
        Debug.Log("No clicked");
    }
    #endregion
}
