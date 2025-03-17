using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialAdvanceZone : MonoBehaviour
{

    private JoinPlayer parentManager;

    [HideInInspector] public int myIndex;

    private bool advance = false;

    private float timer = 0;

    public float timeToAdvance = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        parentManager = GetComponentInParent<JoinPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (advance)
        {
            timer += Time.deltaTime;
            if (timer > timeToAdvance)
            {
                parentManager.DisableDashTutorial(myIndex);
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            advance = true;
        }
    }
}
