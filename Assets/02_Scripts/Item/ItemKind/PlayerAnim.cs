using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �÷��̾� ��ũ��Ʈ ��� ����
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

    // �ϴ� HoldItem �ϳ��� ��ü
    void ItemAnimPlay(EventParam eventParam)
    {
        playerAnimator.Play("HoldItem");
    }


}
