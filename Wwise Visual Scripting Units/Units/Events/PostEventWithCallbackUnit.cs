using System;
using AK.Wwise;
using Unity.VisualScripting;
using UnityEngine;


[UnitSurtitle("Wwise Event")]
[UnitShortTitle("Post With Callback")]
[UnitTitle("Post Event With Callback")]
[UnitCategory("Wwise/Event")]
[TypeIcon(typeof(AudioSource))]
public class PostEventWithCallbackUnit : Unit
{
    [DoNotSerialize,PortLabelHidden]
    public ControlInput input;
    
    [DoNotSerialize,PortLabelHidden]
    public ControlOutput output;
    
    [DoNotSerialize]
    public ControlOutput onCallback;
    
    [DoNotSerialize, PortLabel("Event")] 
    public ValueInput wwiseEvent; 
    
    [DoNotSerialize] 
    public ValueInput gameObject; 
    
    [DoNotSerialize]
    public ValueInput callbackMask;
    
    [DoNotSerialize]
    public ValueOutput id;
    
    private uint eventId;
    private GraphReference graphReference { get; set; }
    
    protected override void Definition()
    {
        input = ControlInput("in", Enter);
        output = ControlOutput("out");
        onCallback = ControlOutput("OnCallback");
       
        wwiseEvent = ValueInput<WwiseEvent>("event");
        gameObject = ValueInput<GameObject>("gameObject", null).NullMeansSelf();
        callbackMask = ValueInput<AK.Wwise.CallbackFlags>("callbackFlag");
        
        id = ValueOutput<uint>("eventID", (flow) => eventId);
        
        Requirement(wwiseEvent, input);
        Requirement(callbackMask, input);
        Succession(input, output);
        Succession(input, onCallback);
    }

    private ControlOutput Enter(Flow flow)
    {
        var callbackType = flow.GetValue<AK.Wwise.CallbackFlags>(callbackMask);
        var gameObj = flow.GetValue<GameObject>(gameObject);
        graphReference = flow.stack.AsReference();
        eventId = flow.GetValue<WwiseEvent>(wwiseEvent).Post(gameObj, callbackType.value, Callback);
        return output;
    }
    
    private void Callback(object in_cookie, AkCallbackType in_type, AkCallbackInfo in_info)
    {
        Flow.New(graphReference).Invoke(onCallback);
    }
    
}
