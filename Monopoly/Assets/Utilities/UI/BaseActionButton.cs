using UnityEngine;
using UnityEngine.UI;

namespace RH.Utilities.UI
{
    [RequireComponent(typeof(Button))]
    public abstract class BaseActionButton : UIElement
    {
        protected Button _button { get; private set; }

        protected abstract void PerformOnClick();
        protected abstract void Init();

        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(PerformOnClick);
            Init();
        }

        private void OnDestroy()
        {
            _button?.onClick.RemoveListener(PerformOnClick);
        }
    }
}