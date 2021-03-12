using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace GeoTime {
	public class TZAbbrLookupFactory {
		public TZAbbrLookup GetLookup() {
			Assembly asm = Assembly.GetExecutingAssembly();
			using StreamReader listReader = GetStreamReader( asm, "GeoTime.timezones.list.json" );
			using StreamReader ambiguityReader = GetStreamReader( asm, "GeoTime.timezones.ambiguity.json" );

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

		private StreamReader GetStreamReader( Assembly asm, string resourceName ) {
			return new( asm.GetManifestResourceStream( resourceName ) );
		}
	}
}
