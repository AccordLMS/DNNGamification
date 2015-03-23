namespace DNNGamification.Json
{
    using System;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Camel case property names contract resolver.
    /// </summary>
    public class CamelCaseResolver : CamelCasePropertyNamesContractResolver
    {
        #region Defines

        private const string SFID_ABBREVIATION = "SFID";

        #endregion

        #region Protected Methods

        /// <summary>
        /// Resolves the name of the property.
        /// </summary>
        protected override string ResolvePropertyName(string propertyName)
        {
            return base.ResolvePropertyName(propertyName);
        }

        #endregion
    }

    /// <summary>
    /// JSON date time convertor.
    /// </summary>
    public class JsonDateTimeConverter : DateTimeConverterBase
    {
        #region Defines

        public const string DATE_TIME_FORMAT = "dd.MM.yyyy HH:mm";

        #endregion

        #region Public Methods

        /// <summary>
        /// Read JSON handler.
        /// </summary>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return DateTime.Parse(reader.Value.ToString());
        }

        /// <summary>
        /// Write JSON handler.
        /// </summary>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((DateTime)value).ToString(DATE_TIME_FORMAT));
        }

        #endregion
    }

    /// <summary>
    /// Json converter.
    /// </summary>
    public static class JsonConverter
    {
        #region Private Properties

        private static JsonSerializerSettings SerializerSettings { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Serializes the specified data.
        /// </summary>
        public static string Serialize(object data)
        {
            return JsonConvert.SerializeObject(data, Formatting.None, SerializerSettings);
        }

        /// <summary>
        /// Deserializes the specified json.
        /// </summary>
        public static object Deserialize(string json)
        {
            return JsonConvert.DeserializeObject(json, SerializerSettings);
        }

        /// <summary>
        /// Deserializes the specified json.
        /// </summary>
        public static object Deserialize(string json, Type type)
        {
            return JsonConvert.DeserializeObject(json, type, SerializerSettings);
        }

        /// <summary>
        /// Deserializes the specified json.
        /// </summary>
        public static T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, SerializerSettings);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        static JsonConverter()
        {
            SerializerSettings = new JsonSerializerSettings // define serialization settings
            {
                ContractResolver = new CamelCaseResolver(), ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            SerializerSettings.Converters.Add(new IsoDateTimeConverter());
        }

        #endregion
    }
}