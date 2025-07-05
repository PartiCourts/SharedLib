namespace PartiCourts.SharedLib.Models
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Model used to turn a json file into a geojson.
    /// </summary>
    public class GeoJson
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GeoJson"/> class.
        /// </summary>
        /// <param name="type">The type of this geojson.</param>
        /// <param name="name">The name of this geojson.</param>
        /// <param name="crs">The Coordinate Reference System of this geojson.</param>
        /// <param name="features">The features in this geojson.</param>
        public GeoJson(string type, string name, JObject crs, List<Feature> features)
        {
            this.Type = type;
            this.Name = name;
            this.Crs = crs;
            this.Features = features;
        }

        /// <summary>
        /// Gets or Sets the type of this geojson.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or Sets the name of this geojson.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets the Coordinate Reference System of this geojson.
        /// </summary>
        [JsonProperty("crs")]
        public JObject Crs { get; set; }

        /// <summary>
        /// Gets or Sets the features in this geojson.
        /// </summary>
        [JsonProperty("features")]
        public List<Feature> Features { get; set; }
    }
}
