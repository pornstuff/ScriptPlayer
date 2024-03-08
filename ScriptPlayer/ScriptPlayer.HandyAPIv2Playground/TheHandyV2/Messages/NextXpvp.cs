﻿using Newtonsoft.Json;

namespace ScriptPlayer.Shared.TheHandyV2
{
    public class NextXpvp
    {
        [JsonProperty("stopOnTarget")]
        public bool StopOnTarget { get; set; }

        [JsonProperty("immediateResponse")]
        public bool ImmediateResponse { get; set; }

        [JsonProperty("position")]
        public double Position { get; set; }

        [JsonProperty("velocity")]
        public double Velocity { get; set; }
    }
}