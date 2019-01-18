using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text _uiText;
    [SerializeField] private Text _warningText;
    [SerializeField] private Image _itemsCollected;
    [SerializeField] private Image _itemsSaved;
    [SerializeField] private Image _torchCounter;
    [SerializeField] private Sprite[] _itemsImages;
    [SerializeField] private Sprite[] _savedItemsImages;
    [SerializeField] private Sprite[] _torchImages;

    private List<string> _warningMessageQueue;

    private bool _warningIsShown = false;

    private void Start()
    {
        _warningMessageQueue = new List<string>();
        Debug.Log(_warningMessageQueue.Count);
    }

    private void Update()
    {
        if (!_warningIsShown && _warningMessageQueue.Count > 0)
        {
            ShowWarningText();
        }
    }

    private void ShowWarningText()
    {
        _warningText.text = _warningMessageQueue[0];
        _warningText.transform.parent.gameObject.SetActive(true);

        StartCoroutine(WarningCooldown());
        _warningIsShown = true;

        _warningMessageQueue.RemoveAt(0);
    }

    public void ShowUIToggle(string text)
    {
        _uiText.text = text;
        _uiText.transform.parent.gameObject.SetActive(true);
    }

    public void HideUIToggle()
    {
        _uiText.transform.parent.gameObject.SetActive(false);
    }

    public void ChangeUIText(string text)
    {
        _uiText.text = text;
    }

    public void AddWarningMessageToQueue(string text)
    {
        _warningMessageQueue.Insert(_warningMessageQueue.Count, text);
    }

    public void ShowGameOver()
    {


    }

    public void ItemCollected(int itemsTotal)
    {
        if (itemsTotal <= 4)
        {
            _itemsCollected.sprite = _itemsImages[itemsTotal];
        }
    }

    public void UpdateTorchUI(int torchLife)
    {
        if (torchLife >= 0)
        {
            _torchCounter.sprite = _torchImages[torchLife];
        }
    }

    public void ItemSaved(int itemsSavedNo)
    {
        if (itemsSavedNo <= 4)
        {
            _itemsSaved.sprite = _savedItemsImages[itemsSavedNo - 1];
        }
    }

    IEnumerator WarningCooldown()
    {
        yield return new WaitForSeconds(2.5f);
        _warningText.transform.parent.gameObject.SetActive(false);
        _warningIsShown = false;
    }
}
