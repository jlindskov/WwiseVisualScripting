using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

[UnitTitle("Execute Action On Event")]
[UnitSurtitle("Wwise Event")]
[UnitShortTitle("Execute Action")]
[UnitCategory("Wwise/Event")]
[TypeIcon(typeof(AudioSource))]
public class ExecuteActionWwiseEventUnit : Unit
{
    [DoNotSerialize, PortLabelHidden]
    public ControlInput input;
    
    [DoNotSerialize, PortLabelHidden]
    public ControlOutput output;
    
    [DoNotSerialize,  PortLabel("Event")] 
    public ValueInput wwiseEvent; 
    
    [DoNotSerialize] 
    public ValueInput gameObject; 
    
    [DoNotSerialize] 
    public ValueInput actionType; 
    
    [DoNotSerialize] 
    public ValueInput transitionTime; 
    
    [DoNotSerialize] 
    public ValueInput interpolation; 

    protected override void Definition()
    {
        input = ControlInput("in", Enter);
        output = ControlOutput("out");
        
        wwiseEvent = ValueInput<WwiseEvent>("WwiseEvent");
        gameObject = ValueInput<GameObject>("GameObject", null).NullMeansSelf();
        actionType = ValueInput<AkActionOnEventType>("actionType", AkActionOnEventType.AkActionOnEventType_Stop);
        transitionTime = ValueInput<int>("transitionTime", 0);
        interpolation = ValueInput<AkCurveInterpolation>("interpolation", AkCurveInterpolation.AkCurveInterpolation_Linear);
        Requirement(wwiseEvent, input);
        Succession(input, output);
    }

    private ControlOutput Enter(Flow flow)
    {
        var gameObj = flow.GetValue<GameObject>(gameObject);
        var action = flow.GetValue<AkActionOnEventType>(actionType);
        var transition = flow.GetValue<int>(transitionTime);
        var curve = flow.GetValue<AkCurveInterpolation>(interpolation);
        flow.GetValue<WwiseEvent>(wwiseEvent).ExecuteAction(gameObj, action, transition, curve);
        
        return output;
    }
}
