using System.Collections;
using System.Collections.Generic;
using AK.Wwise.Editor;
using UnityEngine;


[UnityEditor.CustomPropertyDrawer(typeof(WwiseState))]
public class WwiseStateInspector : BaseTypeDrawer
{ 
    protected override WwiseObjectType WwiseObjectType { get { return WwiseObjectType.State; } }
}

