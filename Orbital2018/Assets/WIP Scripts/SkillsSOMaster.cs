using UnityEngine;

public class SkillsSOMaster : MonoBehaviour {

    public static SkillsSOMaster instance;

    void Start()
    {
        SkillsSOMaster.instance = this;
    }

}
