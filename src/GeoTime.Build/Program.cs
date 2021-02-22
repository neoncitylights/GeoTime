using System;

namespace GeoTime.Build {
	class Program {
		public static void Main() {
			Console.Clear();
			new TZAScraper().Scrape();
		}
	}
}
