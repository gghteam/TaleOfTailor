using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 플레이어 스크립트 대신 만듦
/// </summary>
public class PlayerAnim : MonoBehaviour
{
   private Animator playerAnimator;

    EventParam eventParam = new EventParam();

    private void Start()
    {
        EventManager.StartListening("ITEMANIMPLAY", ItemAnimPlay);
    }
    private void OnDestroy()
    {
        EventManager.StopListening("ITEMANIMPLAY", ItemAnimPlay);
    }
    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
    }

    // 일단 HoldItem 하나로 대체
    void ItemAnimPlay(EventParam eventParam)
    {
        playerAnimator.Play("HoldItem");
    }


}
