using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This Class serves as a way to controll all the UI
/// elements related to the Vault.
/// It takes care of:
/// <term>Toggling all the Vault UI Elements</term>
/// </summary>
public class VaultUI : MonoBehaviour
{
    private static VaultUI instance;
    public static VaultUI Instance {
        get { return instance; }
    }

    static bool waitingForClick;
    public static bool WaitingForClick {
        get { return waitingForClick; }
        set { waitingForClick = value; }
    }

    public static event Action ResetRoomRoomColor;

    [SerializeField] private GameObject _uiButtons;
    [SerializeField] private GameObject _createEmptyRoomPopup;
    [SerializeField] private GameObject _roomPicker;
    [SerializeField] private GameObject _characterCard;
    [SerializeField] private GameObject _roomAssignmentCard;

    private void OnEnable()
    {
        Player.OnVaultEntering += ShowVaultUI;
        Player.OnVaultExiting += HideVaultUI;
        VaultBuilding.OpenConfirmPopup += OpenEmptyRoomConfirmPopup;
        VaultBuilding.OpenRoomPicker += OpenRoomPicker;
        CharacterCard.OnRoomChoosing += CloseCharacterCard;
        CharacterCard.OnRoomChoosing += ShowRoomAssignmentCard;
        RoomAssignmentCard.CloseRoomAssignmentCard += CloseRoomAssignmentCard;
    }

    private void OnDisable()
    {
        Player.OnVaultEntering -= ShowVaultUI;
        Player.OnVaultExiting -= HideVaultUI;
        VaultBuilding.OpenConfirmPopup -= OpenEmptyRoomConfirmPopup;
        VaultBuilding.OpenRoomPicker -= OpenRoomPicker;
        CharacterCard.OnRoomChoosing -= CloseCharacterCard;
        CharacterCard.OnRoomChoosing -= ShowRoomAssignmentCard;
        RoomAssignmentCard.CloseRoomAssignmentCard -= CloseRoomAssignmentCard;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Following methods:
    /// <list>
    /// <see cref="ShowVaultUI"/>
    /// <see cref="HideVaultUI"/>
    /// <see cref="CloseRoomPicker"/>
    /// <see cref="ShowCharacterCard"/>
    /// <see cref="CloseCharacterCard"/>
    /// <see cref="ShowRoomAssignmentCard"/>
    /// <see cref="CloseRoomAssignmentCard"/>
    /// </list>
    /// simply controll the UI elements - either opens them up or closes them
    /// </summary>

    void ShowVaultUI()
    {
        _uiButtons.SetActive(true);
    }

    void HideVaultUI()
    {
        _uiButtons.SetActive(true);
    }

    /// <summary>
    /// Opens up a popup to confirm the user really
    /// wants to create a new empty room
    /// Function is triggered through event <see cref="VaultBuilding.OpenConfirmPopup"/>
    /// </summary>
    void OpenEmptyRoomConfirmPopup()
    {
        _createEmptyRoomPopup.SetActive(true);
        Transform image = _createEmptyRoomPopup.transform.GetChild(0);
        Button confirmButton = image.GetChild(1).GetComponent<Button>();

        if (confirmButton != null)
        {
            if (Money.Amount < 100)
            {
                confirmButton.interactable = false;
            }
            else
            {
                confirmButton.interactable = true;
            }

            waitingForClick = true;
        }
    }

    /// <summary>
    /// Opens up a popup to confirm the user really
    /// wants to create a new specific room
    /// Function is triggered through event <see cref="Player.OpenRoomPicker"/>
    /// </summary>
    void OpenRoomPicker()
    {
        _roomPicker.SetActive(true);
        waitingForClick = true;

        for (int i = 0; i < _roomPicker.transform.childCount; i++)
        {
            Transform roomSelect = _roomPicker.transform.GetChild(i);
            Button button = roomSelect.GetComponent<Button>();
            TextMeshProUGUI price = button.GetComponentInChildren<TextMeshProUGUI>();

            if(price != null)
            {
                if (Convert.ToInt32(price.text.Split("$")[0]) > Money.Amount)
                {
                    button.interactable = false;
                    price.color = Color.red;
                } else
                {
                    button.interactable = true;
                    price.color = Color.white;
                }
            }
        }
    }

    public void CloseRoomPicker()
    {
        _roomPicker.SetActive(false);
        waitingForClick = false;
    }

    public void ShowCharacterCard()
    {
        waitingForClick = true;
        _characterCard.SetActive(true);
    }

    public void CloseCharacterCard()
    {
        waitingForClick = false;
        _characterCard.SetActive(false);
    }

    public void ShowRoomAssignmentCard()
    {
        _roomAssignmentCard.SetActive(true);
        waitingForClick = true;
    }

    public void CloseRoomAssignmentCard()
    {
        _roomAssignmentCard.SetActive(false);
        waitingForClick = false;
    }

    public void ResetRoomCollor()
    {
        ResetRoomRoomColor?.Invoke();
    }
}
