using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class AllWinnerPanel : MonoBehaviour
{
  public WinAnim anim;
  public Transform mainHolder;
  public PrizeTableHolder[] holders;
  // public TableLine linePrefab;

  public Coroutine Init(Dictionary<int, List<Card>> vencedores)
  {
    gameObject.SetActive(true);
    return StartCoroutine(InitCor(vencedores));
  }
  public IEnumerator InitCor(Dictionary<int, List<Card>> vencedores)
  {

    yield return anim.Animation();

    mainHolder.localScale = Vector3.zero;
    mainHolder.gameObject.SetActive(true);

    // for (var i = 0; i < holders.Length; i++)
    // {
    //   var index = i + 4;
    //   var holder = holders[i];
    //   foreach (var cart in vencedores[index])
    //   {
    //     var line = Instantiate(linePrefab, holder.holder);
    //     line.Setup(cart);
    //   }
    // }

    mainHolder.DOScale(1, 1f);

  }
}
