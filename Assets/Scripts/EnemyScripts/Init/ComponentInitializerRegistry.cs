using System;
using System.Collections.Generic;

public class ComponentInitializerRegistry
{
    private readonly Dictionary<Type, List<IComponentInitializer>> _initializersMap = new Dictionary<Type, List<IComponentInitializer>>();

    public void RegisterInitializer<TSettings>(IComponentInitializer initializer) where TSettings : IEnemySettings
    {
        Type settingsType = typeof(TSettings);

        if (!_initializersMap.ContainsKey(settingsType))
        {
            _initializersMap[settingsType] = new List<IComponentInitializer>();
        }

        _initializersMap[settingsType].Add(initializer);
    }

    public List<IComponentInitializer> GetInitializersForSettings(IEnemySettings settings)
    {
        Type settingsType = settings.GetType();

        if (_initializersMap.ContainsKey(settingsType))
        {
            return _initializersMap[settingsType];
        }

        return new List<IComponentInitializer>();
    }
}
