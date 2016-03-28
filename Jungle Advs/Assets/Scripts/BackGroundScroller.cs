using UnityEngine;
using System.Collections;

public class BackGroundScroller : MonoBehaviour {

    //==============================================
    // Constants
    //==============================================

    public static BackGroundScroller current;

    //==============================================
    // Fields
    //==============================================

    public float speed;
    float pos = 0f;

    //==============================================
    // Unity Methods
    //==============================================

    // Use this for initialization
    void Start () {
        current = this;
	}

    //==============================================
    // Methods
    //==============================================

    public void Go(float h)
    {
        pos += speed * Time.deltaTime * h;
        if (pos > 1f)
        {
            pos -= 1f;
        }
        
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(pos, 0);
    }
}
