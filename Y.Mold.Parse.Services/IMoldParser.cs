namespace Y.Mold.Parse.Services;

   /// <summary>
     /// Mold file parser interface.
     /// </summary>
     public interface IMoldingParser
     {
         /// <summary>
         /// Parse a passed string of data.
         /// </summary>
         /// <param name="data">Incoming mold data (string).</param>
         /// <returns>A list of molding data objects.</returns>
         List<MoldingTag> Parse(string data);
     }
