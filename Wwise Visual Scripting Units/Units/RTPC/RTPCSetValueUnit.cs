using Unity.VisualScripting;
using UnityEngine;

[UnitTitle("Set RTPC Value")]
[UnitSurtitle("Wwise RTPC")]
[UnitShortTitle("Set Value")]
[UnitCategory("Wwise/RTPC")]
[TypeIcon(typeof(AudioSource))]
public class RTPCSetValueUnit : Unit
{
    [DoNotSerialize,PortLabelHidden]
    public ControlInput input;
    
    [DoNotSerialize,PortLabelHidden]
    public ControlOutput output;
    
    [DoNotSerialize, PortLabel("RTPC")] 
    public ValueInput rtpc; 
    
    [DoNotSerialize] 
    public ValueInput gameObject; 
    
    [DoNotSerialize, PortLabel("Value")] 
    public ValueInput value; 
    
    protected override void Definition()
    {
        input = ControlInput("in", Enter);
        output = ControlOutput("out");

        rtpc = ValueInput<WwiseRTPC>("RTPC");
        gameObject = ValueInput<GameObject>("gameObject", null).NullMeansSelf();
        value = ValueInput<float>("value", 0);
     
        Requirement(rtpc, input);
        Succession(input, output);
    }

    private ControlOutput Enter(Flow flow)
    {
        var gameObj = flow.GetValue<GameObject>(gameObject); 
        var floatValue = flow.GetValue<float>(gameObject); 
       flow.GetValue<WwiseRTPC>(rtpc).SetValue(gameObj,floatValue);
        return output;
    }
}
