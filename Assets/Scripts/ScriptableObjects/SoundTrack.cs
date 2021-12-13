using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using UnityEngine;




[CreateAssetMenu(fileName = "SoundData", menuName = "Data/Sound")]
public class SoundTrack : SerializedScriptableObject
{
    
    [ValueDropdown("BGMTrack")]
    public string stageBGM;
    
    private ValueDropdownList<string> BGMTrack = new ValueDropdownList<string>()
    {
        { $"{GameStage.Laboratory}", "labBGM" },
        { $"{GameStage.Stage1}", "stage1BGM" },
        { $"{GameStage.Stage2}", "stage2BGM" },
        { $"{GameStage.Stage3}", "stage3BGM" }
    };
}