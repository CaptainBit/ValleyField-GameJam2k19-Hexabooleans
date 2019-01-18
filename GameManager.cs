using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private SoundManager _soundManager;
    private UIManager _uiManager;
  
    private int itemsCollected = 0;
    private int itemsNoticedByTaverner = 0;
    private int lastLife = 100;

    private bool _playerLeftOnce = false;
    private bool _playerInBase = false;
    private bool _firstTorch = true;
    private bool _firstForge = true;

    private GameObject player;
    public GameObject playerPrefab;
   
    public GameObject torchePrefab;
    
    public Vector3 _playerPosition = Vector3.zero;

    private void Start()
    {
        //Get player's starting position
        //Transform player = GameObject.Find("Player").GetComponent<Transform>();

        //if (_playerPosition == Vector3.zero)
        //{
        //    _playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        //}


        // Fetching handles
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "Cinematique")
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        _soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
       
        _soundManager.GameStarted();
    }

    private void Update()
    {
        if(!_playerInBase && (Random.Range(0, 1800) == 1))
        {
            _soundManager.PlayRandomTalkingToHimself();
        }

    }

    public void TorchAlmostDead()
    {
        _soundManager.StartSlowHearthBeat();
    }

    public void TorchIsDead()
    {
        _soundManager.StartFastHearthBeat();
    }

    public void PlayerIsDead()
    {
        itemsCollected = itemsNoticedByTaverner;
        _uiManager.ItemCollected(itemsCollected);

        _soundManager.PlayRandomGameOverSound();

        // Transform player = GameObject.Find("Player").GetComponent<Transform>();

        //player.transform.position = _playerPosition;
        player = GameObject.Find("Player");
        Destroy(player);
        Respawn();

       // Debug.Log(_playerPosition.x.ToString() + ", " + _playerPosition.y.ToString() + ", " + _playerPosition.z.ToString());
       // Debug.Log(player.transform.position.x.ToString() + ", " + player.transform.position.y.ToString() + ", " + player.transform.position.z.ToString());
    }
    void Respawn()
    {
       
        _uiManager.HideUIToggle();
        GameObject newPlayer=(GameObject) Instantiate(playerPrefab);
        GameObject newTorche=(GameObject) Instantiate(torchePrefab);
        newPlayer.name = playerPrefab.name;
        newTorche.name = torchePrefab.name;
        Inventory _inventory = new Inventory();

        _inventory._canPickItem = true;
        _inventory._hasTorch = false;

        Debug.Log("Respawn");
        
    }

        public void PlayerCollectedItem()
    {
        if(itemsCollected < 4)
        {
            itemsCollected++;
            _uiManager.ItemCollected(itemsCollected);
            _soundManager.PlayCratePickupSound();
            _soundManager.PlayFoundItem(itemsCollected);
        }
    }

    public void AgentCollidePlayer()
    {
        if(itemsCollected > itemsNoticedByTaverner)
        {
            itemsCollected--;
            _uiManager.ItemCollected(itemsCollected);
        }
    }
    public void PlayerBackToBase()
    {
        if (itemsCollected == itemsNoticedByTaverner)
        {
            _soundManager.PlayRandomInsult();
        }
        else
        {
            itemsNoticedByTaverner = itemsCollected;
            _uiManager.ItemSaved(itemsNoticedByTaverner);
            _soundManager.PlayRandomSuccess(itemsCollected);
        }
    }

    public void TorchReloaded()
    {
        _soundManager.StopFXSource();
        _uiManager.UpdateTorchUI(100);

        if(_firstForge)
        {
            _soundManager.PlayHintForge();
            _firstForge = false;
        }
    }

    public void TorchLostLife(int totalTorchLife)
    {
        _uiManager.UpdateTorchUI(totalTorchLife);

        if(_firstTorch && (lastLife - totalTorchLife == 3))
        {
            _soundManager.PlayHintTorch();
            _firstTorch = false;
        }

        lastLife = totalTorchLife;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _playerInBase = true;

            Inventory inv = other.gameObject.GetComponent<Inventory>();

            if (inv._hasTorch)
            {
                TorcheJoueur torche = other.gameObject.GetComponentInChildren<TorcheJoueur>();
                torche.StopTorchLosing();
            }
            
            if (_playerLeftOnce)
            {
                PlayerBackToBase();
            }

            _soundManager.PauseSoundTrack();
        } 
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _playerLeftOnce = true;
            _playerInBase = false;

            Inventory inv = other.gameObject.GetComponent<Inventory>();

            if (inv._hasTorch)
            {
                TorcheJoueur torche = other.gameObject.GetComponentInChildren<TorcheJoueur>();
                torche.StartTorchLosing();
            }

            _soundManager.PlaySoundTrack();
        }
    }
}
