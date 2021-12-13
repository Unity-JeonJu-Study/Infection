using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;




[CreateAssetMenu(fileName = "SoundData", menuName = "Data/Sound")]
public class SoundTrack : SerializedScriptableObject
{
    // ...
    public BGMList stageBGM;



    private ValueDropdownList<string> BGMTrack = new ValueDropdownList<string>()
    {
        { "${GameStage.Laboratory}", "bgm1" }
    };
}
