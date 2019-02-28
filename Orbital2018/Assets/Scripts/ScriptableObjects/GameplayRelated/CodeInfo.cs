using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type { Executable, Conditional }

[CreateAssetMenu(menuName = "CodeInfo")]
public class CodeInfo : ScriptableObject, IDescribable {

    public string description;
    public string title;
    public Type type;

    public string GetDescription()
    {
        string titleColor = string.Empty;
        string descriptionColor = "#000000"; // Green

        switch (type)
        {
            case Type.Executable:
                titleColor = "#5C5C5C"; // Dark Grey
                break;
            case Type.Conditional:
                titleColor = "#A11616"; // Dark Red
                break;
        }

        return string.Format("<color={0}>{1}</color>", titleColor, title) + string.Format("\n<color={0}>{1}</color>", descriptionColor,description);
    }


}
