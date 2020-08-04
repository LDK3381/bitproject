using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MtWeaponManager : MonoBehaviourPun, IPunObservable
{
    public int selectedWeapon = 0;

    // 무기 선택함수
    [PunRPC]
    public void SelectWeapon()
    {
        try
        {
            int i = 0;
            foreach (Transform weapon in transform)
            {
                if (i == selectedWeapon)
                    weapon.gameObject.SetActive(true);
                else
                    weapon.gameObject.SetActive(false);
                i++;
            }
        }
        catch
        {
            Debug.Log("MtWeaponManager.SelectWeapon Error");
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        try
        {
            if (stream.IsWriting)
            {
                stream.SendNext(selectedWeapon);
            }
            else
            {
                selectedWeapon = (int)stream.ReceiveNext();
            }
        }
        catch
        {
            Debug.Log("MtWeaponManager.OnPhotonSerializeView Error");
        }
    }
}
