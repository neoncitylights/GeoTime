using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GeoTime.Build {
	public class TZAScraper {
		private const string FileNameList = "timezones.list.json";
		private const string FileNameAmbiguity = "timezones.ambiguity.json";

		public void Scrape() {
			Dictionary<int, object> abbrList = new();
			Dictionary<string, HashSet<int>> abbrAmbiguity = new();

			using StreamReader reader = File.OpenText( "timezones.json" );
			JArray o = (JArray)JToken.ReadFrom( new JsonTextReader( reader ) );
			for ( int i = 0; i < o.Count; i++ ) {
				JObject tzaObject = (JObject)o[i];

				string abbreviation = CleanString( (string)tzaObject["Abbreviation"] );
				string tzName = CleanString( (string)tzaObject["Time zone name"] );
				int offset = GetOffsetInSeconds( CleanOffsetString( (string)tzaObject["Offset"] ) );

				object tza = new {
					id = i,
					abbr = abbreviation,
					name = tzName,
					offset
				};

				abbrList.Add( i, tza );

				if ( abbrAmbiguity.ContainsKey( abbreviation ) ) {
					abbrAmbiguity[abbreviation].Add( i );
				}
				else {
					abbrAmbiguity.Add( abbreviation, new HashSet<int> { i } );
				}

				Console.WriteLine(
					string.Format(
						"{0} ({1}): {2}\t{3} seconds\t{4}",
						$"{i + 1,3}/{o.Count}",
						string.Format( "{0:P2}", ( i + 1 ) / (double)o.Count ),
						abbreviation,
						offset,
						tzName
					)
				);
			}

			File.WriteAllText(
				FileNameList,
				JsonConvert.SerializeObject( abbrList, Formatting.Indented )
			);

			Console.WriteLine( $"DONE: Finished writing to {FileNameList}" );

			File.WriteAllText(
				FileNameAmbiguity,
				JsonConvert.SerializeObject( abbrAmbiguity, Formatting.None )
			);

			Console.WriteLine( $"DONE: Finished writing to {FileNameAmbiguity}" );
		}

		private string CleanString( string value ) {
			return value.Split( '\n' )[0].Trim();
		}

		private string CleanOffsetString( string v ) {
			string cleanedOffset = v.Trim();
			if ( cleanedOffset.StartsWith( "UTC" ) ) {
				cleanedOffset = v.Remove( v.IndexOf( "UTC" ), 3 ).Trim();
			}

			if ( string.IsNullOrEmpty( cleanedOffset ) ) {
				return cleanedOffset;
			}

			if ( cleanedOffset.Contains( "/" ) ) {
				cleanedOffset = cleanedOffset.Split( "/" )[0].Trim();
			}

			return cleanedOffset;
		}

		private int GetOffsetInSeconds( string utc ) {
			int hours;
			int mins;

			if ( string.IsNullOrEmpty( utc ) ) {
				return 0;
			}

			if ( utc.Contains( ':' ) ) {
				string[] hoursAndMins = utc.Split( ':' );
				hours = int.Parse( hoursAndMins[0] );
				mins = int.Parse( hoursAndMins[1] );
			}
			else {
				hours = int.Parse( utc );
				mins = 0;
			}

			return ( hours * 60 * 60 ) + ( mins * 60 );
		}
	}
}
