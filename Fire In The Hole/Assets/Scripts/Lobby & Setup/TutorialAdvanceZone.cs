using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialAdvanceZone : MonoBehaviour
{

    private JoinPlayer parentManager;

    [HideInInspector] public int myIndex;

    // Start is called before the first frame update
    void Start()
    {
        parentManager = GetComponentInParent<JoinPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            parentManager.DisableDashTutorial(myIndex);
            gameObject.SetActive(false);
        }
    }
}
