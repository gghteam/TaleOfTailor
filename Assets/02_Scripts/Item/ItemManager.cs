using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemManager : MonoBehaviour
{
    [SerializeField, Header("������")]
    protected GameObject item;
    [SerializeField, Header("��� �ð�")]
    protected int useTime;

    // ������ ��� ��
    protected abstract void UseItem();

    // ������ ����� ��
    protected abstract void GetItem();

    // ������ ������ 0�� ��
    protected abstract void ItemZero();
}
