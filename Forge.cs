using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forge : MonoBehaviour
{
    [SerializeField]
    private float _cooldownForge = 60f;

    [SerializeField]
    private bool _canReloadTorch = true;

    private bool _isNearForge = false;

    private UIManager _uiManager;
    private GameManager _gameManager;

    private string canReloadTorchText = "Appuyer sur E pour recharger la torche";
    private string cannotReloadTorchText = "Vous devez attendre 10s pour recharger la torche";

    private GameObject _player;

    private void Start()
    {
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _canReloadTorch && _isNearForge)
        {
            TorcheJoueur[] torches = _player.gameObject.GetComponentsInChildren<TorcheJoueur>();

            if (torches != null)
            {
                torches[0].ReloadTorch();
            }

            _canReloadTorch = false;
            _uiManager.ChangeUIText(cannotReloadTorchText);
            _gameManager.TorchReloaded();
            StartCoroutine(CooldownForge());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _player = other.gameObject;
            _isNearForge = true;
            _uiManager.ShowUIToggle(_canReloadTorch ? canReloadTorchText : cannotReloadTorchText);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _player = null;
            _isNearForge = false;
            _uiManager.HideUIToggle();
        }
    }

    IEnumerator CooldownForge()
    {
        yield return new WaitForSeconds(_cooldownForge);
        _canReloadTorch = true;
        _uiManager.ChangeUIText(canReloadTorchText);
    }

}
