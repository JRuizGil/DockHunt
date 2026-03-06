using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using UnityEngine.Serialization;

public class UIWindow : MonoBehaviour
{
    #region properties
    [Header("Settings")] 
    [SerializeField] private string windowID;
    [SerializeField] private Canvas windowCanvas;
    [SerializeField] private CanvasGroup windowCanvasGroup;
    
    [Header("Options")]
    [SerializeField] private bool hideOnStart = true;
    [SerializeField] private float animationTime = 0.5f;
    // Animation easing types from DOTween 
    [SerializeField] private Ease easeShow = Ease.InBack;
    [SerializeField] private Ease easeHide = Ease.OutBack;
    
    public UnityEvent OnStartShowingUI { get; private set; } = new UnityEvent();
    public UnityEvent OnFinishedShowingUI {get; private set;} = new UnityEvent();
    public UnityEvent OnStartHidingUI { get; private set; } = new UnityEvent();
    public UnityEvent OnFinishedHidingUI { get; private set; } = new UnityEvent();
    public bool IsShowing { get; private set; } = false;
    public string WindowID => windowID;
    #endregion

    #region unity methods
    private void Start()
    {
        Initialize();
    }
    #endregion

    #region implementation
    
    /// <summary>
    ///  Initializes the window, hiding it if hideOnStart is true.
    /// </summary>
    public virtual void Initialize()
    {
        if(hideOnStart) Hide(true);
    }
    
    /// <summary>
    ///  Shows the window UI.
    /// </summary>
    /// <param name="instant"> SET TRUE to show instantly. </param>
    [Button]
    public virtual void Show(bool instant = false)
    {
        // if the window is already showing, do nothing
        if(IsShowing) return;
        
        if (instant)
        {
            //show the window instantly
            windowCanvasGroup.transform.DOScale(Vector3.one, 0);
        }
        else
        {
            // show the window with animation time
            windowCanvasGroup.transform.DOScale(Vector3.one, animationTime).SetEase(easeShow);
            IsShowing = true;
        }
    }
    
    /// <summary>
    /// Hides the window UI.
    /// </summary>
    /// <param name="instant"> SET TRUE to hide instantly.</param>
    [Button]
    public virtual void Hide(bool instant = false)
    {
        if (instant)
        {
            //hide the window instantly
            windowCanvasGroup.transform.DOScale(Vector3.zero, 0f);
        }
        else
        {
            // hide the window with animation time
            windowCanvasGroup.transform.DOScale(Vector3.zero, animationTime).SetEase(easeHide);
            IsShowing = false;
        }
            
    }
    #endregion
    
    
    
}
