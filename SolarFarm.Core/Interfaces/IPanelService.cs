using System;
using System.Collections.Generic;
using SolarFarm.Core.DTO;

namespace SolarFarm.Core.Interfaces
{
    public interface IPanelService
    {
        Result<List<SolarPanelRecord>> LoadSection(string section);     // Retrieves only records within section
        Result<SolarPanelRecord> Get(string section, int row, int column);    // Get only the record with the specified info
        Result<SolarPanelRecord> Add(SolarPanelRecord record);                          // Adds a record to storage
        Result<SolarPanelRecord> Remove(string section, int row, int column);      // Removes record for specified info
        Result<SolarPanelRecord> Edit(SolarPanelRecord record);                         // Replaces a record with the same date
        void WriteAll();
    }
}
