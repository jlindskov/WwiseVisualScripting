
using System;
using Unity.VisualScripting;
using UnityEngine;

[UnitTitle("Post Event")]
[UnitSurtitle("Wwise Event")]
[UnitShortTitle("Post")]
[UnitCategory("Wwise/Event")]
[TypeIcon(typeof(AudioSource))]
public class PostEventUnit : Unit, IGraphEventListener, IGraphElementWithData
{
    [DoNotSerialize,PortLabelHidden]
    public ControlInput input;
    
    [DoNotSerialize,PortLabelHidden]
    public ControlOutput output;
    
    [DoNotSerialize, PortLabel("Event")]
    public ValueInput wwiseEvent; 
    
    [DoNotSerialize]
    public ValueInput gameObject; 
    
    [DoNotSerialize]
    public ValueInput stopOnDisable; 
    
    [DoNotSerialize]
    public ValueOutput id;
    
    private uint eventId;
    
    protected override void Definition()
    {
        input = ControlInput("in", Enter);
        output = ControlOutput("out");
        
        wwiseEvent = ValueInput<WwiseEvent>("event");
        gameObject = ValueInput<GameObject>("gameObject", null).NullMeansSelf();
        stopOnDisable = ValueInput<bool>("stopOnDisable");
        
        id = ValueOutput<uint>("eventID", (flow) => eventId);
        
        Requirement(wwiseEvent, input);
        Succession(input, output);
    }

    private ControlOutput Enter(Flow flow)
    {
        eventId = flow.GetValue<WwiseEvent>(wwiseEvent).Post(flow.GetValue<GameObject>(gameObject));
        return output;
    }
    
    
    public class Data : IGraphElementData
    {
        public Delegate handler;
        public bool isListening;
    }

    public void StartListening(GraphStack stack)
    {
        var data = stack.GetElementData<Data>(this);
        
        if (data.isListening)
        {
            return;
        }

        var reference = stack.ToReference();
        var hook = new EventHook(EventHooks.OnDisable, stack.machine);
        Action<EmptyEventArgs> handler = args => StopSound(reference);
        EventBus.Register(hook, handler);
        data.handler = handler;
        data.isListening = true;
    }

    private void StopSound(GraphReference reference)
    {
        using (var flow = Flow.New(reference))
        {

            if (flow.GetValue<bool>(stopOnDisable))
            {
                flow.GetValue<WwiseEvent>(wwiseEvent).ExecuteAction(flow.GetValue<GameObject>(gameObject),
                    AkActionOnEventType.AkActionOnEventType_Stop,0,AkCurveInterpolation.AkCurveInterpolation_Linear);
            }
        }
    }

    public void StopListening(GraphStack stack)
    {
        var data = stack.GetElementData<Data>(this);
        
        if (!data.isListening)
        {
            return;
        }

        var hook = new EventHook(EventHooks.OnDisable, stack.machine);
        EventBus.Unregister(hook, data.handler);
        data.handler = null;
        data.isListening = false;
    }

    public bool IsListening(GraphPointer pointer)
    {
        return pointer.GetElementData<Data>(this).isListening;
    }

    public IGraphElementData CreateData()
    {
        return new Data();
    }
}
