
using Unity.VisualScripting;
using UnityEngine;

[UnitTitle("Set State")]
[UnitSurtitle("Wwise State")]
[UnitShortTitle("Set State")]
[UnitCategory("Wwise")]
[TypeIcon(typeof(AudioSource))]
public class SetStateUnit : Unit
{
    [DoNotSerialize,PortLabelHidden]
    public ControlInput input;
    
    [DoNotSerialize,PortLabelHidden]
    public ControlOutput output;
    
    [DoNotSerialize, PortLabel("State")]
    public ValueInput wwiseState; 
    
    
    protected override void Definition()
    {
        input = ControlInput("in", Enter);
        output = ControlOutput("out");
        
        wwiseState = ValueInput<WwiseState>("state");
        Requirement(wwiseState, input);
        Succession(input, output);
    }

    private ControlOutput Enter(Flow flow)
    {
        flow.GetValue<WwiseState>(wwiseState).SetValue();
        return output;
    }
}
