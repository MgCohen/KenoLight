using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class WinnerPanel : MonoBehaviour
{
    public Game manager;
    public Transform mountPoint;
    public GameObject holderPrefab;
    public CardView cardPrefab;
    public List<WinAnim> anims;

    //Constantes
    private readonly Color _transparent = new Color(1, 1, 1, 0);
    //Tempo que a Cartela fica sendo exibida na tela
    private readonly WaitForSeconds _showTimer = new WaitForSeconds(5f);

    public Coroutine ShowWinners(IList<Card> carts, int prize, Action onComplete = null)
    {
        return StartCoroutine(WinnersCor(carts, prize, onComplete));
    }

    private IEnumerator WinnersCor(IList<Card> cartelas, int prize, Action onComplete)
    {
        yield return null;
        //var index = prize - 4;
        //var anim = anims[index];
        //var winnerSize = cartelas.Count;
        //var prizeText = anim.prizeText;
        //prizeText.color *= _transparent;

        //var valorPremio = manager.sorteio?.prizes[prize - 4] ?? 0;
        //// Caso não seja acumulado
        //if (prize < 7)
        //  prizeText.text = $"{valorPremio}<size=70>/{winnerSize}</size>";
        //else
        //{
        //  //valor do Keno
        //  var keno = manager.sorteio?.prizes[prize - 4] ?? 0;
        //  //valor do Acumulado
        //  var acc = valorPremio;

        //  prizeText.text = $"{keno} + {acc} = {(keno + acc)}<size=70>/{winnerSize}</size>";
        //}

        ////Animação das bolas explodindo | fundo aparecendo
        //anim.gameObject.SetActive(true);
        //yield return anim.Animation();

        ////Animação das Cartelas aparecendo
        //foreach (Transform c in mountPoint)
        //{
        //  Destroy(c.gameObject);
        //}

        //mountPoint.gameObject.SetActive(true);

        //var holderCount = (winnerSize / 4) + 1;
        //var holders = new Transform[holderCount];

        ////Instanciar os holders, cada holder guarda 4 cartelas e cada um é mostrado na sua vez
        //if (winnerSize > 4)
        //{
        //  for (var i = 0; i < holderCount; i++)
        //  {
        //    var holder = Instantiate(holderPrefab, mountPoint).transform;
        //    holder.localScale = Vector3.zero;
        //    holders[i] = holder;
        //  }
        //}
        //else
        //{
        //  var holder = Instantiate(holderPrefab, mountPoint).transform;
        //  holder.localScale = Vector3.zero;
        //  holders[0] = holder;
        //}


        //// Instanciar cada cartela
        //for (var i = 0; i < winnerSize; i++)
        //{
        //  var c = cartelas[i];

        //  if (c == null) continue;
        //  var cart = Instantiate(cardPrefab, holders[i / 4]);
        //  cart.Setup(c);
        //  cart.CatchUp(manager.usedBalls);
        //}


        //holders[0].gameObject.SetActive(true);
        //prizeText.DOFade(1, 1f);


        ////TODO: criar mais escalas possiveis?
        //var targetScale = winnerSize == 1 ? 2 : 1; // Caso seja uma cartela só, mostrar com o dobro do tamanho
        //// Tempo até a cartela expandir na tela
        //yield return holders[0].DOScale(targetScale, 0.75f).SetEase(Ease.OutBack).WaitForCompletion();
        //yield return _showTimer; // Tempo que as cartelas ficam visiveis
        //// Apagar cartela na tela
        //yield return holders[0].DOScale(0, 0.75f).WaitForCompletion();

        //if (holderCount > 1)
        //{
        //  foreach (var holder in holders)
        //  {
        //    // targetScale = holder.childCount == 1 ? 2 : 1;
        //    //Mostrar
        //    yield return holder.DOScale(targetScale, 0.75f).WaitForCompletion();
        //    yield return _showTimer;
        //    //Apagar
        //    yield return holder.DOScale(0, 0.75f).WaitForCompletion();
        //  }
        //}

        //// Apagar texto
        //prizeText.DOFade(0, 0.75f);

        //// Apagar fundo/bolas/moedas/efeitos misc
        //anim.Close(1);

        //mountPoint.gameObject.SetActive(false);

        //// Fim de Tudo
        //onComplete?.Invoke();
    }

}