using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CricApp.Models
{
    public partial class PlayerStatsViewModel
    {
        [JsonProperty("pid")]
        public long Pid { get; set; }

        [JsonProperty("profile")]
        public string Profile { get; set; }

        [JsonProperty("imageURL")]
        public Uri ImageUrl { get; set; }

        [JsonProperty("battingStyle")]
        public string BattingStyle { get; set; }

        [JsonProperty("bowlingStyle")]
        public string BowlingStyle { get; set; }

        [JsonProperty("majorTeams")]
        public string MajorTeams { get; set; }

        [JsonProperty("currentAge")]
        public string CurrentAge { get; set; }

        [JsonProperty("born")]
        public string Born { get; set; }

        [JsonProperty("fullName")]
        public string FullName { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("playingRole")]
        public string PlayingRole { get; set; }

        [JsonProperty("v")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long V { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; }

        [JsonProperty("ttl")]
        public long Ttl { get; set; }

        [JsonProperty("provider")]
        public Provider Provider { get; set; }

        [JsonProperty("creditsLeft")]
        public long CreditsLeft { get; set; }
    }

    public partial class Data
    {
        [JsonProperty("bowling")]
        public Bowling Bowling { get; set; }

        [JsonProperty("batting")]
        public Batting Batting { get; set; }
    }

    public partial class Batting
    {
        [JsonProperty("listA")]
        public Stats ListA { get; set; }

        [JsonProperty("firstClass")]
        public Stats FirstClass { get; set; }

        [JsonProperty("T20Is")]
        public Stats T20Is { get; set; }

        [JsonProperty("ODIs")]
        public Stats OdIs { get; set; }

        [JsonProperty("tests")]
        public Stats Tests { get; set; }
    }

    public partial class Stats
    {
        [JsonProperty("50")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Fifties { get; set; }

        [JsonProperty("100")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Hundreds { get; set; }

        [JsonProperty("St")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long St { get; set; }

        [JsonProperty("Ct")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Ct { get; set; }

        [JsonProperty("6s")]
        public string Sixes { get; set; }

        [JsonProperty("4s")]
        public string Fours { get; set; }

        [JsonProperty("SR")]
        public string Sr { get; set; }

        [JsonProperty("BF")]
        public string Bf { get; set; }

        [JsonProperty("Ave")]
        public string Ave { get; set; }

        [JsonProperty("HS")]
        public string Hs { get; set; }

        [JsonProperty("Runs")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Runs { get; set; }

        [JsonProperty("NO")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long No { get; set; }

        [JsonProperty("Inns")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Inns { get; set; }

        [JsonProperty("Mat")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Mat { get; set; }
    }

    public partial class Bowling
    {
        [JsonProperty("listA")]
        public Dictionary<string, string> ListA { get; set; }

        [JsonProperty("firstClass")]
        public Dictionary<string, string> FirstClass { get; set; }

        [JsonProperty("T20Is")]
        public Dictionary<string, string> T20Is { get; set; }

        [JsonProperty("ODIs")]
        public Dictionary<string, string> OdIs { get; set; }

        [JsonProperty("tests")]
        public Dictionary<string, string> Tests { get; set; }
    }

    public partial class Provider
    {
        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("pubDate")]
        public DateTimeOffset PubDate { get; set; }
    }

    public partial class PlayerStats
    {
        public static PlayerStats FromJson(string json) => JsonConvert.DeserializeObject<PlayerStats>(json, CricApp.Models.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this PlayerStats self) => JsonConvert.SerializeObject(self, CricApp.Models.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}
