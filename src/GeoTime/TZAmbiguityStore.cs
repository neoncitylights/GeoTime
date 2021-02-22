using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace GeoTime {
	public class TZAmbiguityStore : IReadOnlyDictionary<string, HashSet<int>> {
		public TZAmbiguityStore( IReadOnlyDictionary<string, HashSet<int>> store ) {
			Store = store;
		}

		public HashSet<int> this[string key] => Store[key];
		public IReadOnlyDictionary<string, HashSet<int>> Store { get; }
		public IEnumerable<string> Keys => Store.Keys;
		public IEnumerable<HashSet<int>> Values => Store.Values;
		public int Count => Store.Count;

		public bool ContainsKey( string key ) =>
			Store.ContainsKey( key );

		public IEnumerator<KeyValuePair<string, HashSet<int>>> GetEnumerator() =>
			Store.GetEnumerator();

		public bool TryGetValue( string key, [MaybeNullWhen( false )] out HashSet<int> value ) =>
			Store.TryGetValue( key, out value );

		IEnumerator IEnumerable.GetEnumerator() =>
			( (IEnumerable)Store ).GetEnumerator();
	}
}
