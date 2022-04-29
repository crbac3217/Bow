using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundParallax : MonoBehaviour
{
    public List<GameObject> midGrounds, backGrounds;
    public List<Sprite> backSprites, midSprites;
    public GameObject backgParallax, midgParallax;

    public Vector2 backMult, midMult;
    public Transform camPar;
    private Vector2 lastCampos;

    //private void LateUpdate()
    //{
    //    Vector2 moveMent = (Vector2)camPar.position - lastCampos;
    //    foreach (GameObject b in backGrounds)
    //    {
    //        b.transform.position += new Vector3(moveMent.x * backMult.x, moveMent.y * backMult.y, 0);
    //    }
    //    foreach (GameObject m in midGrounds)
    //    {
    //        m.transform.position += new Vector3(moveMent.x * midMult.x, moveMent.y * midMult.y, 0);
    //    }
    //    if (backGrounds[backGrounds.Count -1].transform.localPosition.x <= 0)
    //    {
    //        var backg = Instantiate(backgParallax, new Vector3(backGrounds[backGrounds.Count - 1].transform.position.x + 8, backGrounds[backGrounds.Count - 1].transform.position.y, 0), Quaternion.identity);
    //        backg.transform.SetParent(this.transform);
    //        int randInd = Random.Range(0, backSprites.Count);
    //        backg.GetComponent<SpriteRenderer>().sprite = backSprites[randInd];
    //        backGrounds.Add(backg);
    //        backGrounds.Remove(backGrounds[0]);
    //    }
    //    if (midGrounds[midGrounds.Count - 1].transform.localPosition.x <= 0)
    //    {
    //        var midg = Instantiate(midgParallax, new Vector3(midGrounds[midGrounds.Count - 1].transform.position.x + 8, midGrounds[midGrounds.Count - 1].transform.position.y, 0), Quaternion.identity);
    //        midg.transform.SetParent(this.transform);
    //        int randInd = Random.Range(0, midSprites.Count);
    //        midg.GetComponent<SpriteRenderer>().sprite = midSprites[randInd];
    //        midGrounds.Add(midg);
    //        midGrounds.Remove(midGrounds[0]);
    //    }
    //    lastCampos = camPar.position;
    //}
}
