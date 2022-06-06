using System.Collections;
using System.Collections.Generic;
using AK.Wwise.Editor;
using UnityEngine;


[UnityEditor.CustomPropertyDrawer(typeof(WwiseEvent))]
public class WwiseEventInspector : BaseTypeDrawer
{ 
    protected override WwiseObjectType WwiseObjectType { get { return WwiseObjectType.Event; } }
}

