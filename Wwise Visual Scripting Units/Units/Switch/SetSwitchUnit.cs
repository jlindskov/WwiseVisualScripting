
using Unity.VisualScripting;
using UnityEngine;

[UnitTitle("Set Switch")]
[UnitSurtitle("Wwise Switch")]
[UnitShortTitle("Set Switch")]
[UnitCategory("Wwise")]
[TypeIcon(typeof(AudioSource))]
public class SetSwitchUnit : Unit
{
    [DoNotSerialize,PortLabelHidden]
    public ControlInput input;
    
    [DoNotSerialize,PortLabelHidden]
    public ControlOutput output;
    
    [DoNotSerialize, PortLabel("Switch")]
    public ValueInput wwiseSwitch; 
    
    [DoNotSerialize]
    public ValueInput gameObject; 
    
    protected override void Definition()
    {
        input = ControlInput("in", Enter);
        output = ControlOutput("out");
        
        wwiseSwitch = ValueInput<WwiseSwitch>("switch");
        gameObject = ValueInput<GameObject>("gameObject", null).NullMeansSelf();
        Requirement(wwiseSwitch, input);
        Succession(input, output);
    }

    private ControlOutput Enter(Flow flow)
    {
        var gameObj = flow.GetValue<GameObject>(gameObject); 
         flow.GetValue<WwiseSwitch>(wwiseSwitch).SetValue(gameObj);
        return output;
    }
}
