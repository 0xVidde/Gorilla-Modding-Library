using HarmonyLib;
using BepInEx;
using UnityEngine;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine.XR;
using System.Linq;

namespace Gorilla_Modding_Library
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class LoaderClass : BaseUnityPlugin
    {
        private const string modGUID = "GT.Modding.Lib.Example";
        private const string modName = "A Gorilla Tag Modding Library Example By Vidde";
        private const string modVersion = "0.0.1";

        public void Awake()
        {
            var harmony = new Harmony(modGUID);
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }

    [HarmonyPatch(typeof(GorillaLocomotion.Player))]
    [HarmonyPatch("FixedUpdate", MethodType.Normal)]
    public class MainPatch
    {
        static void Prefix(GorillaLocomotion.Player __instance)
        {
            try
            {
                
            }
            catch
            {

            }
        }
    }
}
