using Unity.VisualScripting;
using UnityEngine;

[UnitTitle("Set RTPC Global Value")]
[UnitSurtitle("Wwise RTPC")]
[UnitShortTitle("Set Global Value")]
[UnitCategory("Wwise/RTPC")]
[TypeIcon(typeof(AudioSource))]
public class RTPCSetGlobalValueUnit : Unit
{
    [DoNotSerialize,PortLabelHidden]
    public ControlInput input;
    
    [DoNotSerialize,PortLabelHidden]
    public ControlOutput output;
    
    [DoNotSerialize, PortLabel("RTPC")] 
    public ValueInput rtpc; 
    
    [DoNotSerialize, PortLabel("Value")] 
    public ValueInput value; 
    
    protected override void Definition()
    {
        input = ControlInput("in", Enter);
        output = ControlOutput("out");

        rtpc = ValueInput<WwiseRTPC>("RTPC");
        value = ValueInput<float>("value", 0);
     
        Requirement(rtpc, input);
        Succession(input, output);
    }

    private ControlOutput Enter(Flow flow)
    { 
        var floatValue = flow.GetValue<float>(value); 
        flow.GetValue<WwiseRTPC>(rtpc).SetGlobalValue(floatValue);
        return output;
    }
}
