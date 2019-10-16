﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Cadmus.Core;
using Fusi.Antiquity.Chronology;
using Fusi.Tools.Config;
using Fusi.Tools.Text;

namespace Cadmus.Lexicon.Parts
{
    /// <summary>
    /// Word etymology. This can include detailed lineages and a generic
    /// textual discussion (or just one of them).
    /// </summary>
    /// <seealso cref="PartBase" />
    [Tag("lexicon-word-etymology")]
    public sealed class WordEtymologyPart : PartBase
    {
        private static readonly TextCutterOptions _options =
            new TextCutterOptions
            {
                LimitAsPercents = false,
                LineFlattening = true,
                MaxLength = 80
            };

        /// <summary>
        /// Gets or sets the optional date for the first attestation of the word.
        /// </summary>
        public HistoricalDate Date { get; set; }

        /// <summary>
        /// Gets or sets the lineage(s) in this etymology.
        /// </summary>
        public List<Lineage> Lineages { get; set; }

        /// <summary>
        /// Gets or sets an optional free textual discussion about this etymology.
        /// </summary>
        public string Discussion { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WordEtymologyPart"/> class.
        /// </summary>
        public WordEtymologyPart()
        {
            Lineages = new List<Lineage>();
        }

        /// <summary>
        /// Get all the key=value pairs exposed by the implementor.
        /// </summary>
        /// <returns>pins</returns>
        public override IEnumerable<DataPin> GetDataPins()
        {
            // TODO pins
            return new DataPin[0];
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            // lineages
            if (Lineages != null)
            {
                Lineage best = Lineages.OrderBy(l => l.Rank).FirstOrDefault();
                if (best != null) sb.Append(best);
                if (Lineages.Count > 1)
                    sb.Append(" (other ").Append(Lineages.Count - 1).Append(")");
            }

            // date
            if (Date != null) sb.Append(" (").Append(Date).Append(")");

            // discussion
            if (!string.IsNullOrEmpty(Discussion))
                sb.Append(TextCutter.Cut(Discussion, _options));

            return sb.ToString();
        }
    }
}
