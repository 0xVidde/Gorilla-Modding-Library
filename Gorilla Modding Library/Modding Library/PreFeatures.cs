using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace Modding_Library
{
    public class PremadeFeatures
    {
        public static void Fly()
        {
            Classes.LocalPlayer Player = Local.GetLocalPlayer();
            Player.player.GetComponent<Rigidbody>().velocity = (Player.player.headCollider.transform.forward * Time.deltaTime) * 1400f;
        }

        public static void TagAll()
        {
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                PhotonView.Get(GorillaGameManager.instance.GetComponent<GorillaGameManager>()).RPC("ReportTagRPC", RpcTarget.MasterClient, new object[]
                {
                    player
                });
            }
        }

        public static void TeleportToRandomPlayer()
        {
            System.Random random = new System.Random();
            int num = random.Next(PhotonNetwork.PlayerList.Length);

            PhotonView photonView = GorillaGameManager.instance.FindVRRigForPlayer(PhotonNetwork.PlayerList[num]);

            if (photonView != null)
            {
                Local.GetLocalPlayer().player.transform.position = photonView.transform.position;
                Local.GetLocalPlayer().player.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
            }
        }

        public static void TeleportToPosition(Vector3 pos)
        {
            Local.GetLocalPlayer().player.transform.position = pos;
            Local.GetLocalPlayer().player.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
        }
    }
}
