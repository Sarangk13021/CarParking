using UnityEngine;
using UnityEngine.UI;
public class CircleSlider : MonoBehaviour
{
 
     public bool b=true;
	 public Image image;
	 public float speed=0.5f;

  public float time =0f;
  
  public Text progress;
  
  void Start()
  {
	  
	image = GetComponent<Image>();
  }
  
    void Update()
    {
		if(b)
		{
			time-=Time.deltaTime*speed;
			image.fillAmount= time;
			if(progress)
			{
				progress.text = (int)(image.fillAmount*100)+"%";
			}
			
        if(time <=0)
		{
						
			time=100;
		}
            if(time*100>70 && time <=100)
            {
                image.color = Color.green;
            }
            else if (time*100 >40 && time <=70)
			{
				image.color = Color.yellow;
			}
            else if (time * 100 >20 && time <=40)
            {
				Color color = new Color(1f, 0.53f, 1f);
                image.color = color;
            }
            else
            {
				image.color = Color.red;
			}
			Debug.Log(time*100);
        }
	}
	
	
}
