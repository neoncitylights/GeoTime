using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace GeoTime {
	public interface ITZAbbrLookup {
		public bool IsAbbrAmbiguous( int id );
		public bool IsAbbrAmbiguous( string abbr );
		public bool IsAbbrAmbiguous( TZAbbr tzAbbr );

		public TZAbbr GetTimeZone( int id );
		public TZAbbr GetTimeZone( string name );
		public bool TryGetTimeZone( int id, [MaybeNullWhen( false )] out TZAbbr value );
		public bool TryGetTimeZone( string name, [MaybeNullWhen( false )] out TZAbbr value );

		public HashSet<TZAbbr> GetTimeZonesByAbbr( string abbr );
		public bool TryGetTimeZonesByAbbr( string abbr, [MaybeNullWhen( false )] out HashSet<TZAbbr> value );
	}
}
