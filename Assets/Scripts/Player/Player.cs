using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This Class takes care of all Player activities and functionalities and
/// serves as a main Class for Player object
/// <list>
///     <term>Helps manipulate the player state</term>
///     <term>keeps track of players location and position</term>
/// </list>
/// </summary>
public class Player : MonoBehaviour
{
    public static event Action OnVaultEntering;
    public static event Action OnVaultExiting;

    private void Update()
    {
        ToggleVaultEntering();
    }

    /// <summary>
    /// Funcionality track whether the player in in the Vault scene
    /// and track its position.
    /// Then it triggers the <see cref="VaultUI"/> funcionalities to display correct UI elements
    /// depening wheter player is or is not in the Vault
    /// <see cref="VaultUI.ShowVaultUI"/>
    /// <see cref="VaultUI.HideVaultUI"/>
    /// </summary>
    void ToggleVaultEntering()
    {
        if (SceneManager.GetActiveScene().name == Constants.VAULT)
        {
            if(transform.position.y < -1f)
            {
                OnVaultEntering?.Invoke();
            } else
            {
                OnVaultExiting?.Invoke();
            }
        }
    }
}
