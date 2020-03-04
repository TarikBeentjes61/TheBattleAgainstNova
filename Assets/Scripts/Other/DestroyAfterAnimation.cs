using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DestroyAfterAnimation : MonoBehaviour
{
    //Simpel script om iets na een animatie te verwijderen. Goed voor explosies.
    void Start () {
        Destroy (gameObject, GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length); 
    }
}
