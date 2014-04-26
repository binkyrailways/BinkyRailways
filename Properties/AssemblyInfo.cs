using System.Reflection;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Binky Railways")]
[assembly: AssemblyCompany("binkyrailways.net")]
[assembly: AssemblyProduct("Binky Railways")]
[assembly: AssemblyCopyright("Copyright © binkyrailways.net 2011")]

// Obfuscate as private assembly
#if !MODEL
[assembly: ObfuscateAssembly(true)]
#endif
[assembly: Obfuscation(Feature="rename /charset:ascii-lower")]