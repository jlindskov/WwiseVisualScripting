
using Unity.VisualScripting;
using UnityEngine;

[UnitTitle("Unload Soundbank")]
[UnitSurtitle("Wwise Soundbank")]
[UnitShortTitle("Unload Soundbank")]
[UnitCategory("Wwise/SoundBank")]
[TypeIcon(typeof(AudioSource))]
public class UnloadSoundbankUnit : Unit
{
    [DoNotSerialize,PortLabelHidden]
    public ControlInput input;
    
    [DoNotSerialize,PortLabelHidden]
    public ControlOutput output;
    
    [DoNotSerialize, PortLabel("Soundbank")]
    public ValueInput wwiseBank; 
    
    
    protected override void Definition()
    {
        input = ControlInput("in", Enter);
        output = ControlOutput("out");
        
        wwiseBank = ValueInput<WwiseSoundbank>("soundbank");
        Requirement(wwiseBank, input);
        Succession(input, output);
    }

    private ControlOutput Enter(Flow flow)
    {
        flow.GetValue<WwiseSoundbank>(wwiseBank).Unload();
        return output;
    }
}
