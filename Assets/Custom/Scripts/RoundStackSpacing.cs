
using UnityEngine;
using UnityEngine.UI;

namespace Custom
{
    [RequireComponent(typeof(VerticalLayoutGroup))]
    [RequireComponent(typeof(LayoutElement))]
    public class RoundStackSpacing : MonoBehaviour
    {
        private LayoutElement referenceLayoutElement; 
        private VerticalLayoutGroup layoutGroup;

        private void Awake()
        {
            layoutGroup = GetComponent<VerticalLayoutGroup>();
            referenceLayoutElement = GetComponent<LayoutElement>();
        }

        private void Start()
        {
            layoutGroup.spacing = -referenceLayoutElement.preferredHeight;
        }
    }
}
