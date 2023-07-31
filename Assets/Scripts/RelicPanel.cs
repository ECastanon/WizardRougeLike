using UnityEngine;

public class RelicPanel : MonoBehaviour
{
    private Animator anim;
    private bool inView = false;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    //Called In EnemyCounter
    public void SlideInMenu()
    {
        if (!inView)
        {
            anim.Play("RelicPanelSlideIn");
            inView = true;
            Time.timeScale = 0f;
        }
    }

    public void SlideOutMenu()
    {
        if (inView)
        {
            anim.Play("RelicPanelSlideOut");
            inView = false;
            Time.timeScale = 1f;
        }
    }
}
