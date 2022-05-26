using DG.Tweening;
using UnityEngine;

public class BasePanel : MonoBehaviour
{
    private CanvasGroup _canvasGroup;

    protected void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Show()
    {
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
        DOTween.To(() => _canvasGroup.alpha, alpha => _canvasGroup.alpha = alpha, 1f, 0.5f);
    }

    protected void Hide()
    {
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
        DOTween.To(() => _canvasGroup.alpha, alpha => _canvasGroup.alpha = alpha, 0f, 0.5f);
    }
}