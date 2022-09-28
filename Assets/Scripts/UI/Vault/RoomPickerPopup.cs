using System;
using UnityEngine;

public class RoomPickerPopup : MonoBehaviour
{
    public static event Action<string> ConfirmRoomCreation;

    /// <summary>
    /// Confirms the chosen room and triggers a function to create it
    /// <see cref="VaultBuilding.CreateNewRoom"/>
    /// </summary>
    public void ConfirmChosenRoomCreation(string roomName)
    {
        if (roomName != null)
        {
            ConfirmRoomCreation?.Invoke(roomName);
        }

        VaultUI.Instance.ResetRoomCollor();
        VaultUI.WaitingForClick = false;
        gameObject.SetActive(false);
    }
}
