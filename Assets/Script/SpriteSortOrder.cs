using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSortOrder : MonoBehaviour
{
    public SpriteRenderer TheSR;

    void Start()
    {
        TheSR = GetComponent<SpriteRenderer>();
        TheSR.sortingOrder = Mathf.RoundToInt(this.transform.position.y * -10f);
    }
}
