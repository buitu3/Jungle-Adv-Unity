using UnityEngine;
using System.Collections;

public class BackGroundScroller : MonoBehaviour {

    public float speed;

    public static BackGroundScroller current;

    float pos = 0f;

	// Use this for initialization
	void Start () {
        current = this;
	}
	
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
