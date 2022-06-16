using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RankDetails : MonoBehaviour
{
    public List<Image> images;
    public TextMeshProUGUI character;
    private void Update()
    {
        transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    }
}
