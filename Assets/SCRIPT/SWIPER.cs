
using UnityEngine;
using UnityEngine.UI;

public class SWIPER : MonoBehaviour
{
    public GameObject ScrollBar;
    float scroll_pos;
    float[] Pos;

    void Start()
    {
        int count = transform.childCount;
        Pos = new float[count];
        float distance = 1f / (count - 1f);
        for (int i = 0; i < count; i++) Pos[i] = distance * i;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
            scroll_pos = ScrollBar.GetComponent<Scrollbar>().value;
        else
            foreach (float p in Pos)
                if (Mathf.Abs(scroll_pos - p) < 0.5f / (Pos.Length - 1f))
                    ScrollBar.GetComponent<Scrollbar>().value = Mathf.Lerp(ScrollBar.GetComponent<Scrollbar>().value, p, 0.1f);

        for (int i = 0; i < Pos.Length; i++)
        {
            Transform child = transform.GetChild(i);
            bool isFocused = Mathf.Abs(scroll_pos - Pos[i]) < 0.5f / (Pos.Length - 1f);
            child.localScale = Vector3.Lerp(child.localScale, isFocused ? Vector3.one : new Vector3(0.85f, 0.85f, 1f), 0.1f);
        }
    }
}