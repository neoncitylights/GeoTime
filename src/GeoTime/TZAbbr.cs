using Newtonsoft.Json;
using NodaTime;

namespace GeoTime {
	public record TZAbbr(
		[property:JsonProperty( "id" )]
		int Id,
		[property:JsonProperty( "abbr" )]
		string Abbr,
		[property:JsonProperty( "name" )]
		string Name,
		[property:JsonProperty( "offset" )]
		int OffsetInSeconds
	) {
		public Offset GetOffset() => Offset.FromSeconds( OffsetInSeconds );
	}
}
