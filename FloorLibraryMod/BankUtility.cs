using BepInEx;
using HarmonyLib;
using System;
using System.IO;
using UnityEngine;

[BepInPlugin("floor.elin.bankutility", "Bank Utility", "1.0.0")]
public class BankUtility : BaseUnityPlugin


{

    public void OnStartCore()
    {
        var dir = Path.GetDirectoryName(Info.Location);
        var excel = dir + "/Data/SourceCard.xlsx";
        var sources = Core.Instance.sources;
        ModUtil.ImportExcel(excel, "Element", sources.elements);
    }

    private KeyCode openBankHotkey;
    private void Awake()
    {
        // Configuring the hotkey
        //openBankHotkey = Config.Bind(
        //    "Keybinds",
        //    "OpenBankHotkey",
        //    KeyCode.B, // Default hotkey
        //    "Key to open the bank from anywhere."
        //).Value;

        

        // Initialize Harmony
        var harmony = new Harmony("floor.elin.bankutility");
        LoadData();
        harmony.PatchAll();
    }
    private void LoadData()
    {
        try
        {
            var dir = Path.GetDirectoryName(GetType().Assembly.Location);
            var excelPath = Path.Combine(dir, "Data/SourceCard.xlsx");

            if (!File.Exists(excelPath))
            {
                BankUtility.Log("[BankUtility] SourceBankAbility.xlsx not found. Skipping data import.");
                return;
            }

            // Importing the elements from an Excel file
            ModUtil.ImportExcel(excelPath, "Element", EClass.sources.elements);

            BankUtility.Log("[BankUtility] Bank Access Element imported successfully from Excel.");

            // Register custom Bank Access action
            ClassCache.caches.Create<ActBankAccess>("ActBankAccess", "BankUtilityNamespace");
            BankUtility.Log("[BankUtility] Bank Access action registered successfully.");

            // Optionally load other game resources, like icons or textures
            // Texture2D tex = IO.LoadPNG(Path.Combine(dir, "Texture/BankAccess.png"));
            // ...
        }
        catch (System.Exception ex)
        {
            BankUtility.Log($"[BankUtility] Error during LoadData: {ex.Message}");
        }
    }
    private void OpenBankFromAnywhere()
    {
        Log("Attempting to open the bank UI from anywhere...");
        BankUtilityPatch.AccessBankInventory(); // Call the patch logic
    }
    //private void Update()
    //{
    //    // Check for the hotkey press
    //    if (Input.GetKeyDown(openBankHotkey))
    //    {
    //        OpenBankFromAnywhere();
    //    }
    //}

    public static void Log(string message)
    {
        Console.WriteLine($"[BankUtility]: {message}");
    }
}