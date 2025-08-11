using R3;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class RaceModeUI : SceneUI
    {
        [SerializeField] private Button _openRecordingModeButton;
        [SerializeField] private Button _restartRaceButton;

        private void Awake()
        {
            _openRecordingModeButton.onClick.AddListener(() => 
                _sceneProvider.OpenRecordingMode());
            
            _restartRaceButton.onClick.AddListener(() => 
                RestartScene());
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
                _sceneProvider.OpenRecordingMode();

            if (Input.GetKeyDown(KeyCode.R))
                RestartScene();
        }
    }
}
