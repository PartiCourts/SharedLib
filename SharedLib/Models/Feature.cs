namespace PartiCourts.SharedLib.Models
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Model used to parse features in geojson files.
    /// </summary>
    public class Feature
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Feature"/> class.
        /// </summary>
        /// <param name="type">The type of this feature.</param>
        /// <param name="properties">The properties of this feature.</param>
        /// <param name="geometry">The geometry information of this feature.</param>
        public Feature(string type, Dictionary<string, object> properties, JObject geometry)
        {
            this.Type = type;
            this.Properties = properties;
            this.Geometry = geometry;
        }

        /// <summary>
        /// Gets or Sets the type of this feature.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or Sets the properties of this feature.
        /// </summary>
        [JsonProperty("properties")]
        public Dictionary<string, object> Properties { get; set; }

        /// <summary>
        /// Gets or Sets the geometry information of this feature.
        /// </summary>
        [JsonProperty("geometry")]
        public JObject Geometry { get; set; }
    }
}
