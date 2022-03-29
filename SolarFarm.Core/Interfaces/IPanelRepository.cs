using System;
using System.Collections.Generic;
using SolarFarm.Core.DTO;


namespace SolarFarm.Core.Interfaces
{
    public interface IPanelRepository
    {
        Result<List<SolarPanelRecord>> GetAll();          // Retrieves all records from storage
        Result<SolarPanelRecord> Add(SolarPanelRecord record);  // Adds a record to storage
        Result<SolarPanelRecord> Remove(string section, int row, int column);   // Removes record for Section, Row, Column
        Result<SolarPanelRecord> Edit(SolarPanelRecord record); // Replaces a record with the Section, Row, Column with new info
        void WriteAll();

    }
}
