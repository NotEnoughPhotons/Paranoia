using System.Reflection;
using MelonLoader;

[assembly: MelonInfo(
    typeof(NEP.Paranoia.Paranoia), 
    NEP.Paranoia.BuildInfo.Name, 
    NEP.Paranoia.BuildInfo.Version, 
    NEP.Paranoia.BuildInfo.Author)]
[assembly: MelonColor(255, 125, 25, 25)]

// Create and Setup a MelonGame Attribute to mark a Melon as Universal or Compatible with specific Games.
// If no MelonGame Attribute is found or any of the Values for any MelonGame Attribute on the Melon is null or empty it will be assumed the Melon is Universal.
// Values for MelonGame Attribute can be found in the Game's app.info file or printed at the top of every log directly beneath the Unity version.
[assembly: MelonGame("Stress Level Zero", "BONELAB")]