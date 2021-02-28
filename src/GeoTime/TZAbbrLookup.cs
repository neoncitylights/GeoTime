using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace GeoTime {
	public class TZAbbrLookup : ITZAbbrLookup {
		public TZAbbrLookup(
			TZAbbrStore mainStore,
			TZAmbiguityStore ambiguityStore
		) {
			MainStore = mainStore;
			AmbiguityStore = ambiguityStore;
		}

		public TZAbbrStore MainStore { get; }
		public TZAmbiguityStore AmbiguityStore { get; }

		/// <inheritdoc />
		public TZAbbr GetTimeZone( int id ) {
			return MainStore[id];
		}

		/// <inheritdoc />
		public TZAbbr GetTimeZone( string name ) {
			foreach ( KeyValuePair<int, TZAbbr> kvp in MainStore ) {
				if ( name == kvp.Value.Name ) {
					return kvp.Value;
				}
			}

			throw new TimeZoneNotFoundException();
		}

		/// <inheritdoc />
		public HashSet<TZAbbr> GetTimeZonesByAbbr( string abbr ) {
			HashSet<int> tzIds = AmbiguityStore[abbr];
			HashSet<TZAbbr> timezones = new();
			foreach ( int id in tzIds ) {
				timezones.Add( GetTimeZone( id ) );
			}

			return timezones;
		}

		/// <inheritdoc />
		public bool IsAbbrAmbiguous( int id ) {
			return IsAbbrAmbiguous( GetTimeZone( id ).Abbr );
		}

		/// <inheritdoc />
		public bool IsAbbrAmbiguous( string abbr ) {
			return AmbiguityStore[abbr].Count > 1;
		}

		/// <inheritdoc />
		public bool IsAbbrAmbiguous( TZAbbr tzAbbr ) {
			return IsAbbrAmbiguous( tzAbbr.Abbr );
		}

		/// <inheritdoc />
		public bool TryGetTimeZone( int id, [MaybeNullWhen( false )] out TZAbbr value ) {
			if ( !MainStore.ContainsKey( id ) ) {
				value = MainStore[id];
				return true;
			}

			value = null;
			return false;
		}

		/// <inheritdoc />
		public bool TryGetTimeZone( string name, [MaybeNullWhen( false )] out TZAbbr value ) {
			foreach ( KeyValuePair<int, TZAbbr> kvp in MainStore ) {
				if ( name == kvp.Value.Name ) {
					value = kvp.Value;
					return true;
				}
			}

			value = null;
			return false;
		}

		/// <inheritdoc />
		public bool TryGetTimeZonesByAbbr( string abbr, [MaybeNullWhen( false )] out HashSet<TZAbbr> value ) {
			if ( !AmbiguityStore.ContainsKey( abbr ) ) {
				value = null;
				return false;
			}

			HashSet<int> tzIds = AmbiguityStore[abbr];
			HashSet<TZAbbr> timezones = new();
			foreach ( int id in tzIds ) {
				timezones.Add( GetTimeZone( id ) );
			}

			value = timezones;
			return true;
		}
	}
}
