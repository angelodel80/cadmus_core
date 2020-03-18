﻿using Cadmus.Core;
using Fusi.Tools.Config;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cadmus.Parts.General
{
    /// <summary>
    /// Index keywords part. This parts contains a list of index keywords,
    /// which are a specialization of <see cref="Keyword"/>'s to represent
    /// entries in a traditional index.
    /// Tag: <c>net.fusisoft.index-keywords</c>.
    /// </summary>
    /// <seealso cref="PartBase" />
    [Tag("net.fusisoft.index-keywords")]
    public sealed class IndexKeywordsPart : PartBase
    {
        /// <summary>
        /// Keywords.
        /// </summary>
        public List<IndexKeyword> Keywords { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexKeywordsPart"/>
        /// class.
        /// </summary>
        public IndexKeywordsPart()
        {
            Keywords = new List<IndexKeyword>();
        }

        /// <summary>
        /// Adds the specified keyword. If such keywords already exist,
        /// the old ones are replaced by the new keyword.
        /// </summary>
        /// <param name="keyword">The keyword.</param>
        /// <exception cref="ArgumentNullException">keyword</exception>
        public void AddKeyword(IndexKeyword keyword)
        {
            if (keyword == null) throw new ArgumentNullException(nameof(keyword));

            Keywords.RemoveAll(k => k.IndexId == keyword.IndexId
                               && k.Language == keyword.Language
                               && k.Value == keyword.Value);
            Keywords.Add(keyword);
        }

        /// <summary>
        /// Get all the key=value pairs exposed by the implementor. Each key is
        /// <c>xkeyword.{indexId}.{lang}</c> where <c>{indexId}</c> is the index
        /// ID (whic may be empty), and <c>{lang}</c> is its language value;
        /// e.g. <c>keyword..eng</c> as name and <c>sample</c> as value.
        /// The pins are returned sorted by index ID, language and then value.
        /// </summary>
        /// <returns>pins</returns>
        public override IEnumerable<DataPin> GetDataPins()
        {
            if (Keywords == null || Keywords.Count == 0)
                return Enumerable.Empty<DataPin>();

            List<DataPin> pins = new List<DataPin>();

            foreach (string indexId in Keywords.Select(k => k.IndexId ?? "")
                .OrderBy(s => s).Distinct())
            {
                var keysByLang = from k in Keywords
                                 where (k.IndexId ?? "") == indexId
                                 group k by k.Language
                                     into g
                                 orderby g.Key
                                 select g;

                foreach (var g in keysByLang)
                {
                    string[] values = (from k in g
                                       orderby k.Value
                                       select k.Value).ToArray();

                    pins.AddRange(from value in values
                                  select CreateDataPin(
                                    $"xkeyword.{indexId}.{g.Key}", value));
                }
            }

            return pins;
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"[Index Keywords] {Keywords.Count}";
        }
    }
}