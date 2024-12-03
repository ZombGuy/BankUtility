using UnityEngine;
using HarmonyLib;
using static TextureReplace;

public class ActBankAccess : Act
{
    public override bool Perform()
    {
        BankUtility.Log("Bank Access action executed!");

        try
        {
            // Open the bank UI and container using the existing BankUtilityPatch
            BankUtilityPatch.AccessBankInventory();
            BankUtility.Log("Bank Access action performed successfully.");
        }
        catch (System.Exception ex)
        {
            BankUtility.Log($"Error during Bank Access action: {ex.Message}");
        }

        return true; // Indicate the action was successfully performed
    }
}

[HarmonyPatch(typeof(Player), nameof(Player.OnLoad))]
class AssignBankAccessAbility
{
    static void Postfix()
    {
        int bankAccessElementId = 90000; // Unique ID for the Bank Access element
        Chara __instance = EClass.player.chara;
        try
        {
            if (__instance == null)
            {
                BankUtility.Log("Chara instance is null. Skipping ability assignment.");
                return;
            }

            BankUtility.Log("Checking if character has the Bank Access ability...");

            if (__instance.IsPC)
            {
                BankUtility.Log("[BankUtility] Assigning Bank Access ability...");
                var alreadyHasAbility = __instance.HasElement(bankAccessElementId);
                if (alreadyHasAbility == false)
                {
                    // Attempt to assign the ability
                    __instance.GainAbility(bankAccessElementId, 100);
                    BankUtility.Log("Bank Access ability granted successfully.");
                }
                
            }
        }
        catch (System.Exception ex)
        {
            BankUtility.Log($"Error granting Bank Access ability: {ex.Message}");
        }
    }
}


