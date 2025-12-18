using Harmony;
using MelonLoader.Utils;
using NEP.Paranoia.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NEP.Paranoia.Data
{
    public static class DataReader
    {
        public static IReadOnlyDictionary<string, string> EntityFiles => m_entityFiles;
        public static IReadOnlyDictionary<string, EntityDefinition> EntityDefinitions => m_entityDefinitions;
        public static IReadOnlyDictionary<string, EventDefinition> EventDefinitions => m_eventDefinitions;

        private static Dictionary<string, EntityDefinition> m_entityDefinitions;
        private static Dictionary<string, EventDefinition> m_eventDefinitions;

        private static Dictionary<string, string> m_entityFiles;

        internal static void Initialize()
        {
            m_entityDefinitions = new Dictionary<string, EntityDefinition>();
            m_eventDefinitions = new Dictionary<string, EventDefinition>();

            m_entityFiles = new Dictionary<string, string>();

            LoadEventData();
            LoadEntityData();
        }

        public static void LoadEventData()
        {
            string modFolder = Path.Combine(MelonEnvironment.UserDataDirectory, "Not Enough Photons/paranoia");
            string eventFolder = Path.Combine(modFolder, "data/event");

            IEnumerable<string> files = Directory.EnumerateFiles(eventFolder);

            foreach (var file in files)
            {
                if (!file.EndsWith(".json"))
                    continue;

                try
                {
                    var definition = JsonConvert.DeserializeObject<EventDefinition>(File.ReadAllText(file));
                    m_eventDefinitions.Add(definition.Event, definition);
                }
                catch (Exception ex)
                {
                    Paranoia.Logger.Error($"Exception caught at file {file} - {ex.Message}");
                }
            }
        }

        public static void LoadEntityData()
        {
            string modFolder = Path.Combine(MelonEnvironment.UserDataDirectory, "Not Enough Photons/paranoia");
            string entityFolder = Path.Combine(modFolder, "data/entity");

            IEnumerable<string> files = Directory.EnumerateFiles(entityFolder);

            foreach (var file in files)
            {
                if (!file.EndsWith(".json"))
                    continue;

                try
                {
                    var definition = JsonConvert.DeserializeObject<EntityDefinition>(File.ReadAllText(file));
                    m_entityDefinitions.Add(definition.Entity, definition);
                    m_entityFiles.Add(definition.Entity, file);
                }
                catch (Exception ex)
                {
                    Paranoia.Logger.Error($"Exception caught at file {file} - {ex.Message}");
                }
            }
        }
    }
}
