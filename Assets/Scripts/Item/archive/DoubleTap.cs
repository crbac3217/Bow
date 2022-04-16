using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class DoubleTap : MonoBehaviour
{
    public float lastTap, dtapCD = 0.3f, dashCD = 5f, dashAmount = 20f, dashDuration = 0.35f;
    public bool isDashAvail = true;
    public Vector2 savedVelocity = new Vector2();
    public object[] parameters;
    public GameObject after;
    public GUIManager guiManager;
    public static DoubleTap instance;
    PlayerControl pc;
    PlayerMove pm;

    private void Start()
    {
        Debug.Log("startedrang");
        after = Resources.Load("Prefabs/Afterimage") as GameObject;
        instance = this;
        pc = GetComponent<PlayerControl>();
        guiManager = pc.guiManager;
        pm = GetComponent<PlayerMove>();
        guiManager.ButtonAdd(this.gameObject, this.GetType(), this.GetType().GetMethod("OnTapRight"), parameters, "rButton", EventTriggerType.PointerDown);
        guiManager.ButtonAdd(this.gameObject, this.GetType(), this.GetType().GetMethod("OnTapLeft"), parameters, "lButton", EventTriggerType.PointerDown);
        Debug.Log("rang");
    }
    public void OnTapRight()
    {
        if (((Time.time-lastTap) < dtapCD)&&isDashAvail)
        {
            float moveSpeed = pc.stats[0].value;
            pm.modifierSpeed = 1 * dashAmount * moveSpeed*0.05f;
            StartCoroutine(Dashing());
            isDashAvail = false;
            StartCoroutine(DashCD());
        }
        else
        {
            lastTap = Time.time;
        }
    }
    public void OnTapLeft()
    {
        if (((Time.time - lastTap) < dtapCD) && isDashAvail)
        {
            float moveSpeed = pc.stats[0].value;
            pm.modifierSpeed = -1 * dashAmount * moveSpeed*0.05f;
            StartCoroutine(Dashing());
            isDashAvail = false;
            StartCoroutine(Dashing());
        }
        else
        {
            lastTap = Time.time;
        }
    }
    IEnumerator DashCD()
    {
        yield return new WaitForSeconds(dashCD);
        isDashAvail = true;

    }
    IEnumerator Dashing()
    {
        Color color = pm.gameObject.GetComponent<SpriteRenderer>().color;
        Color dashColor = new Color(color.r, color.g, color.b, color.a * 0.5f);
        pm.gameObject.GetComponent<SpriteRenderer>().color = dashColor;
        float time = Time.time + dashDuration;
        AfterImage(time);
        yield return new WaitForSeconds(dashDuration / 5);
        AfterImage(time);
        yield return new WaitForSeconds(dashDuration / 5);
        AfterImage(time);
        yield return new WaitForSeconds(dashDuration / 5);
        AfterImage(time);
        yield return new WaitForSeconds(dashDuration / 5);
        AfterImage(time);
        yield return new WaitForSeconds(dashDuration / 5);
        pm.gameObject.GetComponent<SpriteRenderer>().color = color;
        pm.modifierSpeed = 0;
        pm.isMoving = false;
    }
    public void AfterImage(float time)
    {
        var afterImageObj = Instantiate(after, this.transform.position, Quaternion.identity);
        AfterImage afterImage = afterImageObj.GetComponent<AfterImage>();
        afterImage.destroyTime = time;
        afterImage.spriteRenderer.sprite = this.gameObject.GetComponent<SpriteRenderer>().sprite;
        afterImage.spriteRenderer.color = new Color(0,0,255,100);
    }

    private void OnDestroy()
    {
        guiManager.ButtonRemove(this.GetType(), this.GetType().GetMethod("OnTapLeft"), "lButton");
        guiManager.ButtonRemove(this.GetType(), this.GetType().GetMethod("OnTapRight"), "rButton");
    }
}
