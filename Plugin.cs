using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace CatQuest3CoopDuplicateEquipment;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    internal static new ManualLogSource Logger;
        
    private void Awake()
    {
        // Plugin startup logic
        Logger = base.Logger;

        new Harmony(MyPluginInfo.PLUGIN_GUID).PatchAll();

        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
    }
}


[HarmonyPatch(typeof(UIGridCell))]
class EquipmentGridCellPatch {
    [HarmonyPatch(nameof(UIGridCell.CanSelectConfirm))]
    [HarmonyPrefix]
    static bool CanSelectConfirmPrefix(UIGridCell __instance, ref bool __result) {
        if (__instance is not EquipmentGridCell && __instance is not MagicGridCell)
            return true;
        __result = true;
        return false;
    }
}

[HarmonyPatch(typeof(EquipmentHelper))]
class EquipmentHelperPatch {
    [HarmonyPatch(nameof(EquipmentHelper.IsEquipmentEquipped))]
    [HarmonyPrefix]
    static bool IsEquipmentEquippedPrefix(ref bool __result) {
        __result = false;
        return false;
    }

    [HarmonyPatch(nameof(EquipmentHelper.IsSpellEquipped))]
    [HarmonyPrefix]
    static bool IsSpellEquippedPrefix(ref bool __result) {
        __result = false;
        return false;
    }
}

