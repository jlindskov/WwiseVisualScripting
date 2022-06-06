using System;
using Unity.VisualScripting;
using UnityEngine;


[UnitSurtitle("Wwise Event")]
[UnitShortTitle("Post And Wait For End")]
[UnitCategory("Wwise/Event")]
[TypeIcon(typeof(AudioSource))]
public class PostEventAndWaitForEndUnit : Unit
{
      [DoNotSerialize,PortLabelHidden]
    public ControlInput input;
    
    [DoNotSerialize]
    public ControlOutput output;
    
    [DoNotSerialize, PortLabel("Event")]
    public ValueInput wwiseEvent; 
    
    [DoNotSerialize] 
    public ValueInput gameObject;

    [DoNotSerialize]
    public ValueOutput id;
    
    private uint eventId;
    private GraphReference graphReference { get; set; }
    
    protected override void Definition()
    {
        input = ControlInput("In", Enter);
        output = ControlOutput("OnEventEnd");
       
        wwiseEvent = ValueInput<WwiseEvent>("WwiseEvent",new WwiseEvent());
        gameObject = ValueInput<GameObject>("GameObject", null).NullMeansSelf();
        
        id = ValueOutput<uint>("eventID", (flow) => eventId);
        
        Requirement(wwiseEvent, input);
        Succession(input, output);
    }

    private ControlOutput Enter(Flow flow)
    {
        var gameObj = flow.GetValue<GameObject>(gameObject);
        graphReference = flow.stack.AsReference();
        eventId = flow.GetValue<WwiseEvent>(wwiseEvent).Post(gameObj, (uint)AkCallbackType.AK_EndOfEvent, Callback);
        return null;
    }
    
    private void Callback(object in_cookie, AkCallbackType in_type, AkCallbackInfo in_info)
    {
        Flow.New(graphReference).Invoke(output);
    }
    
}
