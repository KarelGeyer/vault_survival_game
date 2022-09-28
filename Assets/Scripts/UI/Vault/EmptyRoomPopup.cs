using System;
using UnityEngine;

public class EmptyRoomPopup : MonoBehaviour
{
    public static event Action ConfirmEmptyRoomCreation;

    /// <summary>
    /// Confirms that new room will be created
    /// <see cref="VaultBuilding.CreateNewEmptyRoom"/>
    /// </summary>
    public void ConfirmNewEmptyRoom(bool handlePopup)
    {
        if (handlePopup)
        {
            ConfirmEmptyRoomCreation?.Invoke();
        }

        VaultUI.Instance.ResetRoomCollor();
        VaultUI.WaitingForClick = false;
        gameObject.SetActive(false);
    }
}
