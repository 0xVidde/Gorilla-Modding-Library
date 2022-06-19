using HarmonyLib;
using BepInEx;
using UnityEngine;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine.XR;

namespace Gorilla_Modding_Library
{
    [BepInPlugin(modGUID, modName, modVersion)]
    class LibraryLoaderClass : BaseUnityPlugin
    {
        private const string modGUID = "GT.Modding.Lib";
        private const string modName = "A Gorilla Tag Modding Library By Vidde";
        private const string modVersion = "0.0.1";

        public void Awake()
        {
            var harmony = new Harmony(modGUID);
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }

    //[HarmonyPatch(typeof(TextCopier))]
    //[HarmonyPatch("Update", MethodType.Normal)]
    //class LibraryPath
    //{
        //static void Prefix()
        //{
            
        //}
    //}
}
