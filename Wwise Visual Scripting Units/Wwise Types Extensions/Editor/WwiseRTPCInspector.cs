using System.Collections;
using System.Collections.Generic;
using AK.Wwise.Editor;
using UnityEngine;


[UnityEditor.CustomPropertyDrawer(typeof(WwiseRTPC))]
public class WwiseRTPCInspector : BaseTypeDrawer
{ 
    protected override WwiseObjectType WwiseObjectType { get { return WwiseObjectType.GameParameter; } }
}

