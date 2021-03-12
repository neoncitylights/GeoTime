using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace GeoTime {
	/// <summary>
	/// Interface defining the contract of a high-level service for querying
	/// information about abbreviated timezones
	/// </summary>
	public interface ITZAbbrLookup {
		/// <summary>
		/// Checks if a timezone abbreviation is ambiguous or unique
		/// </summary>
		/// <exception cref="KeyNotFoundException"></exception>
		/// <param name="id">A unique integer ID representing a timezone</param>
		/// <returns><c>true</c> means the abbreviation is ambiguous, <c>false</c> means the abbreviation is unique</returns>
		public bool IsAbbrAmbiguous( int id );

		/// <summary>
		/// Checks if a timezone abbreviation is ambiguous or unique
		/// </summary>
		/// <exception cref="KeyNotFoundException"></exception>
		/// <param name="abbr">A string of a timezone abbreviation</param>
		/// <returns><c>true</c> means the abbreviation is ambiguous, <c>false</c> means the abbreviation is unique</returns>
		public bool IsAbbrAmbiguous( string abbr );

		/// <summary>
		/// Checks if a timezone abbreviation is ambiguous or unique
		/// </summary>
		/// <exception cref="KeyNotFoundException"></exception>
		/// <param name="tzAbbr">An instance of a <see cref="TZAbbr"/> entity object</param>
		/// <returns><c>true</c> means the abbreviation is ambiguous, <c>false</c> means the abbreviation is unique</returns>
		public bool IsAbbrAmbiguous( TZAbbr tzAbbr );

		/// <summary>
		/// Retrieves an abbreviated timezone by its ID
		/// </summary>
		/// <exception cref="TimeZoneNotFoundException"></exception>
		/// <param name="id">A unique integer ID representing a timezone</param>
		/// <returns>An instance of a <see cref="TZAbbr"/> entity</returns>
		public TZAbbr GetTimeZone( int id );

		/// <summary>
		/// Retrieves an abbreviated timezone by its full name
		/// </summary>
		/// <exception cref="KeyNotFoundException"></exception>
		/// <param name="name">The full name of an abbreviated timezone as a string</param>
		/// <returns>An instance of a <see cref="TZAbbr"/> entity</returns>
		public TZAbbr GetTimeZone( string name );

		/// <summary>
		/// Tries retrieving a timezone by ID, without throwing an exception
		/// </summary>
		/// <param name="id">A unique integer ID representing a timezone</param>
		/// <param name="value">When this method returns, contains a <see cref="TZAbbr"/> instance. If not, it will be a <c>null</c> value.</param>
		/// <returns>Returns a boolean indicating whether or not the timezone was found</returns>
		public bool TryGetTimeZone( int id, [MaybeNullWhen( false )] out TZAbbr value );

		/// <summary>
		/// Tries retrieving a timezone by name, without throwing an exception
		/// </summary>
		/// <param name="name">The full name of an abbreviated timezone as a string</param>
		/// <param name="value">When this method returns, contains a <see cref="TZAbbr"/> instance. If not, it will be a <c>null</c> value.</param>
		/// <returns>Returns a boolean indicating whether or not the timezone was found</returns>
		public bool TryGetTimeZone( string name, [MaybeNullWhen( false )] out TZAbbr value );

		/// <summary>
		/// Retrieves all the possible timezones that an abbreviation represents
		/// </summary>
		/// <exception cref="KeyNotFoundException"></exception>
		/// <param name="abbr">A string of a timezone abbreviation</param>
		/// <returns>A unique set of <see cref="TZAbbr" /> entities</returns>
		public HashSet<TZAbbr> GetTimeZonesByAbbr( string abbr );

		/// <summary>
		/// Tries retrieving all the possible timezones that an abbreviation represents, without throwing an exception
		/// </summary>
		/// <param name="abbr">A string of a timezone abbreviation</param>
		/// <param name="value">When this method returns, contains a <see cref="HashSet<TZAbbr>"/>. If not, it will be a <c>null</c> value.</param>
		/// <returns>Returns a boolean indicating whether or not the method succeeded</returns>
		public bool TryGetTimeZonesByAbbr( string abbr, [MaybeNullWhen( false )] out HashSet<TZAbbr> value );
	}
}
