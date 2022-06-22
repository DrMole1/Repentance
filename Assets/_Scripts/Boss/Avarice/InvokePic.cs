using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class InvokePic : MonoBehaviour
{
    public GameObject pic;
    public GameObject ptcRockPrefab;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MovePic());
    }

    IEnumerator MovePic()
    {
        yield return new WaitForSeconds(2f);

        LeanTween.moveY(pic, pic.transform.position.y + 5f, 0.3f);
        // ptc Rock
        GameObject ptcRockInstance;
        ptcRockInstance = Instantiate(ptcRockPrefab, new Vector2(transform.position.x, transform.position.y + 1f), Quaternion.identity);
        Destroy(ptcRockInstance, 5f);

        yield return new WaitForSeconds(3f);

        Destroy(transform.GetChild(0).gameObject);
        LeanTween.alpha(transform.GetChild(1).gameObject, 0f, 0.2f);
        LeanTween.value(gameObject, gameObject.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().intensity, 0f, 0.2f).setOnUpdate((float val) => {
            gameObject.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().intensity = val;
        });

        Destroy(gameObject, 0.2f);
    }
}
