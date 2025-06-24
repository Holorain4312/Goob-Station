using Content.Shared.Nutrition.Components;
using Content.Server.Temperature.Systems;
using Content.Server.Temperature.Components;
using Robust.Shared.GameObjects;
using Robust.Shared.IoC;
using Robust.Shared.Timing;

namespace Content.Server.Temperature
{
    public sealed class DehydrationTemperatureSystem : EntitySystem
    {
        [Dependency] private readonly TemperatureSystem _temperatureSystem = default!;

        private const float BaseTemperatureIncreasePerSecond = 5.0f; // Kelvin/sec

        private const float DehydrationThresholdForRapidRise = 150.0f; // the value at which you start rapidly overheating from thirst
        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(float frameTime)
        {
            base.Update(frameTime);

            var query = EntityQueryEnumerator<ThirstComponent, TemperatureComponent, DehydrationTemperatureAffectedComponent>();
            while (query.MoveNext(out var uid, out var thirstComp, out var tempComp, out var affectedComp))
            {
                if (thirstComp.CurrentThirst <= DehydrationThresholdForRapidRise)
                {
                    var temperatureIncrease = BaseTemperatureIncreasePerSecond * frameTime;
                    var newTemperature = tempComp.CurrentTemperature + temperatureIncrease;

                    _temperatureSystem.ChangeHeat(uid, newTemperature, true);
                }
            }
        }
    }
}

