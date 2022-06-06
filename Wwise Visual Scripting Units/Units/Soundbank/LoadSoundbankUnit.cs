
using Unity.VisualScripting;
using UnityEngine;

[UnitTitle("Load Soundbank")]
[UnitSurtitle("Wwise Soundbank")]
[UnitShortTitle("Load Soundbank")]
[UnitCategory("Wwise/SoundBank")]
[TypeIcon(typeof(AudioSource))]
public class LoadSoundbankUnit : Unit
{
    [DoNotSerialize,PortLabelHidden]
    public ControlInput input;
    
    [DoNotSerialize,PortLabelHidden]
    public ControlOutput output;
    
    [DoNotSerialize, PortLabel("Soundbank")]
    public ValueInput wwiseBank; 
    
    [DoNotSerialize, PortLabel("Decode Bank")]
    public ValueInput decodeBank; 
    
    [DoNotSerialize, PortLabel("Save Decoded Bank")]
    public ValueInput saveDecodedBank; 
    
    
    protected override void Definition()
    {
        input = ControlInput("in", Enter);
        output = ControlOutput("out");
        
        wwiseBank = ValueInput<WwiseSoundbank>("soundbank");
        wwiseBank = ValueInput<bool>("decodeBank");
        wwiseBank = ValueInput<bool>("saveDecodedBank");
        Requirement(wwiseBank, input);
        Succession(input, output);
    }

    private ControlOutput Enter(Flow flow)
    {
        bool decode = flow.GetValue<bool>(decodeBank); 
        bool save = flow.GetValue<bool>(saveDecodedBank); 
        flow.GetValue<WwiseSoundbank>(wwiseBank).Load(decode,save);
        return output;
    }
}
