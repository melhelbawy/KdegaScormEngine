﻿using Kdega.ScormEngine.Application.Attributes;
using Kdega.ScormEngine.Application.Enums;
using Kdega.ScormEngine.Application.Extensions;
using Kdega.ScormEngine.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Kdega.ScormEngine.Application.ScormModelKeyMediator;
public class ScormMediator : IScormMediator
{
    private readonly IDictionary<ScormApiMethod, Dictionary<string, List<object>>> _components;
    private readonly IMediator _mediator;
    private readonly IServiceProvider _serviceProvider;

    public ScormMediator(IServiceProvider serviceProvider)
    {
        _components = new Dictionary<ScormApiMethod, Dictionary<string, List<object>>>();
        _serviceProvider = serviceProvider;
        _mediator = serviceProvider.GetCustomRequiredService<IMediator>();

        Register().Wait();
    }

    public async Task Handle<T>(string keyName, T request, ScormApiMethod apiMethod)
    {
        var keyCommands = _components[apiMethod];
        var commandObjects = keyCommands.Where(x => x.Key == keyName)
            .SelectMany(x => x.Value).ToList();
        foreach (var keyCommand in commandObjects)
        {
            SetLmsRequestProperty(keyCommand, request);
            await _mediator.Send(keyCommand);
        }
    }

    public Task Register()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var componentTypes = assembly.GetTypes()
            .Where(t => t.GetCustomAttributes(typeof(ScormMediatorComponentAttribute), false).Any());
        return Task.Run(() =>
        {
            foreach (var componentType in componentTypes)
            {
                var attribute = (ScormMediatorComponentAttribute)componentType
                    .GetCustomAttributes(typeof(ScormMediatorComponentAttribute), false).First();
                var componentName = attribute.ComponentName;

                if (ActivatorUtilities.CreateInstance(_serviceProvider, componentType) is { } component)
                    RegisterComponent(component, componentName, attribute.ApiMethod);
            }
        });
    }

    public void RegisterComponent(object component, string componentName, ScormApiMethod apiMethod)
    {
        if (_components.ContainsKey(apiMethod))
        {
            if (_components[apiMethod].ContainsKey(componentName))
            {
                _components[apiMethod][componentName].Add(component);
            }
            else
            {
                _components[apiMethod].Add(componentName, new List<object>() { component });
            }
        }
        else
            _components.Add(apiMethod, new Dictionary<string, List<object>>() { { componentName, new List<object>() { component } } });
    }

    private static void SetLmsRequestProperty<T>(object keyCommand, T request)
    {
        var props = keyCommand.GetType().GetProperties();
        foreach (var property in keyCommand.GetType().GetProperties())
        {
            if (property.PropertyType == typeof(T))
            {
                property.SetValue(keyCommand, request, null);
            }
        }
    }
}
