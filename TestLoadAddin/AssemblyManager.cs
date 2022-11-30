using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;


internal class AssemblyManager
{
    private AppDomain _currentDomain;
    private string _rootDirectory;
    private readonly List<string> _assemblyNameRedirects = new()
    {
        "MyLibrary",
    };

    private readonly Dictionary<string, Assembly> _resolvedAssemblies = new();

    /// <summary>
    /// Constructs a new <see cref="AssemblyManager"/>.
    /// </summary>
    public AssemblyManager()
    {
        _rootDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        _currentDomain = AppDomain.CurrentDomain;
        _currentDomain.AssemblyResolve += OnAssemblyResolve;

    }

    /// <summary>
    /// The event handler which fires when an unresolved assembly event occurs in the
    /// app domain. This is the main method used for resolving assemblies which are
    /// shipped with the MFE software but conflict with older versions shipped with
    /// Revit.
    /// </summary>
    private Assembly OnAssemblyResolve(object sender, ResolveEventArgs args)
    {
        var assemblyFullName = args.Name;

        var matchingAssemblyName = _assemblyNameRedirects.FirstOrDefault(assemblyFullName.Contains);
        if (matchingAssemblyName != null)
        {
            if (_resolvedAssemblies.ContainsKey(matchingAssemblyName))
                return _resolvedAssemblies[matchingAssemblyName];

            var assemblyPath = Path.Combine(_rootDirectory, $"{matchingAssemblyName}.dll");
            var assemblyName = AssemblyName.GetAssemblyName(assemblyPath);
            var assembly = Assembly.Load(assemblyName);

            _resolvedAssemblies[matchingAssemblyName] = assembly;

            return assembly;
        }

        // Must return null so the fallback assembly resolution occurs.
        return null;
    }

    /// <summary>
    /// Shuts down this service.
    /// </summary>
    public void ShutDown()
    {
        _currentDomain.AssemblyResolve -= OnAssemblyResolve;
    }
}
