
using Unity.VisualScripting;
using UnityEngine;

[UnitTitle("Post Trigger")]
[UnitSurtitle("Wwise Trigger")]
[UnitShortTitle("Post Trigger")]
[UnitCategory("Wwise")]
[TypeIcon(typeof(AudioSource))]
public class PostTriggerUnit : Unit
{
    [DoNotSerialize,PortLabelHidden]
    public ControlInput input;
    
    [DoNotSerialize,PortLabelHidden]
    public ControlOutput output;
    
    [DoNotSerialize, PortLabel("Trigger")]
    public ValueInput wwiseSwitch; 
    
    [DoNotSerialize]
    public ValueInput gameObject; 
    
    protected override void Definition()
    {
        input = ControlInput("in", Enter);
        output = ControlOutput("out");
        wwiseSwitch = ValueInput<WwiseTrigger>("trigger");
        gameObject = ValueInput<GameObject>("gameObject", null).NullMeansSelf();
        Requirement(wwiseSwitch, input);
        Succession(input, output);
    }

    private ControlOutput Enter(Flow flow)
    {
        var gameObj = flow.GetValue<GameObject>(gameObject); 
         flow.GetValue<WwiseTrigger>(wwiseSwitch).Post(gameObj);
        return output;
    }
}
