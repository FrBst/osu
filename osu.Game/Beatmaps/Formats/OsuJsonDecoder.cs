﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.IO;
using osu.Game.IO.Serialization;

namespace osu.Game.Beatmaps.Formats
{
    public class OsuJsonDecoder : BeatmapDecoder
    {
        public static void Register()
        {
            AddDecoder<OsuJsonDecoder>("{");
        }

        protected override void ParseFile(StreamReader stream, Beatmap beatmap)
        {
            stream.BaseStream.Position = 0;
            stream.DiscardBufferedData();

            try
            {
                string fullText = stream.ReadToEnd();
                fullText.DeserializeInto(beatmap);
            }
            catch
            {
                // Temporary because storyboards are deserialized into beatmaps at the moment
                // This try-catch shouldn't exist in the future
            }

            foreach (var hitObject in beatmap.HitObjects)
                hitObject.ApplyDefaults(beatmap.ControlPointInfo, beatmap.BeatmapInfo.BaseDifficulty);
        }
    }
}
