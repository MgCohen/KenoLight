using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class WinAnim : MonoBehaviour
{
  public Image bg;
  // public Transform CardHolder;
  public Transform[] nameTargets;
  public Transform[] ballTargets;

  public Image[] names;
  public Image[] balls;

  public float baseTime;
  public TextMeshProUGUI prizeText;
  public Image coins;

  private readonly Color _transparent = new Color(1, 1, 1, 0);


  public IEnumerator Animation()
  {
    bg.color *= _transparent;
    coins.gameObject.SetActive(true);

    foreach (var n in names)
    {
      n.transform.localScale = Vector3.zero;
    }

    foreach (var b in balls)
    {
      b.transform.localScale = Vector3.zero;
    }

    // Espera o bg aparecer
    yield return bg.DOFade(1, baseTime * 0.4f).WaitForCompletion();


    // Explosão de cada bola pro seu luagr

    var duration = baseTime * 1.3f;

    for (var i = 0; i < balls.Length; i++)
    {
      var ball = balls[i];
      var target = ballTargets[i].localPosition;
      var endValue = 1 + Random.Range(-0.5f, 0.4f);

      ball.transform.DOLocalMove(target, duration).SetEase(Ease.OutElastic);
      ball.transform.DOScale(endValue, duration).SetEase(Ease.OutElastic);
    }

    duration = baseTime * 0.9f;

    // Explosão do nome
    names[0].transform.DOScale(1f, duration).SetEase(Ease.OutElastic);
    yield return names[1].transform.DOScale(1.8f, duration).SetEase(Ease.OutElastic).WaitForCompletion();

    duration = baseTime * 0.36f;
    // Redução do nome
    names[0].transform.DOScale(1f / 2.4f, duration).SetEase(Ease.OutBack);
    names[1].transform.DOScale(1.8f / 2.4f, duration).SetEase(Ease.OutBack);
    //Posicionamento final do nome
    names[0].transform.DOLocalMove(nameTargets[0].localPosition, duration).SetEase(Ease.OutBack);
    names[1].transform.DOLocalMove(nameTargets[1].localPosition, duration).SetEase(Ease.OutBack);
  }

  public void Close(float time)
  {
    time *= 0.7f;

    // Nomes
    foreach (var n in names)
    {
      n.DOFade(0, time);
    }

    // Bolas
    foreach (var b in balls)
    {
      b.DOFade(0, time);
    }

    // Animação de Moedas
    coins.DOFade(0, time);

    // Fundo
    bg.DOFade(0, time).OnComplete(() =>
    {
      gameObject.SetActive(false);
      coins.gameObject.SetActive(false);
      coins.color = Color.white; ;
    });

  }
}