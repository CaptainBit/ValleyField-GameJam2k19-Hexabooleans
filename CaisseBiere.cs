using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaisseBiere : MonoBehaviour
{
    private bool _isNearBox = false;
    private UIManager _uiManager;
    private GameManager _gameManager;

    private void Start()
    {
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    
    private void Update()
    {
        if (_isNearBox && Input.GetKeyDown(KeyCode.E))
        {
            _gameManager.PlayerCollectedItem();

            _uiManager.HideUIToggle();

            _uiManager.AddWarningMessageToQueue("Caisse récupérée");

            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _uiManager.ShowUIToggle("Appuyez sur E pour ramasser la caisse");
            _isNearBox = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _uiManager.HideUIToggle();
            _isNearBox = false;
        }
    }
}
