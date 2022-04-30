using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemManager : MonoBehaviour
{
    // 아이템 사용 시
    protected abstract void UseItem();

    // 아이템 얻었을 때
    protected abstract void GetItem();

    // 아이템 갯수가 0일 때
    protected abstract void ItemZero();
}
