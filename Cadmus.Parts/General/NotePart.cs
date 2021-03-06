﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cadmus.Core;
using Fusi.Tools.Config;

namespace Cadmus.Parts.General
{
    /// <summary>
    /// Generic text note. A note just contains some text in any format chosen by
    /// the implementor, plus an optional tag to categorize notes where required.
    /// Tag: <c>net.fusisoft.note</c>.
    /// </summary>
    [Tag("net.fusisoft.note")]
    public class NotePart : PartBase, IHasText
    {
        /// <summary>
        /// Gets or sets the optional tag linked to this note. You might want to use
        /// this value to categorize or group notes according to some criteria.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Gets or sets the text. The format of the text is chosen by the
        /// implementor (it might be plain text, Markdown, RTF, HTML, XML, etc).
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets the text.
        /// </summary>
        /// <returns>full text</returns>
        public string GetText() => Text;

        /// <summary>
        /// Get all the key=value pairs exposed by the implementor.
        /// Pins: <c>tag</c>=tag if defined, else none.
        /// </summary>
        /// <returns>pins</returns>
        public override IEnumerable<DataPin> GetDataPins()
        {
            return Tag != null
                ? new[]
                {
                    CreateDataPin("tag", Tag)
                }
                : Enumerable.Empty<DataPin>();
        }

        /// <summary>
        /// Returns a <see cref="String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("[Note]");
            if (Tag != null) sb.Append(" (").Append(Tag).Append(')');
            if (Text != null)
            {
                sb.Append(Text.Length > 100
                    ? Text.Substring(0, 100) + "..."
                    : Text);
            }
            return sb.ToString();
        }
    }
}
