using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPack : MonoBehaviour,IItem
{
    public int ammo = 30;

    //target�� ź���� �߰��ϴ� ó��
    public void Use(GameObject target)
    {
        Debug.Log("ź���� �����ߴ� : " + ammo);
    }

}
