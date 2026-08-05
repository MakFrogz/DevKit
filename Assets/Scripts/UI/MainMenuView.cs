using AudioSystem;
using Extensions;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    [RequireComponent(typeof(PanelRenderer))]
    public class MainMenuView : MonoBehaviour
    {
        [Header("Scene Loading")]
        [Tooltip("Name of the scene to load when Start is pressed. Must be added to Build Settings.")]
        [SerializeField]
        private string _gameSceneName = "GameScene";

        private PanelRenderer _panelRenderer;
        private int _uiVersion = -1;

        private VisualElement _mainPanel;
        private VisualElement _optionsPanel;
        private VisualElement _creditsPanel;

        private Button _startButton;
        private Button _optionsButton;
        private Button _creditsButton;
        private Button _quitButton;

        private Button _optionsSaveButton;
        private Button _optionsBackButton;
        private Button _creditsBackButton;

        private Slider _masterVolumeSlider;
        private Slider _musicVolumeSlider;
        private Slider _soundVolumeSlider;

        private void Awake()
        {
            _panelRenderer = GetComponent<PanelRenderer>();
            _panelRenderer.RegisterUIReloadCallback(OnUIReload);
        }

        private void OnDestroy()
        {
            _panelRenderer.UnregisterUIReloadCallback(OnUIReload);
        }

        private void OnUIReload(PanelRenderer panelRenderer, VisualElement root, int version)
        {
            if (_uiVersion == version)
            {
                return;
            }

            _uiVersion = version;

            CacheElements(root);
            RegisterCallbacks();
            ShowPanel(_mainPanel);
        }

        private void CacheElements(VisualElement root)
        {
            _mainPanel = root.Q<VisualElement>("main-panel");
            _optionsPanel = root.Q<VisualElement>("options-panel");
            _creditsPanel = root.Q<VisualElement>("credits-panel");

            _startButton = root.Q<Button>("start-button");
            _optionsButton = root.Q<Button>("options-button");
            _creditsButton = root.Q<Button>("credits-button");
            _quitButton = root.Q<Button>("quit-button");

            _optionsSaveButton = root.Q<Button>("options-save-button");
            _optionsBackButton = root.Q<Button>("options-back-button");
            _creditsBackButton = root.Q<Button>("credits-back-button");

            _masterVolumeSlider = root.Q<Slider>("master-volume-slider");
            _musicVolumeSlider = root.Q<Slider>("music-volume-slider");
            _soundVolumeSlider = root.Q<Slider>("sound-volume-slider");

            LoadOptionsIntoUI();
        }

        private void LoadOptionsIntoUI()
        {
            var master = AudioProvider.Instance.GetMasterVolume();
            var music = AudioProvider.Instance.GetMusicVolume();
            var sound = AudioProvider.Instance.GetSoundVolume();

            _masterVolumeSlider?.SetValueWithoutNotify(master);
            _musicVolumeSlider?.SetValueWithoutNotify(music);
            _soundVolumeSlider?.SetValueWithoutNotify(sound);
        }

        private void RegisterCallbacks()
        {
            _startButton?.RegisterCallback<ClickEvent>(OnStartClicked);
            _optionsButton?.RegisterCallback<ClickEvent>(OnOptionsClicked);
            _creditsButton?.RegisterCallback<ClickEvent>(OnCreditsClicked);
            _quitButton?.RegisterCallback<ClickEvent>(OnQuitClicked);

            _optionsSaveButton?.RegisterCallback<ClickEvent>(OnSaveOptionsClicked);
            _optionsBackButton?.RegisterCallback<ClickEvent>(OnOptionsBackClicked);
            _creditsBackButton?.RegisterCallback<ClickEvent>(OnBackClicked);

            _masterVolumeSlider?.RegisterValueChangedCallback(OnMasterVolumeChanged);
            _musicVolumeSlider?.RegisterValueChangedCallback(OnMusicVolumeChanged);
            _soundVolumeSlider?.RegisterValueChangedCallback(OnSoundVolumeChanged);
        }

        private void OnStartClicked(ClickEvent evt)
        {
            if (_gameSceneName.IsNullOrEmpty())
            {
                Debug.LogWarning("MainMenuController: gameSceneName is not set.");
                return;
            }

            UnityEngine.SceneManagement.SceneManager.LoadScene(_gameSceneName);
        }

        private void OnOptionsClicked(ClickEvent evt) => ShowPanel(_optionsPanel);

        private void OnCreditsClicked(ClickEvent evt) => ShowPanel(_creditsPanel);

        private void OnQuitClicked(ClickEvent evt)
        {
            Debug.Log("Quit requested.");

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        private void OnBackClicked(ClickEvent evt) => ShowPanel(_mainPanel);
        
        private void OnOptionsBackClicked(ClickEvent evt)
        {
            AudioProvider.Instance.CancelVolumeChanges();
            LoadOptionsIntoUI();
            ShowPanel(_mainPanel);
        }

        private void OnMasterVolumeChanged(ChangeEvent<float> evt)
        {
            AudioProvider.Instance.SetMasterVolume(evt.newValue);
        }

        private void OnMusicVolumeChanged(ChangeEvent<float> evt)
        {
            AudioProvider.Instance.SetMusicVolume(evt.newValue);
        }

        private void OnSoundVolumeChanged(ChangeEvent<float> evt)
        {
            AudioProvider.Instance.SetSoundVolume(evt.newValue);
        }

        private void OnSaveOptionsClicked(ClickEvent evt)
        {
            AudioProvider.Instance.SaveVolumeChanges();
            Debug.Log("Options saved.");
        }

        private void ShowPanel(VisualElement panelToShow)
        {
            SetPanelVisible(_mainPanel, panelToShow == _mainPanel);
            SetPanelVisible(_optionsPanel, panelToShow == _optionsPanel);
            SetPanelVisible(_creditsPanel, panelToShow == _creditsPanel);
        }

        private void SetPanelVisible(VisualElement panel, bool visible)
        {
            if (panel == null)
            {
                return;
            }

            if (visible)
            {
                panel.RemoveFromClassList("hidden");
            }
            else
            {
                panel.AddToClassList("hidden");
            }
        }
    }
}