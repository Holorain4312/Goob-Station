using Content.Shared.Actions;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom.Prototype;
using Robust.Shared.GameStates;

namespace Content.Goobstation.Shared.Darkswap;

[RegisterComponent]
public sealed partial class DarkswapComponent : Component
{
    [DataField("combatToggleAction", customTypeSerializer: typeof(PrototypeIdSerializer<EntityPrototype>))]
    public string ActionProto = "DarkswapAction";

    [DataField]
    public EntityUid? ActionUid;


}

public sealed partial class DarkswapEvent : InstantActionEvent;
