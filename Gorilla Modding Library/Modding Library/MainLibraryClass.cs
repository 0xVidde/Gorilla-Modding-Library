using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace Modding_Library
{
    public class Local
    {
        public static Classes.LocalPlayer GetLocalPlayer()
        {
            var player = new Classes.LocalPlayer
            {
                headPosition = GorillaLocomotion.Player.Instance.headCollider.transform.position,

                leftHandPosition = GorillaLocomotion.Player.Instance.leftHandTransform.position,
                rightHandPosition = GorillaLocomotion.Player.Instance.rightHandTransform.position,

                player = GorillaLocomotion.Player.Instance,
            };

            return player;
        }

        public static Classes.LocalCosmetics GetLocalCosmetics()
        {
            var cosmetics = new Classes.LocalCosmetics
            {
                badgeName = GorillaNetworking.CosmeticsController.instance.currentWornSet.badge.displayName,
                faceName = GorillaNetworking.CosmeticsController.instance.currentWornSet.face.displayName,
                hatName = GorillaNetworking.CosmeticsController.instance.currentWornSet.hat.displayName,
                leftHandHoldName = GorillaNetworking.CosmeticsController.instance.currentWornSet.leftHandHold.displayName,
                rightHandHoldName = GorillaNetworking.CosmeticsController.instance.currentWornSet.rightHandHold.displayName
            };

            return cosmetics;
        }

        public static Classes.LocalInput GetLocalInput()
        {
            bool isHoldingRightGrip;
            bool isHoldingLeftGrip;
            bool isHoldingRightTrigger;
            bool isHoldingLeftTrigger;

            float rightGripValue;
            float leftGripValue;
            float rightTriggerValue;
            float leftTriggerValue;

            List<InputDevice> leftList = new List<InputDevice>();
            List<InputDevice> rightList = new List<InputDevice>();
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller, leftList);
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller, rightList);

            rightList[0].TryGetFeatureValue(CommonUsages.gripButton, out isHoldingRightGrip);
            leftList[0].TryGetFeatureValue(CommonUsages.gripButton, out isHoldingLeftGrip);
            rightList[0].TryGetFeatureValue(CommonUsages.triggerButton, out isHoldingRightTrigger);
            leftList[0].TryGetFeatureValue(CommonUsages.triggerButton, out isHoldingLeftTrigger);

            rightList[0].TryGetFeatureValue(CommonUsages.grip, out rightGripValue);
            leftList[0].TryGetFeatureValue(CommonUsages.grip, out leftGripValue);
            rightList[0].TryGetFeatureValue(CommonUsages.trigger, out rightTriggerValue);
            leftList[0].TryGetFeatureValue(CommonUsages.trigger, out leftTriggerValue);

            var input = new Classes.LocalInput
            {
                isHoldingRightGrip = isHoldingRightGrip,
                isHoldingLeftGrip = isHoldingLeftGrip,
                isHoldingRightTrigger = isHoldingRightTrigger,
                isHoldingLeftTrigger = isHoldingLeftTrigger,

                rightGripValue = rightGripValue,
                leftGripValue = leftGripValue,
                rightTriggerValue = rightTriggerValue,
                leftTriggerValue = leftTriggerValue,
            };

            return input;
        }

        public static float GetDistanceFromPlayer(GameObject obj)
        {
            return Vector3.Distance(GetLocalPlayer().player.transform.position, obj.transform.position);
        }

        public static void VibrateController(bool isLeftController, float strength, float duration)
        {
            GorillaTagger.Instance.StartVibration(isLeftController, strength, duration);
        }
    }

    public class Online
    {
        public static Classes.OnlinePlayer GetOnlinePlayerFromName(string name)
        {
            var player = new Classes.OnlinePlayer();

            VRRig[] vrRigArray = (VRRig[])GameObject.FindObjectsOfType(typeof(VRRig));
            foreach (VRRig rig in vrRigArray)
            {
                if (rig.photonView.Owner.NickName == name)
                {
                    player.position = rig.transform.position;
                    player.distanceFromPlayer = Vector3.Distance(Modding_Library.Local.GetLocalPlayer().headPosition, rig.transform.position);
                    player.viewId = rig.photonView.ViewID;

                    return player;
                }
            }

            return null;
        }

        public static Classes.OnlinePlayer GetOnlinePlayerFromViewID(string id)
        {
            if (Utilities.ContainsLetter(id))
                return null;

            var player = new Classes.OnlinePlayer();

            VRRig[] vrRigArray = (VRRig[])GameObject.FindObjectsOfType(typeof(VRRig));
            foreach (VRRig rig in vrRigArray)
            {
                if (rig.photonView.ViewID == Int32.Parse(id))
                {
                    player.position = rig.transform.position;
                    player.distanceFromPlayer = Vector3.Distance(Modding_Library.Local.GetLocalPlayer().headPosition, rig.transform.position);
                    player.viewId = rig.photonView.ViewID;

                    return player;
                }
            }

            return null;
        }

        public Dictionary<int, Photon.Realtime.Player> GetAllPlayers()
        {
            return PhotonNetwork.CurrentRoom.Players;
        }

        public static class Sound
        {
            
        }
    }
    
    public class World
    {
        public static GameObject CreateObject(PrimitiveType shape, Vector3 pos, Vector3 scale, Color color)
        {
            GameObject obj = GameObject.CreatePrimitive(shape);
            obj.transform.position = pos;
            obj.transform.localScale = scale;
            obj.GetComponent<MeshRenderer>().material.color = color;

            return obj;
        }

        public static void SetColorOfObject(GameObject obj, Color color)
        {
            if (obj.GetComponent<SkinnedMeshRenderer>())
            {
                obj.GetComponent<SkinnedMeshRenderer>().material.color = color;
            } else if (obj.GetComponent<MeshRenderer>())
            {
                obj.GetComponent<MeshRenderer>().material.color = color;
            }
        }

        public static void RotateObjectTowardsObject(GameObject obj, GameObject target, float speed = 1)
        {
            Vector3 targetDirection = target.transform.position - obj.transform.position;
            float singleStep = speed * Time.deltaTime;

            Vector3 newDirection = Vector3.RotateTowards(obj.transform.forward, targetDirection, singleStep, 0.0f);

            obj.transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }
}