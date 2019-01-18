using UnityEngine;
using UnityEngine.UI;

public class TorcheMurale : MonoBehaviour
{

    public bool isLit = false;

    [SerializeField]
    private Light _light;
    [SerializeField]
    private GameObject _fireParticles;

    private bool _isNearTorch = false;
    private string litText = "Appuyer sur E pour éteindre la torche";
    private string notLitText = "Appuyer sur E pour allumer la torche";
    

    private UIManager _uiManager;
    private GameObject _player;


    private void Start()
    {
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    private void Update()
    {
        if (_isNearTorch && Input.GetKeyDown(KeyCode.E))
        {
            // If torch has been lit up, remove health points to torch
            if (!isLit)
            {
                TorcheJoueur torche = _player.gameObject.GetComponentInChildren<TorcheJoueur>();

                if (torche.torchLife <= 10)
                {
                    _uiManager.AddWarningMessageToQueue("Votre torche n'est pas assez puissante");
                }
                else
                {
                    _uiManager.ChangeUIText(isLit ? notLitText : litText);

                    // Disable or activate light and fire particles
                    _light.gameObject.SetActive(true);
                    _fireParticles.gameObject.SetActive(true);

                    torche.RemoveLifeFromTorch(3);
                }
            }
            else
            {
                _uiManager.ChangeUIText(isLit ? notLitText : litText);

                _light.gameObject.SetActive(false);
                _fireParticles.gameObject.SetActive(false);
            }

            isLit = !isLit;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _player = other.gameObject;
            _isNearTorch = true;
            _uiManager.ShowUIToggle(isLit ? litText : notLitText);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _player = null;
            _isNearTorch = false;
            _uiManager.HideUIToggle();
        }
    }
}
