using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class AgroAreaMonstre : MonoBehaviour
{
    public Transform headMonster;
    public GameObject lightFaceMonster;
    ScriptMonstre behaviour;

    void Awake()
    {
        behaviour = transform.parent.gameObject.GetComponent<ScriptMonstre>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Quand le joueur touche l'aire d'aggro
        if (other.gameObject.tag == "Player")
        {
            if (behaviour.isMoving == false)
            {
                StartCoroutine(GoMove());
            }
        }
    }

    IEnumerator GoMove()
    {
        LeanTween.value(gameObject, -90f, 0f, 4f).setOnUpdate((float val) =>
        {
            headMonster.rotation = Quaternion.Euler(headMonster.rotation.x, headMonster.rotation.y, val);
        });

        yield return new WaitForSeconds(2f);

        LeanTween.value(gameObject, 0f, 2f, 2f).setOnUpdate((float val) =>
        {
            lightFaceMonster.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().intensity = val;
        });

        yield return new WaitForSeconds(2f);

        behaviour.StartMove();
        behaviour.isMoving = true;
    }
}
