using System;
using System.Collections.Generic;
using SolarFarm.Core.DTO;
using SolarFarm.Core.Interfaces;
using SolarFarm.BLL;

namespace SolarFarm
{
    class MenuController
    {
        private ConsoleIO _ui;
        public IPanelService Service { get; set; }

        public MenuController(ConsoleIO ui)
        {
            _ui = ui;
            Service = new PanelService();
        }
        public void Run()
        {
            bool running = true;

            while (running)
            {
                switch (GetMenuChoice())
                {
                    case 0:
                        SaveAll();
                        running = false;
                        break;
                    case 1:
                        LoadPanelSection();
                        break;
                    case 2:
                        AddPanel();
                        break;
                    case 3:
                        UpdatePanel();
                        break;
                    case 4:
                        RemovePanel();
                        break;
                    default:
                        _ui.Error("Invalid entry");
                        break;
                }
            }
        }

        private void RemovePanel()
        {
            _ui.DisplayTitle("Remove a Panel");
            Result<SolarPanelRecord> ret;
            ret = Service.Get(_ui.GetString("Section: "), _ui.GetInt("Row: "), _ui.GetInt("Column: "));
            if (ret.Success)
            {
                _ui.Display(ret.Message);
            }
            else
            {
                _ui.Display(ret.Message);
            }
        }

        private void UpdatePanel()
        {
            _ui.DisplayTitle("Update a Panel");
            Result<SolarPanelRecord> returnCurrent = Service.Get(_ui.GetString("Section: "), _ui.GetInt("Row: "), _ui.GetInt("Column: "));
            if (returnCurrent.Success == true)
            {
                string section = _ui.GetStringOrNull($"Section ({returnCurrent.Data.Section}): ");
                int row = _ui.GetIntOrNull($"Row ({returnCurrent.Data.Row}): ");
                int column = _ui.GetIntOrNull($"Column ({returnCurrent.Data.Column}): ");
                MaterialMode material = _ui.GetMaterialOrNull($"Material ({returnCurrent.Data.Material}): ");
                int year = _ui.GetIntOrNull($"Installation Year ({returnCurrent.Data.Year}): ");
                string isTracking = _ui.YesOrNoOrNull($"Tracking [y/n] ({returnCurrent.Data.IsTracking}): ");
                SolarPanelRecord ret = new();
                ret.Section = section;
                ret.Row = row;
                ret.Column = column;
                ret.Material = material;
                ret.Year = year;
                ret.IsTracking = isTracking;
                Result<SolarPanelRecord> result = new();
                Service.Edit(ret);

            }
            else
            {
                _ui.Display(returnCurrent.Message);
            }
        }

        private void AddPanel()
        {
            _ui.DisplayTitle("Add a Panel");
            Result<SolarPanelRecord> ret; 
            ret = Service.Add(_ui.CreateRecord(""));
            if (ret.Success)
            {
                _ui.Display(ret.Data.ToString());
                _ui.Display(ret.Message);
            }
            else
            {
                _ui.Display(ret.Message);
            }
        }

        private void LoadPanelSection()
        {
            _ui.DisplayTitle("Find Panels by Section");
            Result<List<SolarPanelRecord>> ret;
            string section = _ui.GetString("\nSection Name: ");
            ret = Service.LoadSection(section);
            if (ret.Success)
            {
                foreach (SolarPanelRecord record in ret.Data)
                {
                    _ui.Display(record.ToString());
                }
            }
            else
            {
                _ui.Display(ret.Message);
            }
        }

        private void SaveAll()
        {
            Service.WriteAll();
        }

        public int GetMenuChoice()
        {
            DisplayMenu();
            return _ui.GetInt("Select [0-4]: ");
        }
        public void DisplayMenu()
        {
            _ui.DisplayTitle("Welcome to Solar Farm");
            _ui.DisplayTitle("Main Menu");
            _ui.Display("0. Exit");
            _ui.Display("1. Find Panels by Section");
            _ui.Display("2. Add a Panel");
            _ui.Display("3. Update a Panel");
            _ui.Display("4. Remove a Panel");

        }
    }
}
