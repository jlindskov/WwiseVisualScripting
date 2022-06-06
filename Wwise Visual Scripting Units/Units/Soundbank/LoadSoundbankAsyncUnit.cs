
using Unity.VisualScripting;
using UnityEngine;

[UnitTitle("Load Soundbank Async")]
[UnitSurtitle("Wwise Soundbank")]
[UnitShortTitle("Load Soundbank Async")]
[UnitCategory("Wwise/SoundBank")]
[TypeIcon(typeof(AudioSource))]
public class LoadSoundbankAsyncUnit : Unit
{
    [DoNotSerialize,PortLabelHidden]
    public ControlInput input;
    
    [DoNotSerialize,PortLabelHidden]
    public ControlOutput output;
    
    [DoNotSerialize]
    public ControlOutput onBankLoaded;
    
    [DoNotSerialize, PortLabel("Soundbank")]
    public ValueInput wwiseBank;

    private GraphReference graphReference { get; set; }
    
    protected override void Definition()
    {
        input = ControlInput("in", Enter);
        output = ControlOutput("out");
        onBankLoaded = ControlOutput("onBankLoaded");
        
        wwiseBank = ValueInput<WwiseSoundbank>("soundbank");
        Requirement(wwiseBank, input);
        Succession(input, output);
        Succession(input, onBankLoaded);
    }

    private ControlOutput Enter(Flow flow)
    {
        flow.GetValue<WwiseSoundbank>(wwiseBank).LoadAsync(Callback);
        return output;
    }
    
    private void Callback(uint in_bankID, System.IntPtr in_InMemoryBankPtr, AKRESULT in_eLoadResult, object in_Cookie)
    {
        Flow.New(graphReference).Invoke(onBankLoaded);
    }
}
