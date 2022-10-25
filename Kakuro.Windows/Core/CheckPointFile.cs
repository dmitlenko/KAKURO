using Kakuro.Engine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kakuro.Windows.Core
{
    public class CheckPointFile
    {
        public List<CheckPoint> CheckPoints = new List<CheckPoint>();

        public string FileName { get; set; } = "checkpoints.kcp";

        public CheckPointFile()
        {
            Load();
        }

        public void Load()
        {
            try
            {
                CheckPoints = (List<CheckPoint>)Serealizer.Deserialize(FileName);
            }
            catch
            {
                CheckPoints = new List<CheckPoint>();
            }
        }

        public void Save()
        {
            Serealizer.Serialize(CheckPoints, FileName);
        }
    }
}
