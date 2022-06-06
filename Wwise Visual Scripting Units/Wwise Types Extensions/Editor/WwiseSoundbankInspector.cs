using System.Collections;
using System.Collections.Generic;
using AK.Wwise.Editor;
using UnityEngine;


[UnityEditor.CustomPropertyDrawer(typeof(WwiseSoundbank))]
public class WwiseSoundbankInspector : BaseTypeDrawer
{ 
    protected override WwiseObjectType WwiseObjectType { get { return WwiseObjectType.Soundbank; } }
}

