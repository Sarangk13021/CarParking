using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Bomb : MonoBehaviour
{
    public GameObject Panel;
    public GameObject BombEffect;
    public AudioSource bombsound;
    private MeshRenderer mesh;
    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();   
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("tyre"))
        {
            Instantiate(BombEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            bombsound.volume = 0.3f;
            bombsound.Play();
            mesh.enabled = false;
            StartCoroutine(BombBlast());
        }
    }
    IEnumerator BombBlast()
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0;
        Panel.SetActive(true);
    }
}
