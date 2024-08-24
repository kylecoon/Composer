using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArticulationMenu : MonoBehaviour
{
    public GameObject StacattoIcon;
    public GameObject TenutoIcon;
    public GameObject TieIcon;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreateStaccato()
    {
        EventBus.Publish(new ArticulationSelect('s'));
    }

    public void CreateTenuto()
    {
        EventBus.Publish(new ArticulationSelect('o'));
    }

    public void CreateTie()
    {
        EventBus.Publish(new ArticulationSelect('t'));
    }


}
