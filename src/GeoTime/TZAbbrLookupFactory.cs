using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Newtonsoft.Json;

namespace GeoTime {
	public class TZAbbrLookupFactory {
		public TZAbbrLookup GetLookup() {
			using StreamReader listReader = File.OpenText( "timezones.list.json" );
			using StreamReader ambiguityReader = File.OpenText( "timezones.ambiguity.json" );
			Dictionary<int, TZAbbr> list =
				JsonConvert.DeserializeObject<Dictionary<int, TZAbbr>>(
				listReader.ReadToEnd() );

			Dictionary<string, HashSet<int>> ambiguity =
				JsonConvert.DeserializeObject<Dictionary<string, HashSet<int>>>(
				ambiguityReader.ReadToEnd() );

			return new TZAbbrLookup(
				new TZAbbrStore( new ReadOnlyDictionary<int, TZAbbr>( list ) ),
				new TZAmbiguityStore( new ReadOnlyDictionary<string, HashSet<int>>( ambiguity ) )
			);
		}
	}
}
