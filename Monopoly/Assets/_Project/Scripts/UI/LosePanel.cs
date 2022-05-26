using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class LosePanel : MonoBehaviour
{
    [SerializeField] private Button _restart;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        _restart.onClick.AddListener(() =>
        {
            Hide();
            //LevelManager.Instance.LoadLevel();
        });
    }

    private void OnDisable()
    {
        _restart.onClick.RemoveAllListeners();
    }

    public void Show()
    {
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
        DOTween.To(() => _canvasGroup.alpha, alpha => _canvasGroup.alpha = alpha, 1f, 0.5f);
    }

    private void Hide()
    {
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
        DOTween.To(() => _canvasGroup.alpha, alpha => _canvasGroup.alpha = alpha, 0f, 0.5f);
    }
}