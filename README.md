# GeoTime
GeoTime aims to be a fast, lightweight, and memory-efficient C# library for querying timezone abbreviations.

## Architecture
 - **TZAbbr**: An entity representing an abbreviated timezone, which holds: a unique ID, an abbreviation, a name, and an offset (in seconds).
 - **TZAbbrLookup**: A high-level service that allows looking up information about abbreviated timezones
 - **TZAbbrLookupFactory**: A factory for creating instances of a **TZAbbrLookup** service
 - **TZAbbrStore**: A low-level service that holds a read-only dictionary, mapping unique primary keys/IDs to abbreviated timezone (`IReadOnlyDictionary<int, TZAbbr>`)
 - **TZAmbiguityStore**: A low level indexed service that holds a read-only dictionary, mapping timezone abbreviations to a set of timezone IDs (`IReadOnlyDictionary<string,HashSet<int>>`)

### The concept of ambiguity
We say a timezone abbreviation is *ambiguous*, if it can possibly stand for multiple different timezones. For example, **BST** can mean:
 - [Bangladesh Standard Time](https://en.wikipedia.org/wiki/Bangladesh_Standard_Time) (UTC -06)
 - [Bougainville Standard Time](https://en.wikipedia.org/wiki/Bougainville_Standard_Time) (UTC +11)
 - [British Summer Time](https://en.wikipedia.org/wiki/British_Summer_Time) (UTC +01)

## Building the timezone data
The original timezone data is auto-extracted from an HTML table using a JS snippet, and then placed into `data/timezones.json`.

To build the data and make it usable, you can run the `GeoTime.Build` project, which will produce two files in the `/artifacts` folder:

 - `artifacts/timezones.list.json`: Holds a map with integer IDs mapped to an abbreviated timezone.
 - `artifacts/timezones.ambiguity.json`: Holds a map that maps the timezone abbreviations (string keys) to the unique ID of a timezone it can stand for (array of integers).

Notes:
The following issues are being worked on.
 - Currently, the `GeoTime.Build` project cannot be ran with `dotnet run`, and has to be built with Microsoft Studio IDE.
 - The artifacts are not automatically placed into the `artifacts` folder, and will have to be manually moved from `src/GeoTime.Build/bin/Release/net5.0` to `artifacts`.

## Usage
```csharp
TZAbbrLookup lookup = new TZAbbrLookupFactory().GetLookup();
```

### Check if an abbreviation is ambiguous
Checking if an abbreviation is ambiguous takes constant time(O(1) time).

```csharp
lookup.IsAbbrAmbiguous( "CST" ); // true
lookup.IsAbbrAmbiguous( "ART" ); // false
```

### Get an abbreviated timezone by ID or name
Querying an abbreviated timezone by:
  * their unique integer ID takes O(1) time.
  * their unique full name does a linear search, and will therefore take O(n) time, where `n` represents the number of timezone abbreviations.

```csharp
TZAbbr novosibirsk = lookup.GetTimeZone( 149 );
// new TZAbbr( 149, "NOVST", "Novosibirsk Summer Time", 25200 )

TZAbbr argentina = lookup.GetTimeZone( "Argentina Time" );
// new TZAbbr( 22, "ART", "Argentina Time", -10800 )

argentina.Abbr; // "ART"
argentina.OffsetInSeconds; // -10800
argentina.GetOffset().ToString(); // "UTC -03"

TZAbbr someTZ = lookup.GetTimeZone( "Foo" );
// throws TimeZoneNotFoundException
```

### Get possible timezones from an abbreviation
Querying a list of timezones by abbreviation will take constant time (O(1) time).

```csharp
lookup.GetTimeZonesByAbbr( "CST" );
/**
   new HashSet<TZAbbr>() {
      new TZAbbr( 61, "CST", "Central Standard Time", -21600 ),
      new TZAbbr( 62, "CST", "China Standard Time", 28800 ),
      new TZAbbr( 63, "CST", "Cuba Standard Time", -18000 )
   }
 **/

lookup.GetTimeZonesByABbr( "PGT" );
/**
   new HashSet<TZAbbr>() {
      new TZAbbr( 166, "PGT", "Papua New Guinea Time", 36000 )
   }
 **/
```
