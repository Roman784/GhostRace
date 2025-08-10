using System.Collections;
using UnityEngine;
using R3;

namespace UI
{
    public class UIRoot : MonoBehaviour
    {
        [SerializeField] private Transform _sceneUIContainer;

        public IEnumerator ShowLoadingScreen()
        {
            yield return null;
        }

        public IEnumerator HideLoadingScreen()
        {
            yield return null;
        }

        public void AttachSceneUI(SceneUI sceneUI)
        {
            sceneUI.transform.SetParent(_sceneUIContainer, false);
        }

        public void ClearAllContainers()
        {
            ClearContainer(_sceneUIContainer);
        }

        private void ClearContainer(Transform container)
        {
            var childCount = container.childCount;
            for (int i = 0; i < childCount; i++)
            {
                Destroy(container.GetChild(i).gameObject);
            }
        }
    }
}