using System;
using System.Collections.Generic;
using System.IO;
using SolarFarm.Core.DTO;
using SolarFarm.Core.Interfaces;

namespace SolarFarm.DAL
{
    public class SolarFarmRepository : IPanelRepository
    {
        private List<SolarPanelRecord> _records;

        public SolarFarmRepository()
        {
            List<SolarPanelRecord> records = new();
            string path = Directory.GetCurrentDirectory() + @"\SolarPanelRepository.csv";
            if (File.Exists(path))
            {

                using (StreamReader sr = new StreamReader(path))
                {
                    string CurrentLine = sr.ReadLine();
                    CurrentLine = sr.ReadLine();

                    while (CurrentLine != null)
                    {
                        SolarPanelRecord record = new();
                        string[] columns = CurrentLine.Split(',');

                        record.Section = columns[0];
                        record.Row = int.Parse(columns[1]);
                        record.Column = int.Parse(columns[2]);
                        record.Material = Enum.Parse<MaterialMode>(columns[3]);
                        record.Year = int.Parse(columns[4]);
                        record.IsTracking = columns[5];

                        records.Add(record);

                        CurrentLine = sr.ReadLine();
                    }
                    _records = records;
                }
            }
            else
            {
                File.Create(path).Close();
            }
        }

        public Result<SolarPanelRecord> Add(SolarPanelRecord record)
        {
            Result<SolarPanelRecord> result = new();
            _records.Add(record);
            result.Message = "";
            result.Success = true;
            result.Data = record;
            return result;
        }

        public Result<SolarPanelRecord> Edit(SolarPanelRecord record)
        {
            Result<SolarPanelRecord> result = new();
            result.Data = record;
            for (int i = 0; i < _records.Count; i++)
            {
                if (_records[i].Section == record.Section && _records[i].Row == record.Row && _records[i].Column == record.Column)
                {
                    _records[i] = record;
                }
            }
            return result;
        }

        public Result<List<SolarPanelRecord>> GetAll()
        {
            Result<List<SolarPanelRecord>> result = new();
            result.Success = true;
            result.Message = "";
            result.Data = new(_records);
            return result;
        }

        public Result<SolarPanelRecord> Remove(string section, int row, int column)
        {
            Result<SolarPanelRecord> result = new();
            for (int i = 0; i < _records.Count; i++)
            {
                if (_records[i].Section == section && _records[i].Row == row && _records[i].Column == column)
                {
                    result.Data = _records[i];
                    result.Success = true;
                    result.Message = "";
                    _records.Remove(_records[i]);
                }
            }
            return result;
        }
        public void WriteAll()
        {
            string path = Directory.GetCurrentDirectory() + @"\SolarPanelRepository.csv";

            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine("Section,Row,Column,YearInstalled,Material,IsTracking");

                foreach (SolarPanelRecord record in _records)
                {

                    sw.WriteLine($"{record.Row},{record.Column},{record.Material},{record.Year},{record.IsTracking}");
                }
            }

        }
    }
}
