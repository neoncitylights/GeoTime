using System;
using System.Collections.Generic;

namespace GeoTime.Demo {
	class Program {
		static void Main() {
			TZAbbrLookup lookup = new TZAbbrLookupFactory().GetLookup();

			while ( true ) {
				Console.Write( "Type in a timezone abbreviation: " );
				string abbr = Console.ReadLine();

				bool tzsFound = lookup.TryGetTimeZonesByAbbr( abbr, out HashSet<TZAbbr> timezones );

				if ( tzsFound ) {
					int i = 1;
					foreach ( TZAbbr tz in timezones ) {
						Console.WriteLine( tz.ToString() );
						i++;
					}
				}
				else {
					Console.WriteLine( $"The timezone {abbr} does not exist." );
				}

				Console.WriteLine( "" );
			}
		}
	}
}
