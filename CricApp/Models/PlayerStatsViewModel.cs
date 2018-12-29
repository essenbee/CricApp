using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Display(Name = "Batting Style")]
        public string BattingStyle { get; set; }

        [JsonProperty("bowlingStyle")]
        [Display(Name = "Bowling Style")]
        public string BowlingStyle { get; set; }

        [JsonProperty("majorTeams")]
        [Display(Name = "Major Teams")]
        public string MajorTeams { get; set; }

        [JsonProperty("currentAge")]
        [Display(Name = "Current Age")]
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
        [Display(Name = "Playing Role")]
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
        [Display(Name = "List A")]
        public BattingStats ListA { get; set; }

        [JsonProperty("firstClass")]
        [Display(Name = "1st Class")]
        public BattingStats FirstClass { get; set; }

        [JsonProperty("T20Is")]
        public BattingStats T20Is { get; set; }

        [JsonProperty("ODIs")]
        public BattingStats OdIs { get; set; }

        [JsonProperty("tests")]
        public BattingStats Tests { get; set; }
    }

    public partial class BattingStats
    {
        [JsonProperty("50")]
        [Display(Name = "50s")]
        public string Fifties { get; set; }

        [JsonProperty("100")]
        [Display(Name = "100s")]
        public string Hundreds { get; set; }

        [JsonProperty("St")]
        [Display(Name = "ST")]
        public string St { get; set; }

        [JsonProperty("Ct")]
        [Display(Name = "CT")]
        public string Ct { get; set; }

        [JsonProperty("6s")]
        public string Sixes { get; set; }

        [JsonProperty("4s")]
        public string Fours { get; set; }

        [JsonProperty("SR")]
        [Display(Name = "SR")]
        public string Sr { get; set; }

        [JsonProperty("BF")]
        [Display(Name = "BF")]
        public string Bf { get; set; }

        [JsonProperty("Ave")]
        public string Ave { get; set; }

        [JsonProperty("HS")]
        [Display(Name = "HS")]
        public string Hs { get; set; }

        [JsonProperty("Runs")]
        public string Runs { get; set; }

        [JsonProperty("NO")]
        [Display(Name = "NO")]
        public string No { get; set; }

        [JsonProperty("Inns")]
        public string Inns { get; set; }

        [JsonProperty("Mat")]
        [Display(Name = "Matches")]
        public string Mat { get; set; }
    }

    public partial class BowlingStats
    {
        [JsonProperty("10")]
        [Display(Name = "10 Wkts")]
        public string TenWkts { get; set; }

        [JsonProperty("5w")]
        [Display(Name = "5 Wkts")]
        public string FiveWkts { get; set; }

        [JsonProperty("4w")]
        [Display(Name = "4 Wkts")]
        public string FourWkts { get; set; }

        [JsonProperty("Econ")]
        public string Econ { get; set; }

        [JsonProperty("Ave")]
        public string Ave { get; set; }

        [JsonProperty("BBM")]
        [Display(Name = "BBM")]
        public string Bbm { get; set; }

        [JsonProperty("BBI")]
        [Display(Name = "BBI")]
        public string Bbi { get; set; }

        [JsonProperty("Wkts")]
        public string Wkts { get; set; }

        [JsonProperty("Runs")]
        public string Runs { get; set; }

        [JsonProperty("Balls")]
        public string Balls { get; set; }

        [JsonProperty("SR")]
        [Display(Name = "SR")]
        public string Sr { get; set; }

        [JsonProperty("Inns")]
        public string Inns { get; set; }

        [JsonProperty("Mat")]
        [Display(Name = "Matches")]
        public string Mat { get; set; }
    }

    public partial class Bowling
    {
        [JsonProperty("listA")]
        [Display(Name = "List A")]
        public BowlingStats ListA { get; set; }

        [JsonProperty("firstClass")]
        [Display(Name = "1st Class")]
        public BowlingStats FirstClass { get; set; }

        [JsonProperty("T20Is")]
        public BowlingStats T20Is { get; set; }

        [JsonProperty("ODIs")]
        public BowlingStats OdIs { get; set; }

        [JsonProperty("tests")]
        public BowlingStats Tests { get; set; }
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
