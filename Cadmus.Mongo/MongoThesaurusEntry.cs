﻿using Cadmus.Core.Config;
using System;

namespace Cadmus.Mongo
{
    /// <summary>
    /// Mongo thesaurus entry.
    /// </summary>
    public class MongoThesaurusEntry
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoThesaurusEntry"/> class.
        /// </summary>
        public MongoThesaurusEntry()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoThesaurusEntry"/> class.
        /// </summary>
        /// <param name="entry">The entry.</param>
        /// <exception cref="ArgumentNullException">entry</exception>
        public MongoThesaurusEntry(ThesaurusEntry entry)
        {
            if (entry == null) throw new ArgumentNullException(nameof(entry));

            Id = entry.Id;
            Value = entry.Value;
        }

        /// <summary>
        /// Get a <see cref="ThesaurusEntry"/> from this object.
        /// </summary>
        /// <returns>The entry.</returns>
        public ThesaurusEntry ToThesaurusEntry() => new ThesaurusEntry(Id, Value);

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"[{Id}] {Value}";
        }
    }
}
