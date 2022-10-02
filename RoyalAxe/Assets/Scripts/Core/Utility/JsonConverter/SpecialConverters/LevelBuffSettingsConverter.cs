namespace Core.SpecialConverters
{
    public class LevelBuffSettingsConverter
    {
        
    }
    
      /*/// <summary>
    ///     Специальный конвертер для поведения персонажей. не надо каждую команду квеста переконвентировать дважды.
    /// </summary>
    public class CharactersBehaviourTaskModelJsonConverter : JsonConverter
    {
        private IMetaQuestCommandTypeAdapter _typeAdapter;

        public CharactersBehaviourTaskModelJsonConverter(IMetaQuestCommandTypeAdapter adapter)
        {
            _typeAdapter = adapter;
        }

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
                                        JsonSerializer serializer)
        {
            var jQ = serializer.Deserialize(reader) as JObject;
            if (jQ == null) return null;

            CharactersBehaviourTaskModel result = new CharactersBehaviourTaskModel
            {
                ScenarioName = serializer.Deserialize<string>(jQ["ScenarioName"].CreateReader()),
                Characters   = serializer.Deserialize<List<CharacterType>>(jQ["Characters"].CreateReader()),
                ConditionsModel = serializer.Deserialize<QuestBehaviourCommandCondition>(jQ["ConditionsModel"].CreateReader()),
            };
            List<object> deserializeCommands = new List<object>();

            foreach (var command in jQ["CharacterCommands"])
            {
                var                      rss             = command["CommandType"];
                BehaviourTaskCommandType commandTypetype = (BehaviourTaskCommandType) rss.Value<int>();
                var                      type            = _typeAdapter.GetModelType(commandTypetype);
                if (type == null) continue;

                CharacterTaskCommandModel commandModel =
                    serializer.Deserialize(command.CreateReader(), type) as CharacterTaskCommandModel;

                deserializeCommands.Add(commandModel);
            }

            result.CharacterCommands = deserializeCommands;
            return result;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(CharactersBehaviourTaskModel);
        }
    }*/
}