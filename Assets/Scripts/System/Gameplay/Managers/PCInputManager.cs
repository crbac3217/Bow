using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCInputManager : MonoBehaviour
{
    public PlayerControl pc;
    private Dictionary<string, KeyCode> dic = new Dictionary<string, KeyCode>();
    public string[] keys = new string[] { };
    public string[] defaultVal = new string[]{};
    private void Start()
    {
        foreach (string str in keys)
        {
            SetUpKey(str, System.Array.IndexOf(keys, str));
        }
    }
    private void SetUpKey(string key, int index)
    {
        if (PlayerPrefs.HasKey(key))
        {
            dic.Add(key, (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(key)));
        }
        else
        {
            dic.Add(key, (KeyCode)System.Enum.Parse(typeof(KeyCode), defaultVal[index]));
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(dic["MoveForward"]))
        {
            pc.pm.MoveRight();
        }
        if (Input.GetKeyDown(dic["MoveBackward"]))
        {
            pc.pm.MoveLeft();
        }
        if (Input.GetKey(dic["MoveForward"]) || Input.GetKey(dic["MoveBackward"]))
        {
            pc.pm.isMoving = true;
        }
        if (Input.GetKeyDown(dic["Jump"]))
        {
            pc.pj.Jump();
        }
        if (Input.GetKeyUp(dic["MoveForward"]))
        {
            pc.pm.LetGoRight();
        }
        if (Input.GetKeyUp(dic["MoveBackward"]))
        {
            pc.pm.LetGoLeft();
        }
        if (Input.GetKeyDown(dic["SkillButton1"]))
        {
            pc.OnSkillPress(1);
        }
        if (Input.GetKeyDown(dic["SkillButton2"]))
        {
            pc.OnSkillPress(2);
        }
        if (Input.GetKeyDown(dic["SkillButton2"]))
        {
            pc.OnSkillPress(3);
        }
        if (Input.GetKeyDown(dic["SkillButton1"]))
        {
            pc.OnSkillRelease(1);
        }
        if (Input.GetKeyDown(dic["SkillButton2"]))
        {
            pc.OnSkillRelease(2);
        }
        if (Input.GetKeyDown(dic["SkillButton2"]))
        {
            pc.OnSkillRelease(3);
        }
    }
}
