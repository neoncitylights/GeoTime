using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace GeoTime {
	public class TZAbbrStore : IReadOnlyDictionary<int, TZAbbr> {
		public TZAbbrStore( IReadOnlyDictionary<int, TZAbbr> store ) {
			Store = store;
		}

		public TZAbbr this[int key] => Store[key];
		public IReadOnlyDictionary<int, TZAbbr> Store { get; }
		public IEnumerable<int> Keys => Store.Keys;
		public IEnumerable<TZAbbr> Values => Store.Values;
		public int Count => Store.Count;

		public bool ContainsKey( int key ) =>
			Store.ContainsKey( key );

		public IEnumerator<KeyValuePair<int, TZAbbr>> GetEnumerator() =>
			Store.GetEnumerator();

		public bool TryGetValue( int key, [MaybeNullWhen( false )] out TZAbbr value ) =>
			Store.TryGetValue( key, out value );

		IEnumerator IEnumerable.GetEnumerator() =>
			( (IEnumerable)Store ).GetEnumerator();
	}
}
