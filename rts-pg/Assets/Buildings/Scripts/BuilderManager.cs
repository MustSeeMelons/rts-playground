using UnityEngine;

public class BuilderManager : MonoBehaviour {

    public GameObject townCenter;
    public GameObject house;

    public void BuildTownCenter() {
        EventManager.TriggerEvent(CEvents.BUILD, new BuildMessage(townCenter));
    }

    public void BuildHouse() {
        EventManager.TriggerEvent(CEvents.BUILD, new BuildMessage(house));
    }
}
