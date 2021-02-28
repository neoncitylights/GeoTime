using System;
using System.Diagnostics;
using Newtonsoft.Json;
using NodaTime;

namespace GeoTime {
	[DebuggerDisplay("({GetOffset().ToString()}) | {Abbr} | {Name}")]
	public record TZAbbr(
		[property:JsonProperty( "id" )]
		int Id,
		[property:JsonProperty( "abbr" )]
		string Abbr,
		[property:JsonProperty( "name" )]
		string Name,
		[property:JsonProperty( "offset" )]
		int OffsetInSeconds
	) : IComparable<TZAbbr> {
		public int CompareTo( TZAbbr other ) => OffsetInSeconds.CompareTo( other.OffsetInSeconds );
		public Offset GetOffset() => Offset.FromSeconds( OffsetInSeconds );
		public override string ToString() => $"(UTC {GetOffset()}) {Abbr}: {Name}";
	}
}
