using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Button
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class HighlightedButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header ("Text Object")]
        [SerializeField] private TMP_Text _buttonText;

        public void OnPointerEnter(PointerEventData eventData)
        {
            _buttonText.fontStyle = FontStyles.Underline;

        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _buttonText.fontStyle = FontStyles.Normal;
        }
    }
}