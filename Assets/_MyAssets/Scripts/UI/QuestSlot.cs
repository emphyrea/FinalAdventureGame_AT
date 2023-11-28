using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestSlot : MonoBehaviour
{

    [SerializeField] string QuestTitle;
    [SerializeField] string QuestDetails;
    [SerializeField] Image completedImg;
    [SerializeField] Image inProgressImg;

    // Start is called before the first frame update
    void Start()
    {
        completedImg.gameObject.SetActive(false);
        inProgressImg.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
