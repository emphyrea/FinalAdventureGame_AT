using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUI : MonoBehaviour
{

    [SerializeField] GameObject questUIObj;

    // Start is called before the first frame update
    void Start()
    {
        questUIObj.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            questUIObj.gameObject.SetActive(true);
        }
    }
}
