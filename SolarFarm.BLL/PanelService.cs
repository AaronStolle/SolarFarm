using SolarFarm.Core.Interfaces;
using SolarFarm.Core.DTO;
using System;
using SolarFarm.DAL;
using System.Collections.Generic;

namespace SolarFarm.BLL
{
    public class PanelService : IPanelService
    {
        private IPanelRepository _repo;

        public PanelService()
        {
            _repo = new SolarFarmRepository();
        }

        public Result<SolarPanelRecord> Add(SolarPanelRecord record)
        {
            List<SolarPanelRecord> records = _repo.GetAll().Data;
            Result<SolarPanelRecord> result = new();
            result.Data = record;
            result.Success = true;

            if (record.Row < 1 || record.Row > 250)
            {
                result.Success = false;
                result.Message = "[Err]\nRow must be between 1 and 250.";
            }
            if (record.Column < 1 || record.Column > 250)
            {
                result.Success = false;
                result.Message = "[Err]\nColumn must be between 1 and 250.";
            }
            for (int i = 0; i < records.Count; i++)
            {
                if (records[i].Section == record.Section && records[i].Row == record.Row && records[i].Column == record.Column)
                {
                    result.Success = false;
                    result.Message = $"[Err]\nPanel already exists at {record.Section}-{record.Row}-{record.Column}";
                    break;
                }
            }
            if (record.Material != MaterialMode.MultiSi || record.Material != MaterialMode.MonoSi ||
                record.Material != MaterialMode.aSi || record.Material != MaterialMode.CdTe || record.Material != MaterialMode.CIGS)
            {
                result.Success = false;
                result.Message = "Invalid input only accepts MultiSi, MonoSi, aSi, CdTe, CIGS";

            }
            if (record.Year > 2022)
            {
                result.Success = false;
                result.Message = "Future year cannot be entered";
            }
            if (record.IsTracking != "yes" && record.IsTracking != "no")
            {
                result.Success = false;
                result.Message = "Invalid Entry! Try again.";
            }

            else
            {
                result = _repo.Add(record);
                result.Message = $"[Success]\nPanel {record.Section}-{record.Row}-{record.Column} added.";
            }
            return result;

        }

        public Result<SolarPanelRecord> Edit(SolarPanelRecord record)
        {
            List<SolarPanelRecord> records = _repo.GetAll().Data;
            Result<SolarPanelRecord> result = new();
            for (int i = 0; i < records.Count; i++)
            {
                if (records[i].Section == record.Section && records[i].Row == record.Row && records[i].Column == record.Column)
                {
                    if (record.Section == "")
                    {
                        record.Section = records[i].Section;
                    }
                    if (record.Row == -1)
                    {
                        record.Row = records[i].Row;
                    }
                    if (record.Column == -1)
                    {
                        record.Column = records[i].Column;
                    }
                    if (record.Material == 0)
                    {
                        record.Material = records[i].Material;
                    }
                    if (record.Year == -1)
                    {
                        record.Year = records[i].Year;
                    }
                    if (record.IsTracking == "")
                    {
                        record.IsTracking = records[i].IsTracking;
                    }
                    result.Data = record;
                    break;
                }
            }
            result = _repo.Edit(record);
            return result;
        }

        public Result<SolarPanelRecord> Get(string section, int row, int column)
        {
            Result<List<SolarPanelRecord>> records = _repo.GetAll();
            Result<SolarPanelRecord> result = new();

            foreach (SolarPanelRecord record in records.Data)
            {
                if (record.Section == section && record.Row == row && record.Column == column)
                {
                    result.Data = record;
                    result.Success = true;
                    result.Message = "";
                }
            }
            result.Success = false;
            result.Message = "Panel doesn't Exist";
            result.Data = null;
            return result;
        }

        public Result<List<SolarPanelRecord>> LoadSection(string section)
        {
            Result<List<SolarPanelRecord>> records = _repo.GetAll();
            Result<List<SolarPanelRecord>> recordList = new();

            foreach (SolarPanelRecord record in records.Data)
            {
                if (record.Section == section)
                {

                    recordList.Data = new();
                    recordList.Success = true;
                    recordList.Message = "";
                    return recordList;
                }
            }
            recordList.Success = false;
            recordList.Message = "Section doesn't exist";
            recordList.Data = null;
            return recordList;
        }

        public Result<SolarPanelRecord> Remove(string section, int row, int column)
        {
            Result<SolarPanelRecord> result;
            result = _repo.Remove(section, row, column);

            if (result.Success)
            {
                result.Message = $"[Success]\nPanel at {section}-{row}-{column} was removed.";
                return result;
            }
            else
            {
                result.Message = $"[Err]\nThere is no panel {section}-{row}-{column}.";
                return null;
            }
        }

        public void WriteAll()
        {
            _repo.WriteAll();
        }
    }
}
