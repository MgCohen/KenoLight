using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class Cortina : MonoBehaviour
{
  [SerializeField] int tempoPassarTelas = 2;
  [SerializeField] private GameObject tempObject;
  [SerializeField] private GameObject activeObject;
  [SerializeField] private Animator animator;


  //[SerializeField] private EventHandler handler;

  [SerializeField] private List<GameObject> telasNormais = new List<GameObject>();
  [SerializeField] private List<GameObject> telasEspeciais;  //sorteio especial, tela oferecimento
  [SerializeField] private List<GameObject> telasSuperEspeciais;   //sorteio super especial, tela oferecimento

  private bool podePassar;
  private int active;
  private bool chameiEvento;
  


  private void OnEnable() {
    activeObject = telasNormais[0];
    // handler.SorteioEspecial.AddListener(AddSorteioEspecial);
    // handler.SorteioSuperEspecial.AddListener(AddSorteioSuperEspecial);
    
    StartCoroutine(OpenCoroutine());

  }
  private void OnDisable() {
    // handler.SorteioEspecial.RemoveListener(AddSorteioEspecial);
    // handler.SorteioSuperEspecial.RemoveListener(AddSorteioSuperEspecial);
  }

//AtivarTransicao e DesativarTransicao chamados pela animação

public void AddSorteioEspecial(Sorteio sorteio){
  // if(sorteio.isSpecial){
  //   for(int i = 0;i < telasEspeciais.Count; i++){
  //     telasNormais.Add(telasEspeciais[i]);
  // }
  //}
    
}
public void AddSorteioSuperEspecial(Sorteio sorteio){
// if(sorteio.isSuperSpecial){
//     for(int i = 0;i < telasSuperEspeciais.Count; i++){
//       telasNormais.Add(telasSuperEspeciais[i]);
//   }
//}
  
}
  public IEnumerator OpenCoroutine()
  { 
    //tempo para verificar se especial e super especial e atualizar telasNomais.count  
    yield return new WaitForSeconds(2);
    
     for(int i = 0; i < telasNormais.Count;i++)
    {
      if(i == telasNormais.Count -1){
        animator.SetTrigger("anim");
        yield return new WaitUntil(() => podePassar == true);
        telasNormais[0].SetActive(true);
        telasNormais[telasNormais.Count -1].SetActive(false);
        podePassar = false;

        i = 0;
        yield return new WaitForSeconds(tempoPassarTelas);
      }
        animator.SetTrigger("anim");
        yield return new WaitUntil(() => podePassar == true);
        telasNormais[i+1].SetActive(true);
        podePassar = false;


        yield return new WaitForSeconds(tempoPassarTelas);

        telasNormais[i].SetActive(false);
    }
    yield return StartCoroutine(OpenCoroutine());
  }
    public void AtivarTransicao(){
      podePassar = true;
    //activeObject.SetActive(true);

  }
}
