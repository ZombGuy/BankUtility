using System;

public static class BankUtilityPatch
{
    public static void AccessBankInventory()
    {
        try
        {
            var container = LayerInventory.CreateContainer(EClass.game.cards.container_deposit);

            if (container != null)
            {
                BankUtility.Log("Bank container opened successfully.");
            }
            else
            {
                BankUtility.Log("Failed to open bank container.");
            }
        }
        catch (Exception ex)
        {
            BankUtility.Log($"Error opening bank container: {ex.Message}");
        }
    }
}