using System.Collections;
using System.Collections.Generic;
using AK.Wwise.Editor;
using UnityEngine;


[UnityEditor.CustomPropertyDrawer(typeof(WwiseTrigger))]
public class WwiseTriggerInspector : BaseTypeDrawer
{ 
    protected override WwiseObjectType WwiseObjectType { get { return WwiseObjectType.Trigger; } }
}

