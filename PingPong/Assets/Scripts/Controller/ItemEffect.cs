using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffect : MonoBehaviour
{
    public enum EITEM
    {
        None = 0,
        Speed,
        ScaleBall
    }

    public EITEM m_itemType = EITEM.None;
}
