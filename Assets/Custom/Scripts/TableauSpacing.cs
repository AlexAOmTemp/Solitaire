
using UnityEngine;
using UnityEngine.UI;

namespace Custom
{
    [RequireComponent(typeof(VerticalLayoutGroup))]
    [RequireComponent(typeof(LayoutElement))]
    public class TableauSpacing : MonoBehaviour
    {
        [SerializeField] private float HideMultiplier01 = .7f;
        private LayoutElement referenceLayoutElement; 
        private VerticalLayoutGroup layoutGroup;

        private void Awake()
        {
            layoutGroup = GetComponent<VerticalLayoutGroup>();
            referenceLayoutElement = GetComponent<LayoutElement>();
        }

        private void Start()
        {
            HideMultiplier01 = Mathf.Clamp01(HideMultiplier01);
            layoutGroup.spacing = -referenceLayoutElement.preferredHeight * HideMultiplier01;
        }
    }
}
