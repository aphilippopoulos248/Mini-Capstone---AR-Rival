using Immersal;
using Immersal.XR;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SessionManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI debugText;
    [SerializeField] private GameObject multiuserUI;
    [SerializeField] private GameObject playerInfoUI;
    [SerializeField] private NetworkDragonSpawner dragonSpawner;
    [SerializeField] private TMP_Text playerHealth;
    [SerializeField] private float startHealth = 1000f;
    [SerializeField] private float stunCoolDown = 5f;

    private ImmersalSDK immersalSDK;
    private NetworkManager networkManager;
    private Localizer localizer;
    public Button stunButton;
    private bool isSessionPaused;
    private bool isStunCooldown = false;
    private float stunCounter = 0f;

    public static SessionManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void OnEnable()
    {
        //multiuserUI.SetActive(false);
    }

    private void Start()
    {
        immersalSDK = ImmersalSDK.Instance;

        localizer = GameObject.FindObjectOfType<Localizer>();
        localizer.OnFirstSuccessfulLocalization.AddListener(OnSuccessfulLocalizations);

        networkManager = NetworkManager.Instance;
        networkManager.onPlayerJoinedEvent.AddListener(OnPlayerJoined);
        networkManager.onPlayerLeftEvent.AddListener(OnPlayerLeft);

        stunButton = playerInfoUI.GetComponentInChildren<Button>();
        stunButton.onClick.AddListener(StartStunCooldown);
    }

    private void OnSuccessfulLocalizations()
    {
        multiuserUI.SetActive(true);
    }

    private void OnPlayerJoined()
    {
        multiuserUI.SetActive(false);
        playerInfoUI.SetActive(true);

        // Preventing multiple instances of the enemy spawning each time a client joins
        var runner = NetworkManager.Instance.Runner;
        if (runner == null)
            return;
        if (!runner.IsSharedModeMasterClient)
            return;

        dragonSpawner.SpawnDragon();
        playerHealth.text = $"{startHealth}";
    }

    private void OnPlayerLeft()
    {
        multiuserUI.SetActive(true);
        playerInfoUI.SetActive(false);
    }

    private void Update()
    {
        int q = immersalSDK.TrackingStatus?.TrackingQuality ?? 0;

        if (q >= 1 && !isSessionPaused)
        {
            immersalSDK.Session.PauseSession();
            isSessionPaused = true;
            debugText.text += "\n Localizer Paused.";
        }
        else if (q < 1 && isSessionPaused)
        {
            immersalSDK.Session.ResumeSession();
            isSessionPaused = false;
            debugText.text += "\n Localizer Resumed.";
        }

        if (isStunCooldown)
        {
            stunCounter += Time.deltaTime;
            if (stunCounter >= stunCoolDown)
            {
                stunButton.interactable = true;
                isStunCooldown = false;
                stunCounter = 0f;
            }
        }
    }

    public void UpdatePlayerHealth(float damage)
    {
        float currentHealth = Mathf.Max(0, float.Parse(playerHealth.text) - damage);
        playerHealth.text = $"{currentHealth}";
    }

    public void StartStunCooldown()
    {
        stunButton.interactable = false;
        isStunCooldown = true;
    }
}
